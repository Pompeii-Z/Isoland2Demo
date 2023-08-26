using UnityEngine;
using UnityEngine.UI;

public class ItemToolTip : MonoBehaviour
{
    public Text itemNameText;

    public void UpdateItemName(ItemName itemName)
    {
        //语法糖
        itemNameText.text = itemName switch
        {
            ItemName.key => "信箱钥匙",
            ItemName.ticket => "一张船票",
            _ => ""
        };
    }
}
