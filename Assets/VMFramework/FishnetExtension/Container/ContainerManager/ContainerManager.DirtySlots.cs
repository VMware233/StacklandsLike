#if FISHNET
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Network;

namespace VMFramework.Containers
{
    public partial class ContainerManager
    {
        private static readonly Dictionary<IContainer, HashSet<int>> allDirtySlots = new();
        
        private static readonly List<IContainer> dirtyContainersRemovalList = new();

        private static void RemoveContainerDirtySlotsInfo(IContainer container)
        {
            allDirtySlots.Remove(container);
        }
        
        #region Container Changed

        private void Update()
        {
            if (IsServerStarted == false)
            {
                return;
            }

            var isHost = IsHostStarted;
            var hostClientID = isHost ? ClientManager.Connection.ClientId : -1;

            foreach (var (container, dirtySlots) in allDirtySlots)
            {
                if (dirtySlots.Count == 0)
                {
                    continue;
                }

                if (UUIDCoreManager.TryGetInfo(container.uuid, out var info) == false)
                {
                    Debug.LogWarning($"Container with UUID {container.uuid} not found.");
                    dirtyContainersRemovalList.Add(container);
                    continue;
                }

                if (info.observers.Count == 0)
                {
                    dirtySlots.Clear();
                    continue;
                }

                if (info.observers.Count == 1 && info.observers.Contains(hostClientID))
                {
                    dirtySlots.Clear();
                    continue;
                }

                if (dirtySlots.Count == 1)
                {
                    var slotIndex = dirtySlots.First();
                    ReconcileItemOnObservers(container, info, slotIndex);
                }
                else
                {
                    var ratio = dirtySlots.Count / (float)container.size;

                    if (ratio > 0.5f)
                    {
                        ReconcileAllItemsOnObservers(container, info);
                    }
                    else
                    {
                        ReconcileSomeItemsOnObservers(container, info, dirtySlots);
                    }
                }

                SetDirty(container.uuid);

                dirtySlots.Clear();
            }

            if (dirtyContainersRemovalList.Count > 0)
            {
                foreach (var container in dirtyContainersRemovalList)
                {
                    RemoveContainerDirtySlotsInfo(container);
                }
            }
        }

        private static void OnContainerItemCountChanged(IContainer container, 
            int slotIndex, IContainerItem item, int previous, int current)
        {
            if (current != previous)
            {
                SetSlotDirty(container, slotIndex);
            }
        }

        private static void OnItemRemoved(IContainer container, int slotIndex, 
            IContainerItem item)
        {
            SetSlotDirty(container, slotIndex);
        }

        private static void OnItemAdded(IContainer container, int slotIndex, 
            IContainerItem item)
        {
            SetSlotDirty(container, slotIndex);
        }

        #endregion

        #region Set Slot Dirty

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSlotDirty(IContainer container, int slotIndex)
        {
            if (container.uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"{container}'s UUID is null or empty.");
                return;
            }
            
            if (allDirtySlots.TryGetValue(container, out var dirtySlots) == false)
            {
                dirtySlots = new HashSet<int>();
                allDirtySlots.Add(container, dirtySlots);
            }
            
            dirtySlots.Add(slotIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSlotDirty(IContainerItem item)
        {
            var container = item.sourceContainer;
            if (container.TryGetSlotIndex(item, out var slotIndex))
            {
                SetSlotDirty(container, slotIndex);
            }
        }

        #endregion
    }
}
#endif