using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    /// <summary>
    /// 存储Item的显隐状态
    /// </summary>
    private Dictionary<ItemName, bool> itemAvailableDict = new Dictionary<ItemName, bool>();

    /// <summary>
    /// Item的互动状态
    /// </summary>
    private Dictionary<string, bool> interactiveStateDict = new Dictionary<string, bool>();

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnLoadEvent += OnBeforeSceneLoadedEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.UpdateItemStatusEvent += OnUpdateItemStatusEvent;
    }

    /// <summary>
    /// 进入新场景前：更新当前场景中的Item的状态(可能有新的Item出现)
    /// </summary>
    private void OnBeforeSceneLoadedEvent()
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
            {
                itemAvailableDict.Add(item.itemName, true);
            }
        }

        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
                interactiveStateDict[item.name] = item.isDone;
            else
                interactiveStateDict.Add(item.name, item.isDone);
        }
    }

    /// <summary>
    /// 进入新场景后：第一次记录Item状态，第X次根据状态信息控制显隐
    /// </summary>
    private void OnAfterSceneLoadedEvent()
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            //不在就添加，存在则更新状态
            if (!itemAvailableDict.ContainsKey(item.itemName))
            {
                itemAvailableDict.Add(item.itemName, true);
            }
            else//第X次
            {
                item.gameObject.SetActive(itemAvailableDict[item.itemName]);
            }
        }

        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
                item.isDone = interactiveStateDict[item.name];
            else
                interactiveStateDict.Add(item.name, item.isDone);
        }

    }

    /// <summary>
    /// 拾取Item后，更新Item的激活状态为false
    /// </summary>
    /// <param name="itemDetails"></param>
    /// <param name="arg2"></param>
    private void OnUpdateItemStatusEvent(ItemDetails itemDetails)
    {
        if (itemDetails != null)
        {
            itemAvailableDict[itemDetails.itemName] = false;
        }
    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnLoadEvent -= OnBeforeSceneLoadedEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.UpdateItemStatusEvent -= OnUpdateItemStatusEvent;
    }

}
