using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 道具提示UI
/// </summary>
public class ItemToolTip : MonoBehaviour
{
    public Text itemNameText;

    /// <summary>
    /// 更新道具提示UI
    /// </summary>
    /// <param name="itemName"></param>
    public void UpdateItemName(ItemName itemName)
    {
        //语法糖
        itemNameText.text = itemName switch
        {
            ItemName.key => "信箱钥匙",
            ItemName.ticket => "一张船票",
            ItemName.apple => "苹苹苹果",
            ItemName.blueberry => "蓝蓝蓝莓",
            ItemName.match => "火火火柴",
            _ => ""
        };
    }
}
