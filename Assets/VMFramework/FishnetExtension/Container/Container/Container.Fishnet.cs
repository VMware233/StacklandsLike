#if FISHNET
using System;
using FishNet.Connection;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Network;

namespace VMFramework.Containers
{
    public partial class Container
    {
        [ShowInInspector]
        public string uuid { get; private set; }
        
        public bool isDirty = true;

        public event Action<IUUIDOwner, bool, NetworkConnection> OnObservedEvent;
        public event Action<IUUIDOwner, NetworkConnection> OnUnobservedEvent;
        
        public event Action<IContainer> OnOpenOnServerEvent;
        public event Action<IContainer> OnCloseOnServerEvent;

        string IUUIDOwner.uuid
        {
            get => uuid;
            set => uuid = value;
        }

        bool IUUIDOwner.isDirty
        {
            get => isDirty;
            set => isDirty = value;
        }

        void IUUIDOwner.OnObserved(bool isDirty, NetworkConnection connection)
        {
            OnObservedEvent?.Invoke(this, isDirty, connection);
        }

        void IUUIDOwner.OnUnobserved(NetworkConnection connection)
        {
            OnUnobservedEvent?.Invoke(this, connection);
        }

        #region Open & Close

        public void OpenOnServer()
        {
            if (isDebugging)
            {
                Debug.LogWarning($"{this}在服务器上打开");
            }

            OnOpenOnServerEvent?.Invoke(this);
        }

        public void CloseOnServer()
        {
            if (isDebugging)
            {
                Debug.LogWarning($"{this}在服务器上关闭");
            }

            OnCloseOnServerEvent?.Invoke(this);
        }

        #endregion
    }
}
#endif