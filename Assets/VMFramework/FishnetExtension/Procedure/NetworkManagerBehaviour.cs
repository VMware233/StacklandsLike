#if FISHNET
using System;
using FishNet.Object;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Procedure
{
    [RequireComponent(typeof(NetworkObject))]
    public class NetworkManagerBehaviour<TInstance> : NetworkBehaviour, IManagerBehaviour
        where TInstance : NetworkManagerBehaviour<TInstance>
    {
        [ShowInInspector]
        [HideInEditorMode]
        protected static TInstance _instance;

        public static TInstance instance => _instance;

        protected virtual void OnBeforeInit()
        {
            _instance = (TInstance)this;
            _instance.AssertIsNotNull(nameof(_instance));
        }

        void IInitializer.OnBeforeInit(Action onDone)
        {
            OnBeforeInit();
            onDone();
        }
    }
}
#endif
