using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>, ISaveable
{
    public string startScene;

    public CanvasGroup fadeCanvasGroup;
    private bool isFade;
    public float fadeDuration;

    /// <summary>
    /// 根据游戏状态判断是否可以传送
    /// </summary>
    private bool canTransition;

    private void Start()
    {// Debug.Log(SceneManager.sceneCount);//长度  
        //StartCoroutine(TransitionToScene(string.Empty, startScene));
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }
    private void OnEnable()
    {
        EventHandler.GameStateChangedEvent += OnGameStateChangedEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameStateChangedEvent -= OnGameStateChangedEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    /// <summary>
    /// 根据游戏周目加载初始化场景
    /// </summary>
    /// <param name="gameWeek"></param>
    private void OnStartNewGameEvent(int gameWeek)
    {
        StartCoroutine(TransitionToScene("Menu", startScene));
    }

    /// <summary>
    /// 需要时订阅事件，根据游戏状态在某些时候可以或不可以做什么。
    /// </summary>
    /// <param name="gameState"></param>
    private void OnGameStateChangedEvent(GameState gameState)
    {
        canTransition = gameState == GameState.GamePlay;
    }

    public void Transition(string From, string To)
    {
        if (!isFade && canTransition)
            StartCoroutine(TransitionToScene(From, To));
    }

    //场景跳转
    public IEnumerator TransitionToScene(string From, string To)
    {
        yield return Fade(1);
        if (From != string.Empty)
        {
            EventHandler.CallBeforeSceneUnLoadEvent();
            yield return SceneManager.UnloadSceneAsync(From);
        }

        yield return SceneManager.LoadSceneAsync(To, LoadSceneMode.Additive);

        //多个场景时，设置需要的为激活场景
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));  //Build Settings中 场景索引从0开始

        EventHandler.CallAfterSceneLoadedEvent();
        yield return Fade(0);
    }

    //场景过渡
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;
        //Approximately() 比较两个浮点值是否相似，相似返回true
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;

        isFade = false;
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.currentScene = SceneManager.GetActiveScene().name;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        Transition("Menu", saveData.currentScene);
    }
}
