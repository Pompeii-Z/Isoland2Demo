 using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string SceneFrom;
    public string SceneToGo;

    //不用这个是为了统一代码 据情况改变
    //private void OnMouseDown()
    //{
    //    TeleportScene();
    //}
    
    public void TeleportScene()
    {
        TransitionManager.Instance.Transition(SceneFrom, SceneToGo);
    }

}
