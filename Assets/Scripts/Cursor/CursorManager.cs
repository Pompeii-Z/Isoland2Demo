using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CursorManager : Singleton<CursorManager>
{
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));//得到鼠标在屏幕的世界坐标  => 等于 get

    private bool canClick = false;

    public RectTransform hand;  //选取道具后手的UI

    private ItemName currentItem;   

    private bool holdItem; //是否选择Item

    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
    }
    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
    }

    private void Update()
    {
        canClick = ObjectAtMousePosition();
        if (hand.gameObject.activeInHierarchy)//手UI与鼠标跟随
        {
            hand.position = Input.mousePosition;
        }
        if (canClick && Input.GetMouseButtonDown(0))
        {
            ClickAction(ObjectAtMousePosition().gameObject);
        }
    }

    /// <summary>
    /// 已使用Item
    /// </summary>
    /// <param name="obj"></param>
    private void OnItemUsedEvent(ItemName obj)
    {
        currentItem = ItemName.none;
        holdItem = false;
        hand.gameObject.SetActive(false);
    }

    /// <summary>
    /// 选择Item 激活手的UI 并把Item名赋值在ClickAction判断使用
    /// </summary>
    /// <param name="itemDetails"></param>
    /// <param name="isSelected"></param>
    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        holdItem = isSelected;
        if (isSelected)
        {
            currentItem = itemDetails.itemName;
        }
        hand.gameObject.SetActive(holdItem);
    }

    /// <summary>
    /// 检测物体标签 判断do something
    /// </summary>
    /// <param name="clickObj"></param>
    public void ClickAction(GameObject clickObj)
    {
        switch (clickObj.tag)
        {
            case "Teleport":
                var teleport = clickObj.GetComponent<Teleport>();
                teleport?.TeleportScene();
                break;
            case "Item":
                var item = clickObj.GetComponent<Item>();
                item?.ItemClicked();
                break;
            case "Interactive":
                var interactive = clickObj.GetComponent<Interactive>();
                if (holdItem)
                    interactive?.CheckItem(currentItem);
                else
                    interactive?.EmptyAction();
                break;
        }
    }

    /// <summary>
    /// 检测与鼠标重叠的碰撞体
    /// </summary>
    /// <returns></returns>
    public Collider2D ObjectAtMousePosition()
    {
        //OverlapPoint 返回与鼠标点击位置重叠的Physics2D(物体)的信息
        return Physics2D.OverlapPoint(mouseWorldPos);
    }

}
