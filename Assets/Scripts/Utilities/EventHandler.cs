using System;

/// <summary>
/// 事件中心
/// </summary>
public static class EventHandler
{
    public static event Action<ItemDetails, int> UpdateUIEvent;
    /// <summary>
    /// UI同步更新
    /// </summary>
    /// <param name="itemDetails"></param>
    /// <param name="index"></param>
    public static void CallUpdateUIEvent(ItemDetails itemDetails, int index)
    {
        UpdateUIEvent?.Invoke(itemDetails, index);
    }   
  
    public static event Action<ItemDetails> UpdateItemStatusEvent;
    /// <summary>
    /// 拾取Item后，更新Item的激活状态为false的事件
    /// </summary>
    /// <param name="itemDetails"></param>
    public static void CallUpdateItemStatusEvent(ItemDetails itemDetails)
    {
        UpdateItemStatusEvent?.Invoke(itemDetails);
    }

    public static event Action AfterSceneLoadedEvent;
    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadedEvent?.Invoke();
    }

    public static event Action BeforeSceneUnLoadEvent;
    public static void CallBeforeSceneUnLoadEvent()
    {
        BeforeSceneUnLoadEvent?.Invoke();
    }

    public static event Action<ItemDetails, bool> ItemSelectedEvent;
    public static void CallItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    { 
        ItemSelectedEvent?.Invoke(itemDetails, isSelected);
    }

    public static event Action<ItemName> ItemUsedEvent;
    public static void CallItemUsedEvent(ItemName itemName)
    {
        ItemUsedEvent?.Invoke(itemName);
    }


}
