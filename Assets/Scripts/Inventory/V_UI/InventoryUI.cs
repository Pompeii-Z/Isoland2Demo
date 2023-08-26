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
            currentIndex = index;
            slotUI.SetItem(itemDetails);
            if (index > 0)
                leftBtn.interactable = true;
            if (index == -1)
            {
                rightBtn.interactable = false;
                leftBtn.interactable = false;
            }
        }
    }

    public void SwitchItem(int amout)
    {
        var index = currentIndex + amout;
        if (index < currentIndex)
        {
            leftBtn.interactable = false;
            rightBtn.interactable = true;
        }
        else if (index > currentIndex)
        {
            leftBtn.interactable = true;
            rightBtn.interactable = false;
        }
        else
        {
            leftBtn.interactable = true;
            rightBtn.interactable = true;
        }

        EventHandler.CallChangeItemEvent(index);

    }



}
