using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        EventHandler.CallGameStateChangeEvent(GameState.GamePlay);
    }
}
   