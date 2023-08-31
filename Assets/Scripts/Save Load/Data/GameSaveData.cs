using System.Collections.Generic;

/// <summary>
/// 需要保存的数据添加到这里
/// </summary>
public class GameSaveData
{
    public int gameWeek;
    public Dictionary<string, bool> miniGameStateDict;

    public string currentScene;

    public Dictionary<ItemName, bool> itemAvailableDict;
    public Dictionary<string, bool> interactiveStateDict;

    public List<ItemName> itemList;

}
