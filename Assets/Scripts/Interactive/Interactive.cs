using UnityEngine;
public class Interactive : MonoBehaviour
{
    public ItemName requireItem;
    public bool isDone;

    public void CheckItem(ItemName itemName)
    {
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
