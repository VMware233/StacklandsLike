using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Procedure
{
    /// <summary>
    /// 非线程安全的管理器基类，用于实现单例。
    /// Non-thread-safe manager base class used for singleton implementation.
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    public class ManagerBehaviour<TInstance> : MonoBehaviour, IManagerBehaviour
        where TInstance : ManagerBehaviour<TInstance>
    {
        [ShowInInspector]
        [HideInEditorMode]
        protected static TInstance instance;

        protected virtual void OnBeforeInit()
        {
            instance = (TInstance)this;
            instance.AssertIsNotNull(nameof(instance));
        }

        void IInitializer.OnBeforeInit(Action onDone)
        {
            OnBeforeInit();
            onDone();
        }
    }
}
