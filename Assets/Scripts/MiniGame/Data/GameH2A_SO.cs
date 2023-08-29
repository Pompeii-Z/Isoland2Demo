using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameH2A_SO", menuName = "Mini Game/GameH2A_SO")]
public class GameH2A_SO : ScriptableObject
{
    [Header("球的名字和对应的图片")]
    public List<BallDetails> ballDetails;

    [Header("游戏逻辑数据")]
    public List<Conections> lineConections;     //位置的对应关系
    public List<BallName> startBallOrder;       //开始时的球顺序

    /// <summary>
    /// 获取球的信息
    /// </summary>
    /// <param name="ballName"></param>
    /// <returns></returns>
    public BallDetails GetBallDetails(BallName ballName)
    {
        return ballDetails.Find(b => b.ballName == ballName);
    }
}

[System.Serializable]
public class BallDetails
{
    public BallName ballName;
    public Sprite wrongSprite;
    public Sprite rightSprite;
}

[System.Serializable]
public class Conections
{
    public int from;
    public int to;
}