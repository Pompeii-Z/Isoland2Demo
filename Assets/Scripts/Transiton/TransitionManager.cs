using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>
{
    public CanvasGroup canvasGroup;
    private bool isFade;
    public float fadeDuration;

    private void Start()
    {// Debug.Log(SceneManager.sceneCount);//长度  
    }

    public void Transition(string From, string To)
    {
        if (!isFade)
            StartCoroutine(TransitionToScene(From, To));
    }

    public IEnumerator TransitionToScene(string From, string To)
    {
        yield return Fade(1);

        yield return SceneManager.UnloadSceneAsync(From);

        yield return SceneManager.LoadSceneAsync(To, LoadSceneMode.Additive);

        //多个场景时，设置需要的为激活场景
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));  //Build Settings中 场景索引从0开始

        yield return Fade(0);
    }

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