using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    public static partial class ContainerUtility
    {
        #region Add Item

        public static void AddItem(this IContainer container, IContainerItem item)
        {
            container.TryAddItem(item);
        }
        
        public static bool TryAddItem(this IContainer container, IContainerItem item, IKCube<int> range)
        {
            return container.TryAddItem(item, range.min, range.max);
        }

        /// <summary>
        /// 尝试将item添加到index槽位，如果item完全添加到槽位（指item的剩余count小于等于0），
        /// 则返回true，否则返回false
        /// </summary>
        /// <param name="container"></param>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool TryAddItem(this IContainer container, int index, IContainerItem item)
        {
            if (item == null)
            {
                return true;
            }

            if (item.count <= 0)
            {
                return true;
            }

            if (container.TryGetItem(index, out var itemInContainer) == false)
            {
                Debug.LogWarning($"{nameof(index)}:{index}无效");
                return false;
            }

            if (itemInContainer == null)
            {
                container.SetItem(index, item);

                if (container.isDebugging)
                {
                    Debug.LogWarning($"{item}被添加到{container}第{index}个槽位");
                }

                return true;
            }

            if (container.TryMergeItem(index, item))
            {
                if (container.isDebugging)
                {
                    Debug.LogWarning($"{item}被添加到{container}第{index}个槽位");
                }

                if (item.count <= 0)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
        
        #region Swap Item

        /// <summary>
        /// 和另一个容器的某个槽位交换两个槽位中的物品
        /// </summary>
        /// <param name="container"></param>
        /// <param name="index"></param>
        /// <param name="targetContainer"></param>
        /// <param name="targetIndex"></param>
        public static void SwapItem(this IContainer container, int index, IContainer targetContainer,
            int targetIndex)
        {
            if (container.TryGetItem(index, out var thisItem) == false)
            {
                Debug.LogWarning($"{nameof(index)}:{index}无效");
                return;
            }

            if (targetContainer.TryGetItem(targetIndex, out var targetItem) == false)
            {
                Debug.LogWarning($"{nameof(targetIndex)}:{targetIndex}无效");
                return;
            }

            if (thisItem == null && targetItem == null)
            {
                Debug.LogWarning("交换的双方IContainerItem都是Null");
                return;
            }

            container.SetItem(index, targetItem);
            targetContainer.SetItem(targetIndex, thisItem);

            if (container.isDebugging == false)
            {
                return;
            }

            if (container != targetContainer)
            {
                Debug.LogWarning($"{container}的第{index}个物品和{targetContainer}的第{targetIndex}个物品发生了交换");
            }
            else
            {
                Debug.LogWarning($"{container}的第{index}个物品和第{targetIndex}个物品发生了交换");
            }
        }

        #endregion
        
        #region Add Or Swap Item

        /// <summary>
        /// 将此容器的某个槽位中的物品添加到另一个容器的某个槽位中，如果目标槽位已经有物品，则交换两个槽位中的物品
        /// </summary>
        /// <param name="container"></param>
        /// <param name="index"></param>
        /// <param name="targetContainer"></param>
        /// <param name="targetIndex"></param>
        public static void AddOrSwapItemTo(this IContainer container, int index, IContainer targetContainer,
            int targetIndex)
        {
            if (targetContainer.TryGetItem(targetIndex, out var targetItem) == false)
            {
                Debug.LogWarning($"{nameof(targetIndex)}:{targetIndex}无效");
                return;
            }

            if (targetItem == null)
            {
                container.SwapItem(index, targetContainer, targetIndex);
                return;
            }

            if (container.TryGetItem(index, out var thisItem) == false)
            {
                Debug.LogWarning($"{nameof(index)}:{index}无效");
                return;
            }

            if (thisItem == null)
            {
                container.SwapItem(index, targetContainer, targetIndex);
                return;
            }

            if (targetContainer.TryAddItem(targetIndex, thisItem) == false)
            {
                container.SwapItem(index, targetContainer, targetIndex);
            }
        }

        #endregion
        
        #region Split

        public static bool TrySplitItemTo(this IContainer container, int slotIndex, int count,
            IContainer targetContainer, int targetSlotIndex)
        {
            var item = container.GetItem(slotIndex);

            if (item.IsSplittable(count))
            {
                var targetItem = targetContainer.GetItem(targetSlotIndex);

                var splitResult = item.Split(count);

                if (targetItem == null)
                {
                    targetContainer.SetItem(targetSlotIndex, splitResult);

                    return true;
                }

                if (targetItem.IsMergeableWith(splitResult))
                {
                    targetItem.MergeWith(splitResult);
                    item.MergeWith(splitResult);

                    return true;
                }

                item.MergeWith(splitResult);
            }

            return false;
        }

        #endregion
        
        #region Remove

        #region By Item ID

        /// <summary>
        /// 尝试移除希望数量的一些物品，如果所有物品完全移除，则返回true，否则返回false
        /// </summary>
        /// <param name="container"></param>
        /// <param name="consumptions"></param>
        /// <param name="removedCount">被移除的物品数量</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRemoveItems(this IContainer container,
            IReadOnlyDictionary<string, int> consumptions,
            out Dictionary<string, int> removedCount)
        {
            return container.TryRemoveItems(consumptions, int.MinValue, int.MaxValue,
                out removedCount);
        }

        /// <summary>
        /// 在限定的槽位序号里，尝试移除希望数量的一些物品，如果所有物品完全移除，则返回true，否则返回false
        /// </summary>
        /// <param name="container"></param>
        /// <param name="consumptions"></param>
        /// <param name="startSlotIndex"></param>
        /// <param name="endSlotIndex"></param>
        /// <param name="removedCount"></param>
        /// <returns></returns>
        public static bool TryRemoveItems(this IContainer container,
            IReadOnlyDictionary<string, int> consumptions,
            int startSlotIndex, int endSlotIndex,
            out Dictionary<string, int> removedCount)
        {
            var result = true;

            removedCount = new Dictionary<string, int>();
            foreach (var (itemID, preferredCount) in consumptions)
            {
                result &= container.TryRemoveItems(itemID, preferredCount, startSlotIndex,
                    endSlotIndex, out var itemRemovedCount);

                removedCount.TryAdd(itemID, 0);

                removedCount[itemID] += itemRemovedCount;
            }

            return result;
        }

        /// <summary>
        /// 在限定的槽位序号里，尝试移除希望数量的物品，如果完全移除，则返回true，否则返回false
        /// </summary>
        /// <param name="container"></param>
        /// <param name="itemID"></param>
        /// <param name="preferredCount"></param>
        /// <param name="startSlotIndex">限定槽位序号起始</param>
        /// <param name="endSlotIndex">限定槽位序号结束</param>
        /// <param name="removedCount">实际被移除的物品数量</param>
        /// <returns></returns>
        public static bool TryRemoveItems(this IContainer container, string itemID, int preferredCount,
            int startSlotIndex, int endSlotIndex, out int removedCount)
        {
            removedCount = 0;

            if (preferredCount <= 0)
            {
                Debug.LogWarning("试图移除数量小于等于0的物品");
                return true;
            }

            if (itemID.IsNullOrEmpty())
            {
                Debug.LogWarning("试图移除空的物品ID");
                return true;
            }

            foreach (var validSlotIndex in container.validSlotIndices)
            {
                if (validSlotIndex < startSlotIndex || validSlotIndex > endSlotIndex)
                {
                    continue;
                }

                var item = container.GetItem(validSlotIndex);

                if (item.id == itemID)
                {
                    var maxRemovedCount = preferredCount - removedCount;
                    if (item.count >= maxRemovedCount)
                    {
                        item.count -= maxRemovedCount;
                        removedCount = preferredCount;
                        return true;
                    }

                    container.SetItem(validSlotIndex, null);
                    removedCount += item.count;
                }
            }

            return false;
        }

        /// <summary>
        /// 尝试移除希望数量的物品，如果完全移除，则返回true，否则返回false
        /// </summary>
        /// <param name="container"></param>
        /// <param name="itemID"></param>
        /// <param name="preferredCount"></param>
        /// <param name="removedCount"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRemoveItems(this IContainer container, string itemID, int preferredCount,
            out int removedCount)
        {
            return container.TryRemoveItems(itemID, preferredCount, int.MinValue,
                int.MaxValue, out removedCount);
        }

        #endregion

        #region By Item

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRemoveItem(this IContainer container, IContainerItem item, int preferredCount)
        {
            return container.TryRemoveItem(item, preferredCount, out _, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryRemoveItem(this IContainer container, IContainerItem item, int preferredCount,
            out int removedCount)
        {
            return container.TryRemoveItem(item, preferredCount, out removedCount, out _);
        }

        /// <summary>
        /// 尝试移除希望数量的物品，如果完全移除，则返回true，否则返回false，
        /// 如果item为null或item没找到，则changedSlotIndex为-1
        /// </summary>
        /// <param name="container"></param>
        /// <param name="item"></param>
        /// <param name="preferredCount"></param>
        /// <param name="removedCount"></param>
        /// <param name="changedSlotIndex"></param>
        /// <returns></returns>
        public static bool TryRemoveItem(this IContainer container, IContainerItem item, int preferredCount,
            out int removedCount, out int changedSlotIndex)
        {
            removedCount = 0;
            changedSlotIndex = -1;

            if (item == null)
            {
                return true;
            }

            if (container.TryGetSlotIndex(item, out changedSlotIndex) == false)
            {
                return false;
            }

            removedCount = item.count.Min(preferredCount);
            item.count -= removedCount;

            return removedCount == preferredCount;
        }

        #endregion

        #endregion

        #region Stack Items

        /// <summary>
        /// 堆叠物品，将相同物品的数量合并到一起
        /// </summary>
        /// <param name="container"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        public static void StackItems(this IContainer container, int startIndex, int endIndex)
        {
            startIndex = startIndex.ClampMin(0);
            endIndex = endIndex.ClampMax(container.size - 1);
            
            container.GetRangeItems(startIndex, endIndex).BuildIDDictionary(out var itemsByID);

            foreach (var (_, items) in itemsByID)
            {
                int totalCount = items.Sum(item => item.count);

                if (totalCount <= 1)
                {
                    continue;
                }

                int maxStackCount = items[0].maxStackCount;

                if (maxStackCount <= 1)
                {
                    continue;
                }

                int validItemNum = totalCount / maxStackCount;
                int leftCount = totalCount % maxStackCount;

                for (int i = 0; i < items.Count; i++)
                {
                    if (i < validItemNum)
                    {
                        items[i].count = maxStackCount;
                    }
                    else if (i == validItemNum)
                    {
                        items[i].count = leftCount;
                    }
                    else
                    {
                        items[i].count = 0;
                    }
                }
            }
        }

        #endregion
    }
}
