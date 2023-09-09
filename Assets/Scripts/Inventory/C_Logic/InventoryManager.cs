using System.Collections.Generic;

/// <summary>
/// 逻辑部分
/// </summary>
public class InventoryManager : Singleton<InventoryManager>, ISaveable
{
    public ItemDataList_SO itemData;

    private List<ItemName> itemList = new List<ItemName>();

    private void Start()
    {
        ISaveable saveable = this;
        saveable.SaveableRegister();

        EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(ItemName.none), 0);
    }

    private void OnEnable()
    {
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
        EventHandler.ChangeItemEvent += OnChangeItemEvent;
        EventHandler.MenuAfterSceneLoadedEvent += OnMenuAfterSceneLoadedEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
        EventHandler.ChangeItemEvent -= OnChangeItemEvent;
        EventHandler.MenuAfterSceneLoadedEvent -= OnMenuAfterSceneLoadedEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    /// <summary>
    /// 加载不同周目的要清空数据
    /// </summary>
    /// <param name="obj"></param>
    private void OnStartNewGameEvent(int obj)
    {
        itemList.Clear();
    }

    /// <summary>
    /// 从Menu场景加载之后
    /// </summary>
    private void OnMenuAfterSceneLoadedEvent()
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

    /// <summary>
    /// 更改显示的道具
    /// </summary>
    /// <param name="index"></param>
    private void OnChangeItemEvent(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {
            ItemDetails item = itemData.GetItemDetails(itemList[index]);
            EventHandler.CallUpdateUIEvent(item, index);
        }
    }

    /// <summary>
    /// 使用道具
    /// </summary>
    /// <param name="itemName"></param>
    private void OnItemUsedEvent(ItemName itemName)
    {
        var index = GetItemIndex(itemName);
        itemList.RemoveAt(index);
        //单一使用物品效果
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

    /// <summary>
    /// 获取道具索引
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    private int GetItemIndex(ItemName itemName)
    {
        return itemList.IndexOf(itemName);
    }

    /// <summary>
    /// 获取总的道具数
    /// </summary>
    /// <returns></returns>
    public int GetListCount()
    {
        return itemList.Count;
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.itemList = this.itemList;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.itemList = saveData.itemList;
    }
}

