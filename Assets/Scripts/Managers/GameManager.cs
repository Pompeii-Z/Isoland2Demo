using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //记录MiniGame通关状态
    private Dictionary<string, bool> miniGameStateDict = new Dictionary<string, bool>();

    //第几周目
    private int gameWeek;

    private GameController currentGame;

    void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        EventHandler.CallGameStateChangeEvent(GameState.GamePlay);
    }

    private void OnStartNewGameEvent(int gameWeek)
    {
        this.gameWeek = gameWeek;
        miniGameStateDict.Clear();
    }

    /// <summary>
    /// 当H2A通关时会自动返回到H2,所以在此事件中判断是否通过了小游戏，通过了则执行。
    /// </summary>
    private void OnAfterSceneLoadedEvent()
    {
        foreach (var miniGame in FindObjectsOfType<MiniGame>())
        {
            if (miniGameStateDict.TryGetValue(miniGame.gameName, out bool isPass))//通过key尝试获取值，如果获取到则返回true,否则false
            {
                miniGame.isPass = isPass;
                miniGame.UpdateMiniGamState();//当isPass为true时执行。
            }
        }

        currentGame = FindObjectOfType<GameController>();
        currentGame?.SetGameWeekData(gameWeek);

    }

    private void OnGamePassEvent(string gameName)
    {
        //添加进miniGameStateDict字典
        miniGameStateDict[gameName] = true;
    }

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
}
