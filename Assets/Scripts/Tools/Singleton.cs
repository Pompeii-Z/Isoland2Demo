using UnityEngine;

//泛型单例
//where 约束
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance
    {
        get { return instance; }
    }

    //只允许继承的类访问
    protected virtual void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = (T)this;//继承的任意类型
    }

    /// <summary>
    /// 静态属性，判断"单例"是否已经初始化
    /// </summary>
    /// <value></value>
    public static bool IsInitialized
    {
        get { return instance != null; }
    }

    //有多个单例时需要设置，被销毁时设置instance为null
    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
