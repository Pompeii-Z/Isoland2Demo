using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void GoBackToMenu()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        TransitionManager.Instance.Transition(currentScene, "Menu");

        //保存游戏进度

    }

    public void StartGameWeek(int gameWeek)
    { 
        EventHandler.CallStartNewGameEvent(gameWeek);
    }

    public void ContinueGame()
    {
        //加载游戏进度
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
