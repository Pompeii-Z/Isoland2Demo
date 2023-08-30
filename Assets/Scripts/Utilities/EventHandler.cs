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

    public static event Action<int> ChangeItemEvent;
    public static void CallChangeItemEvent(int index)
    {
        ChangeItemEvent?.Invoke(index);
    }

    public static event Action<string> ShowDialogueEvent;
    public static void CallShowDialogueEvent(string dialogue)
    {
        ShowDialogueEvent?.Invoke(dialogue);
    }

    public static event Action<GameState> GameStateChangedEvent;
    /// <summary>
    /// 游戏状态改变
    /// </summary>
    /// <param name="gameState"></param>
    public static void CallGameStateChangeEvent(GameState gameState)
    {
        GameStateChangedEvent?.Invoke(gameState);
    }

    public static event Action CheckGameStateEvent;
    public static void CallCheckGameStateEvent()
    { 
        CheckGameStateEvent?.Invoke();
    }

    public static event Action<string> GamePassEvent;
    public static void CallGamePassEvent(string gameName)
    {
        GamePassEvent?.Invoke(gameName);
    }

    public static event Action<int> StartNewGameEvent;
    public static void CallStartNewGameEvent(int gameWeek)
    {
        StartNewGameEvent?.Invoke(gameWeek);
    }

}
