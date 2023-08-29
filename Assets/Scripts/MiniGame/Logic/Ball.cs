using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public BallDetails ballDetails;

    public bool isMatch;

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

    private void SetWrong()
    {
        spriteRenderer.sprite = ballDetails.wrongSprite;
    }

    private void SetRight()
    {
        spriteRenderer.sprite = ballDetails.rightSprite;
    }
}
