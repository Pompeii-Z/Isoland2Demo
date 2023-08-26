using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList_SO", menuName = "Inventory/ItmeDataList_So")]

public class ItemDataList_SO : ScriptableObject
{
    /// <summary>
    ///在SO文件中 ，用list配置道具信息
    /// </summary>
    public List<ItemDetails> itemDetailsList;

    /// <summary>
    /// 外部获取使用，得到Item的信息
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public ItemDetails GetItemDetails(ItemName itemName)
    {
        return itemDetailsList.Find(i => i.itemName == itemName);//=> 等于 get
        //return itemDetailsList.Find((ItemDetails i) => {
        //    return i.itemName == itemName;
        //});
    }
}

/// <summary>
/// 道具信息
/// </summary>
[System.Serializable]
public class ItemDetails
{
    /// <summary>
    /// 道具名
    /// </summary>
    public ItemName itemName;
    /// <summary>
    /// 精灵图片
    /// </summary>
    public Sprite itemSprite;
}
