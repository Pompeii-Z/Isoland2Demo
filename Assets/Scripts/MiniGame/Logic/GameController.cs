using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
    public UnityEvent OnFinish; //拖拽跳转方法加载场景

    [Header("游戏数据")]
    public GameH2A_SO gameData;
    public GameH2A_SO[] gameDataArray;

    public GameObject lineParent;
    public LineRenderer linePrefab;
    public Ball ballPrefab;

    public Transform[] holderTransforms;        //所有的位置(7个)

    private void Start()
    {
        DrawLine();
        CreateBall();
    }
    private void OnEnable()
    {
        EventHandler.CheckGameStateEvent += OnCheckGameStateEvent;
    }

    private void OnDisable()
    {
        EventHandler.CheckGameStateEvent -= OnCheckGameStateEvent;
    }

    /// <summary>
    /// 重置游戏
    /// </summary>
    public void ResetGame()
    {
        foreach (var holder in holderTransforms)
        {
            if (holder.childCount > 0)
            {
                Destroy(holder.GetChild(0).gameObject);
            }
        }
        CreateBall();
    }

    private void OnCheckGameStateEvent()
    {
        foreach (var ball in FindObjectsOfType<Ball>())
        {
            if (!ball.isMatch)
                return;
        }
        //游戏结束
        Debug.LogWarning("游戏结束");
        foreach (var holder in holderTransforms)
        {
            holder.GetComponent<Collider2D>().enabled = false;        //防止游戏结束时快速点击产生的问题
        }

        EventHandler.CallGamePassEvent(gameData.gameName);//添加对应miniGame的通关状态到字典中(true)
        OnFinish?.Invoke();//自动返回场景H2
    }

    /// <summary>
    /// 根据配置好的球的线段连接关系(lineConections)和存储好的位置，绘制连线
    /// </summary>
    private void DrawLine()
    {
        foreach (var conections in gameData.lineConections)
        {
            var line = Instantiate(linePrefab, lineParent.transform);
            line.SetPosition(0, holderTransforms[conections.from].position);
            line.SetPosition(1, holderTransforms[conections.to].position);

            //创建每个Holder的连接关系
            holderTransforms[conections.from].GetComponent<Holder>().linkHolders.Add(holderTransforms[conections.to].GetComponent<Holder>());
            holderTransforms[conections.to].GetComponent<Holder>().linkHolders.Add(holderTransforms[conections.from].GetComponent<Holder>());

        }
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

            holderTransforms[i].GetComponent<Holder>().CheckBall(ball);
            holderTransforms[i].GetComponent<Holder>().isEmpty = false;
            ball.SetupBall(gameData.GetBallDetails(gameData.startBallOrder[i]));
        }
    }

    /// <summary>
    /// 设置对应周目的Mini游戏数据
    /// </summary>
    /// <param name="week"></param>
    public void SetGameWeekData(int week)
    {
        gameData = gameDataArray[week];
        DrawLine();
        CreateBall();
    }
}
