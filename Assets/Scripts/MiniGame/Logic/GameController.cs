using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameH2A_SO gameData;

    public GameObject lineParent;
    public LineRenderer linePrefab;
    public Ball ballPrefab;

    public Transform[] holderTransforms;        //所有的位置(7个)

    private void Start()
    {
        DrawLine();
        CreateBall();
    }

    /// <summary>
    /// 根据startBallOrder初始化球(位置及图片)
    /// </summary>
    private void CreateBall()
    {
        for (int i = 0; i < gameData.startBallOrder.Count; i++)
        {
            if (gameData.startBallOrder[i] == BallName.None)//第一个位置不需要设置球，跳过
            {
                holderTransforms[i].GetComponent<Holder>().isEmpty = true;
                continue;
            }
            Ball ball = Instantiate(ballPrefab, holderTransforms[i]);
            holderTransforms[i].GetComponent<Holder>().isEmpty = false;
            ball.SetupBall(gameData.GetBallDetails(gameData.startBallOrder[i]));    
        }
    }

    /// <summary>
    /// 根据配置好的球的线段对应关系(lineConections)和存储好的位置，绘制连线
    /// </summary>
    private void DrawLine()
    {
        foreach (var conections in gameData.lineConections)
        {
            var line = Instantiate(linePrefab, lineParent.transform);
            line.SetPosition(0, holderTransforms[conections.from].position);
            line.SetPosition(1, holderTransforms[conections.to].position);
        }
    }



}
