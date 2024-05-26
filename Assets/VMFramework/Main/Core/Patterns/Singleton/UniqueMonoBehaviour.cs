using System;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// 非线程安全的单例MonoBehaviour
/// </summary>
/// <typeparam name="T"></typeparam>
[DisallowMultipleComponent]
public abstract class UniqueMonoBehaviour<T> : SerializedMonoBehaviour where T : UniqueMonoBehaviour<T>
{
    public static T instance;

    protected virtual void Awake()
    {
        if (instance != null)
        {
            throw new Exception($"重复添加组件{nameof(T)}");
        }

        instance = (T)this;
    }

#if UNITY_EDITOR
    public void _DisplaySelf()
    {
        Debug.Log($"{name}");
    }
#endif
}