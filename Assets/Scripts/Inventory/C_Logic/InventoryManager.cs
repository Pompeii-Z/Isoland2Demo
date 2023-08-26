using System.Collections.Generic;

public class InventoryManager : Singleton<InventoryManager>
{
    public ItemDataList_SO itemData;

    private List<ItemName> itemList = new List<ItemName>();

    private void Start()
    {
        EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(ItemName.none), 0);
    }

    private void OnEnable()
    {
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
        EventHandler.ChangeItemEvent += OnChangeItemEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
        EventHandler.ChangeItemEvent -= OnChangeItemEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }

    private void OnAfterSceneLoadedEvent()
    {
        if (itemList.Count == 0)
            EventHandler.CallUpdateUIEvent(null, -1);
        else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemList[i]), i);
            }
        }
    }

    private void OnChangeItemEvent(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {
            ItemDetails item = itemData.GetItemDetails(itemList[index]);
            EventHandler.CallUpdateUIEvent(item, index);
        }
    }

    private void OnItemUsedEvent(ItemName itemName)
    {
        var index = GetItemIndex(itemName);
        itemList.RemoveAt(index);
        //TODO:单一使用物品效果
        if (itemList.Count == 0)
        {
            EventHandler.CallUpdateUIEvent(null, -1);
        }
    }


    /// <summary>
    /// 添加物品
    /// </summary>
    /// <param name="itemName"></param>
    public void AddItem(ItemName itemName)
    {
        if (!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            //显示对应UI
            EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemName), itemList.Count - 1);
            EventHandler.CallUpdateItemStatusEvent(itemData.GetItemDetails(itemName));
        }
    }

    private int GetItemIndex(ItemName itemName)
    {
        return itemList.IndexOf(itemName);
    }
}

