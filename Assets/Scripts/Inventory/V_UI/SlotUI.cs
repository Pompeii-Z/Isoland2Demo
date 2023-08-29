using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 道具插槽 UI部分
/// </summary>
public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage;
    private ItemDetails currentItem;

    private bool isSelected;

    public ItemToolTip itemToolTip;

    /// <summary>
    /// 设置对应道具UI的sprite和尺寸
    /// </summary>
    /// <param name="itemDetails"></param>
    public void SetItem(ItemDetails itemDetails)
    {
        currentItem = itemDetails;
        this.gameObject.SetActive(true);
        itemImage.sprite = currentItem.itemSprite;
        itemImage.SetNativeSize();
    }

    /// <summary>
    /// 清空UI状态
    /// </summary>
    public void SetEmpty()
    {
        this.gameObject.SetActive(false);
        itemImage.sprite = null;
        isSelected = false;
    }

    /// <summary>
    /// 点击事件，选择/取消选择 道具。
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = !isSelected;
        EventHandler.CallItemSelectedEvent(currentItem, isSelected);
    }

    /// <summary>
    /// 鼠标进入，更新提示UI
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject.activeInHierarchy)
        {
            itemToolTip.gameObject.SetActive(true);
            itemToolTip.UpdateItemName(currentItem.itemName);
        }
    }

    /// <summary>
    /// 鼠标退出，关闭提示UI
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        itemToolTip.gameObject.SetActive(false);
    }
}
