using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void GoBackToMenu()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        TransitionManager.Instance.Transition(currentScene, "Menu");

        //保存游戏进度
        SaveLoadManager.Instance.Save();
    }

    /// <summary>
    /// 面板拖拽方法，传递int值 以加载不同的SO文件，来切换小游戏的难度
    /// </summary>
    /// <param name="gameWeek"></param>
    public void StartGameWeek(int gameWeek)
    {
        EventHandler.CallStartNewGameEvent(gameWeek);
    }

    public void ContinueGame()
    {
        //加载游戏进度
        SaveLoadManager.Instance.Load();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
