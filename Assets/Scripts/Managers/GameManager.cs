using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveable
{
    //记录MiniGame通关状态
    private Dictionary<string, bool> miniGameStateDict = new Dictionary<string, bool>();

    //第几周目
    private int gameWeek;

    private GameController currentGame;

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.GamePassEvent += OnGamePassEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.GamePassEvent -= OnGamePassEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }
    void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        EventHandler.CallGameStateChangeEvent(GameState.GamePlay);

        //保存数据
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnStartNewGameEvent(int gameWeek)
    {
        this.gameWeek = gameWeek;//暂存对应的游戏周目int，在OnAfterSceneLoadedEvent中通知GameController设置游戏数据
        miniGameStateDict.Clear();
    }

    /// <summary>
    /// 一，当H2A通关时会自动返回到H2,所以在此事件中判断是否通过了小游戏，通过了更新游戏状态。
    /// 二，加载场景之后判断是第几周目的游戏数据，加载对应周目的小游戏。
    /// </summary>
    private void OnAfterSceneLoadedEvent()
    {
        //一，获取场景中的MiniGame 并判断
        foreach (var miniGame in FindObjectsOfType<MiniGame>())
        {
            if (miniGameStateDict.TryGetValue(miniGame.gameName, out bool isPass))//通过key尝试获取值，如果获取到则返回true,否则false
            {
                miniGame.isPass = isPass;
                miniGame.UpdateMiniGamState();//当isPass为true时执行。
            }
        }

        //二，只有MiniGame中有GameController
        currentGame = FindObjectOfType<GameController>();
        currentGame?.SetGameWeekData(gameWeek);

    }

    /// <summary>
    /// 将通关的小游戏加入字典
    /// </summary>
    /// <param name="gameName"></param>
    private void OnGamePassEvent(string gameName)
    {
        //添加进miniGameStateDict字典
        miniGameStateDict[gameName] = true;
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.gameWeek = this.gameWeek;
        saveData.miniGameStateDict = this.miniGameStateDict;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.gameWeek = saveData.gameWeek;
        this.miniGameStateDict = saveData.miniGameStateDict;
    }
}
