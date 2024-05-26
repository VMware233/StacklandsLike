#if FISHNET
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FishNet;
using FishNet.Connection;
using FishNet.Object;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Network;

namespace VMFramework.Containers
{
    public partial class ContainerManager
    {
        #region Reconcile Item On Observers

        private static void ReconcileItemOnObservers(IContainer container, UUIDInfo info, int slotIndex)
        {
            foreach (var observer in info.observers)
            {
                var observerConn = InstanceFinder.ServerManager.Clients[observer];

                if (observerConn.IsHost)
                {
                    continue;
                }

                if (instance.isDebugging)
                {
                    Debug.LogWarning($"准备Reconcile客户端：{observer}");
                }

                instance.ReconcileOnTarget(observerConn, container.uuid, slotIndex,
                    container.GetItem(slotIndex));
            }
        }

        #endregion

        #region Reconcile Some Items On Observers

        private static void ReconcileSomeItemsOnObservers(IContainer container, UUIDInfo info, HashSet<int> slotIndices)
        {
            var items = new Dictionary<int, IContainerItem>();

            foreach (var slotIndex in slotIndices)
            {
                items.Add(slotIndex, container.GetItem(slotIndex));
            }

            foreach (var observer in info.observers)
            {
                var observerConn = InstanceFinder.ServerManager.Clients[observer];

                if (observerConn.IsHost)
                {
                    continue;
                }

                if (instance.isDebugging)
                {
                    Debug.LogWarning($"准备Reconcile客户端：{observer}");
                }

                instance.ReconcileSomeOnTarget(observerConn, container.uuid, items);
            }
        }

        #endregion
        
        #region Reconcile On Observers

        private static void ReconcileAllItemsOnObservers(IContainer container, UUIDInfo containerInfo)
        {
            foreach (var observer in containerInfo.observers)
            {
                var observerConn = InstanceFinder.ServerManager.Clients[observer];

                if (observerConn.IsHost)
                {
                    continue;
                }

                if (instance.isDebugging)
                {
                    Debug.LogWarning($"准备Reconcile客户端：{observer}");
                }

                instance.ReconcileAllOnTarget(observerConn, containerInfo.owner.uuid,
                    container.GetItemArray());
            }
        }

        #endregion
        
        #region Reconcile On Target

        [TargetRpc(ExcludeServer = true)]
        [ObserversRpc(ExcludeServer = true)]
        private void ReconcileOnTarget(NetworkConnection connection, string containerUUID,
            int slotIndex, IContainerItem item)
        {
            if (isDebugging)
            {
                Debug.LogWarning($"正在恢复{containerUUID}的第{slotIndex}个物品，恢复为：{item}");
            }

            if (UUIDCoreManager.TryGetOwner(containerUUID, out IContainer container))
            {
                container.SetItem(slotIndex, item);
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{nameof(containerUUID)}:{containerUUID}对应的{nameof(container)}");
            }
        }

        [TargetRpc(ExcludeServer = true)]
        [ObserversRpc(ExcludeServer = true)]
        private void ReconcileSomeOnTarget(NetworkConnection connection, string containerUUID,
            Dictionary<int, IContainerItem> items)
        {
            if (UUIDCoreManager.TryGetOwner(containerUUID, out IContainer container))
            {
                foreach (var (slotIndex, item) in items)
                {
                    container.SetItem(slotIndex, item);
                }
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{nameof(containerUUID)}:{containerUUID}对应的{nameof(container)}");
            }
        }

        [TargetRpc(ExcludeServer = true)]
        [ObserversRpc(ExcludeServer = true)]
        private void ReconcileAllOnTarget(NetworkConnection connection, string containerUUID,
            IContainerItem[] items)
        {
            if (UUIDCoreManager.TryGetOwner(containerUUID, out IContainer container))
            {
                container.LoadFromItemArray(items);
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{nameof(containerUUID)}:{containerUUID}对应的{nameof(container)}");
            }
        }

        private static void ReconcileAllOnTarget(NetworkConnection connection, IContainer container)
        {
            instance.ReconcileAllOnTarget(connection, container.uuid, container.GetItemArray());
        }

        #endregion
        
        #region Request Reconcile

        [Client]
        public static void RequestReconcile(IContainer container, int slotIndex)
        {
            container.AssertIsNotNull(nameof(container));

            instance.RequestReconcile(container.uuid, slotIndex);
        }

        [ServerRpc(RequireOwnership = false)]
        private void RequestReconcile(string containerUUID, int slotIndex,
            NetworkConnection connection = null)
        {
            if (UUIDCoreManager.TryGetOwner(containerUUID, out IContainer container))
            {
                var item = container.GetItem(slotIndex);

                ReconcileOnTarget(connection, containerUUID, slotIndex, item);
            }
            else
            {
                Debug.LogWarning(
                    $"不存在此{nameof(containerUUID)}:{containerUUID}对应的{nameof(container)}");
            }
        }

        [Client]
        public static void RequestReconcileAll(IContainer container)
        {
            container.AssertIsNotNull(nameof(container));

            instance.RequestReconcileAll(container.uuid);
        }

        [ServerRpc(RequireOwnership = false)]
        private void RequestReconcileAll(string containerUUID,
            NetworkConnection connection = null)
        {
            if (UUIDCoreManager.TryGetOwner(containerUUID, out IContainer container))
            {
                ReconcileAllOnTarget(connection, containerUUID, container.GetItemArray());
            }
        }

        #endregion
    }
}
#endif