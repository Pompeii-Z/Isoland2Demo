public interface ISaveable
{
    /// <summary>
    /// 注册 ,将Manager注册进List中
    /// </summary>
    void SaveableRegister()
    {
        SaveLoadManager.Instance.Register(this);
    }

    /// <summary>
    /// 保存游戏数据
    /// </summary>
    /// <returns></returns>
    GameSaveData GenerateSaveData();

    /// <summary>
    /// 恢复游戏数据
    /// </summary>
    /// <param name="saveData"></param>
    void RestoreGameData(GameSaveData saveData);

}
