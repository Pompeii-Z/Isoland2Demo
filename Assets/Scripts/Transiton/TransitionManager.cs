using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>
{
    public string startScene;

    public CanvasGroup canvasGroup;
    private bool isFade;
    public float fadeDuration;

    private void Start()
    {// Debug.Log(SceneManager.sceneCount);//长度  
        StartCoroutine(TransitionToScene(string.Empty, startScene));
    }

    public void Transition(string From, string To)
    {
        if (!isFade)
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

        canvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(canvasGroup.alpha - targetAlpha) / fadeDuration;
        //Approximately() 比较两个浮点值是否相似，相似返回true
        while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        canvasGroup.blocksRaycasts = false;

        isFade = false;
    }

}
