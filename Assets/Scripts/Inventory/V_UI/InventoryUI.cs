using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Button leftBtn, rightBtn;

    public SlotUI slotUI;

    public int currentIndex;

    private void OnEnable()
    {
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
    }
    private void OnDisable()
    {
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
    }

    /// <summary>
    /// 更新Item UI
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
            slotUI.SetItem(itemDetails);
            currentIndex = index;
            leftBtn.interactable = true;
            rightBtn.interactable = true;
        }
    }

}
