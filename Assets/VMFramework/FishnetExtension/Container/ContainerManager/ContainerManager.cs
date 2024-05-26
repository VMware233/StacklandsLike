#if FISHNET
using FishNet.Object;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FishNet.Connection;
using UnityEngine;
using VMFramework.Network;
using VMFramework.Procedure;

namespace VMFramework.Containers
{
    [ManagerCreationProvider(ManagerType.NetworkCore)]
    public partial class ContainerManager : UUIDManager<ContainerManager, IContainer>
    {
        [SerializeField]
        protected bool isDebugging;

        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();

            OnRegisterEvent += OnRegister;
            OnUnregisterEvent += OnUnregister;
        }

        #region Register & Unregister

        private static void OnRegister(IContainer container)
        {
            if (_instance.IsServerStarted)
            {
                container.OnItemCountChangedEvent += OnContainerItemCountChanged;
                container.OnItemAddedEvent += OnItemAdded;
                container.OnItemRemovedEvent += OnItemRemoved;
                container.OnObservedEvent += OnObserved;
                container.OnUnobservedEvent += OnUnobserved;
            }
        }
        
        private static void OnUnregister(IContainer container)
        {
            if (_instance.IsServerStarted)
            {
                container.OnItemCountChangedEvent -= OnContainerItemCountChanged;
                container.OnItemAddedEvent -= OnItemAdded;
                container.OnItemRemovedEvent -= OnItemRemoved;
                container.OnObservedEvent -= OnObserved;
                container.OnUnobservedEvent -= OnUnobserved;
                
                RemoveContainerDirtySlotsInfo(container);
            }
        }

        #endregion

        #region Observe & Unobserve

        private static void OnObserved(IUUIDOwner container, bool isDirty, NetworkConnection connection)
        {
            if (UUIDCoreManager.TryGetInfo(container.uuid, out var info))
            {
                if (info.observers.Count == 0)
                {
                    ((IContainer)container).OpenOnServer();
                }
            }
            else
            {
                Debug.LogWarning($"不存在此{container.uuid}对应的{nameof(UUIDInfo)}");
            }
            
            if (isDirty)
            {
                ReconcileAllOnTarget(connection, (IContainer)container);
            }
        }

        private static void OnUnobserved(IUUIDOwner container, NetworkConnection connection)
        {
            if (UUIDCoreManager.TryGetInfo(container.uuid, out var info))
            {
                if (info.observers.Count <= 0)
                {
                    ((IContainer)container).CloseOnServer();
                }
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{container.uuid}对应的{nameof(UUIDInfo)}");
            }
        }

        #endregion

        #region Set Dirty

        [ObserversRpc(ExcludeServer = true)]
        private void SetDirty(string containerUUID)
        {
            if (UUIDCoreManager.TryGetOwner(containerUUID, out IContainer container))
            {
                if (container.isOpen == false)
                {
                    container.isDirty = true;
                }
            }
        }

        #endregion
    }
}

#endif