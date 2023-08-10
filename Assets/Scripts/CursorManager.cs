using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class CursorManager : Singleton<CursorManager>
{
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

    private bool canClick = false;

    private void Update()
    {
        canClick = ObjectAtMousePosition();
        if (canClick && Input.GetMouseButtonDown(0))
        {
            ClickAction(ObjectAtMousePosition().gameObject);
        }
    }

    /// <summary>
    /// 检测物体标签 判断do something
    /// </summary>
    /// <param name="clickObj"></param>
    public void ClickAction(GameObject clickObj)
    {
        switch (clickObj.tag)
        {
            case "Teleport":
                var target = clickObj.GetComponent<Teleport>();
                target?.TeleportScene();
                break;
            case "xxx":

                break;
        }
    }

    /// <summary>
    /// 检测与鼠标重叠的碰撞体
    /// </summary>
    /// <returns></returns>
    public Collider2D ObjectAtMousePosition()
    {
        //OverlapPoint 返回与鼠标点击位置重叠的Physics2D(物体)的信息
        return Physics2D.OverlapPoint(mouseWorldPos);
    }

}
