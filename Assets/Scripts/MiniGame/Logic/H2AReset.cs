using DG.Tweening;
using UnityEngine;

public class H2AReset : Interactive
{
    private Transform gearSprite;

    private void Awake()
    {
        gearSprite = transform.GetChild(0);
    }

    public override void EmptyAction()
    {
        //重置游戏
        GameController.Instance.ResetGame();
        gearSprite.DOPunchRotation(Vector3.forward * 180, 1, 1, 0);
    }

}
