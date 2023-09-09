using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Holder : Interactive
{
    public HashSet<Holder> linkHolders = new HashSet<Holder>();

    public BallName matchBall;      //匹配的球

    public Ball currentBall;

    public bool isEmpty;

    [Header("ball移动速度")]
    [Range(0f, 2f)]
    public float duration = 0.2f;

    public void CheckBall(Ball ball)
    {
        currentBall = ball;
        if (ball.ballDetails.ballName == matchBall)
        {
            currentBall.isMatch = true;
            currentBall.SetRight();
        }
        else
        {
            currentBall.isMatch = false;
            currentBall.SetWrong();
        }
    }

    /// <summary>
    /// 点击Holder:移动球逻辑
    /// </summary>
    public override void EmptyAction()
    {
        foreach (var holder in linkHolders)
        {
            if (holder.isEmpty)
            {
                //移动球
                //currentBall.transform.position = holder.transform.position;
                currentBall.transform.DOMove(holder.transform.position, duration);
                currentBall.transform.SetParent(holder.transform);

                //交换球
                holder.CheckBall(currentBall);
                this.currentBall = null;

                //改变状态
                this.isEmpty = true;
                holder.isEmpty = false;

                EventHandler.CallCheckGameStateEvent();
            }
        }
    }

}
