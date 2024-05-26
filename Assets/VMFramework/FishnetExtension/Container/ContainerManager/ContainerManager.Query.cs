#if FISHNET
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Network;

namespace VMFramework.Containers
{
    public partial class ContainerManager
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetItem(string containerUUID, int slotIndex, out IContainerItem item)
        {
            if (UUIDCoreManager.TryGetOwner(containerUUID, out IContainer container) == false)
            {
                Debug.LogError($"Container with UUID {containerUUID} does not exist.");
                item = null;
                return false;
            }

            if (container.TryGetItem(slotIndex, out item) == false)
            {
                Debug.LogError($"{slotIndex} is not a valid slot index for container {containerUUID}.");
                
                return false;
            }
            
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetItem<TContainerItem>(string containerUUID, int slotIndex,
            out TContainerItem item) where TContainerItem : IContainerItem
        {
            if (TryGetItem(containerUUID, slotIndex, out var containerItem) == false)
            {
                item = default;
                return false;
            }
            
            if (containerItem is not TContainerItem typedItem)
            {
                item = default;
                return false;
            }
            
            item = typedItem;
            return true;
        }
    }
}
#endif