using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public BallDetails ballDetails;

    public bool isMatch;        //是否匹配

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    ///设置球的图片
    /// </summary>
    /// <param name="ball"></param>
    public void SetupBall(BallDetails ball)
    {
        ballDetails = ball;

        if (isMatch)
            SetRight();
        else
            SetWrong();
    }

    /// <summary>
    /// 设置错误图片
    /// </summary>
    public void SetWrong()
    {
        spriteRenderer.sprite = ballDetails.wrongSprite;
    }

    /// <summary>
    /// 设置正确图片
    /// </summary>
    public void SetRight()
    {
        spriteRenderer.sprite = ballDetails.rightSprite;
    }
}
