using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Props Holder UI
/// </summary>
public class InventoryUI : MonoBehaviour
{
    public Button leftBtn, rightBtn;

    public SlotUI slotUI;

    public int currentIndex;

    public Image propImage;

    [Header("拾取道具速度")]
    [Range(0f, 5f)]
    public float duration = 1f;

    private void Start()
    {
        leftBtn.onClick.AddListener(delegate () { SwitchItem(-1); });
        rightBtn.onClick.AddListener(delegate () { SwitchItem(1); });
    }

    private void OnEnable()
    {
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
        EventHandler.UpdateUIMoveEvent += OnUpdateUIMoveEvent;
    }
    private void OnDisable()
    {
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
        EventHandler.UpdateUIMoveEvent -= OnUpdateUIMoveEvent;
    }

    /// <summary>
    /// 更新 道具，左右按钮等UI index为Count-1
    /// </summary>
    /// <param name="itemDetails"></param>
    /// <param name="index"></param>
    private void OnUpdateUIEvent(ItemDetails itemDetails, int index)
    {
        if (itemDetails == null)
        {
            slotUI.SetEmpty();
            currentIndex = -1;
            leftBtn.interactable = false;
            rightBtn.interactable = false;
        }
        else
        {
            currentIndex = index;
            slotUI.SetItem(itemDetails);

            // 更新左右按钮的交互状态
            var TOTAL_ITEMS = InventoryManager.Instance.GetListCount();
            leftBtn.interactable = index > 0;
            rightBtn.interactable = index < TOTAL_ITEMS - 1;

            //if (index == -1)
            //{
            //    rightBtn.interactable = false;
            //    leftBtn.interactable = false;
            //}
        }
    }

    /// <summary>
    /// 物品切换控制按钮UI状态，并通知更新道具显示
    /// </summary>
    /// <param name="amout"></param>
    public void SwitchItem(int amount)
    {
        var TOTAL_ITEMS = InventoryManager.Instance.GetListCount();
        var index = currentIndex + amount;

        // 确保 index 在合法范围内（0 到 TOTAL_ITEMS-1）
        index = Mathf.Clamp(index, 0, TOTAL_ITEMS - 1);

        // 更新左右按钮的交互状态
        //leftBtn.interactable = index > 0;
        //rightBtn.interactable = index < TOTAL_ITEMS - 1;

        // 触发物品切换事件
        EventHandler.CallChangeItemEvent(index);
    }

    /// <summary>
    /// 拾取道具的移动动画
    /// </summary>
    /// <param name="itemPos"></param>
    /// <param name="itemImage"></param>
    /// <param name="itemName"></param>
    private void OnUpdateUIMoveEvent(Vector2 itemPos, Sprite itemImage, ItemName itemName)
    {
        propImage.gameObject.SetActive(true);
        propImage.transform.position = itemPos;
        propImage.sprite = itemImage;
        propImage.SetNativeSize();

        propImage.transform.DOMove(transform.position, duration).OnComplete(
            () =>
            {
                propImage.gameObject.SetActive(false);
                InventoryManager.Instance.AddItem(itemName);
            });
    }

}
