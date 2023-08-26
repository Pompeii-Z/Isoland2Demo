using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    protected override void OnClickedAction()
    {
        spriteRenderer.sprite = openSprite;
        transform.GetChild(0).gameObject.SetActive(true);
    }

}
