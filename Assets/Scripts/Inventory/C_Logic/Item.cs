using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemName itemName;

    public void ItemClicked()
    {
        // 添加背包中并隐藏UI物体
        InventoryManager.Instance.AddItem(itemName);
        this.gameObject.SetActive(false);
    }

}
