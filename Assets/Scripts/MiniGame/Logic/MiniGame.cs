using UnityEngine;
using UnityEngine.Events;

public class MiniGame : MonoBehaviour
{
    public UnityEvent OnGameFinish; //暴露面板使用
    public string gameName;
    public bool isPass;

    /// <summary>
    /// 小游戏通过应该执行的内容
    /// </summary>
    public void UpdateMiniGamState()
    {
        //当isPass为true时执行。隐藏该小游戏入口，激活通向H3场景的传送
        if (isPass)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
            OnGameFinish?.Invoke();
        }
    }

}
