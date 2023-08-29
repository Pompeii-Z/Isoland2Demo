using UnityEngine;

/// <summary>
/// 信箱 子类
/// </summary>
public class MailBox : Interactive
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;

    public Sprite openSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }

    /// <summary>
    /// 场景加载之后 判断是否已经交互过
    /// </summary>
    private void OnAfterSceneLoadedEvent()
    {
        if (!isDone)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            spriteRenderer.sprite = openSprite;
            collider.enabled = false;
        }
    }

    /// <summary>
    /// 正确的道具执行
    /// </summary>
    protected override void OnClickedAction()
    {
        spriteRenderer.sprite = openSprite;
        transform.GetChild(0).gameObject.SetActive(true);
    }

}
