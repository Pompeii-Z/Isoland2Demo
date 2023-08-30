using UnityEngine;

/// <summary>
/// 基类 可交互的道具
/// </summary>
public class Interactive : MonoBehaviour
{
    public ItemName requireItem;    //需要的道具
    public bool isDone;             //是否使用

    /// <summary>
    /// 检查Item 是不是需要的道具
    /// </summary>
    /// <param name="itemName"></param>
    public void CheckItem(ItemName itemName)
    {
        //是否为正确的物品
        if (itemName == requireItem && !isDone)
        {
            isDone = true;
            //使用物品后，移除物品
            OnClickedAction();
            EventHandler.CallItemUsedEvent(itemName);
        }
    }

    /// <summary>
    /// 默认为正确的Item执行
    /// </summary>
    protected virtual void OnClickedAction()
    {

    }

    /// <summary>
    /// 空点
    /// </summary>
    public virtual void EmptyAction()
    {
        Debug.Log("空点");
    }

}
