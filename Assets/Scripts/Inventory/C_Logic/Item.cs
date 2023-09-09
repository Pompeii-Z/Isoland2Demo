using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemName itemName;

    private Camera mainCamera;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    public void ItemClicked()
    {
        //获取屏幕坐标和图片
        Vector2 screenPosition = mainCamera.WorldToScreenPoint(transform.position);
        Sprite image = transform.GetComponent<SpriteRenderer>().sprite;

        EventHandler.CallUpdateUIMoveEvent(screenPosition, image, itemName);

        // 添加背包中并隐藏UI物体
        //InventoryManager.Instance.AddItem(itemName);
        this.gameObject.SetActive(false);
    }


}
