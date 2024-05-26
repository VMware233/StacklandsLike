using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Containers
{
    public static class ContainerItemUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryMergeWith(this IContainerItem item, IContainerItem other)
        {
            if (item.IsMergeableWith(other))
            {
                item.MergeWith(other);

                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TrySplit(this IContainerItem item, int targetCount, out IContainerItem result)
        {
            result = null;

            if (item.IsSplittable(targetCount))
            {
                result = item.Split(targetCount);

                return true;
            }

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetContainerAndSlotIndex(this IContainerItem item, out IContainer container,
            out int slotIndex)
        {
            container = item.sourceContainer;
            
            if (container == null)
            {
                Debug.LogError("Item does not have a source container.");
                slotIndex = -1;
                return false;
            }

            if (container.TryGetSlotIndex(item, out slotIndex) == false)
            {
                Debug.LogError("Item does not exist in the source container.");
                return false;
            }
            
            return true;
        }
    }
}