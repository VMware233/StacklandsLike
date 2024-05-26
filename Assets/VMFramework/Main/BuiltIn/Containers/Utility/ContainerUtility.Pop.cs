using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Containers
{
    public partial class ContainerUtility
    {
        #region Try Pop Item By Slot Index

        /// <summary>
        /// 尝试弹出槽位上的物品，如果槽位无效或者物品为null，则返回false，其他情况返回true
        /// </summary>
        /// <param name="container"></param>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool TryPopItemBySlotIndex(this IContainer container, int index, out IContainerItem item)
        {
            if (container.TryGetItem(index, out item) == false)
            {
                return false;
            }

            if (item == null)
            {
                return false;
            }

            container.SetItem(index, null);

            return true;
        }

        /// <summary>
        /// 尝试弹出槽位上的物品到指定的容器，如果槽位无效或者物品为null，返回True
        /// 如果目标容器无法完全接收物品，则返回false，其他情况返回True
        /// </summary>
        /// <param name="container"></param>
        /// <param name="slotIndex"></param>
        /// <param name="targetContainer"></param>
        /// <returns></returns>
        public static bool TryPopItemBySlotIndexTo(this IContainer container, int slotIndex,
            IContainer targetContainer)
        {
            if (container.TryGetItem(slotIndex, out var item) == false)
            {
                return true;
            }

            if (item == null)
            {
                return true;
            }

            if (targetContainer.TryAddItem(item) == false)
            {
                return false;
            }

            container.SetItem(slotIndex, null);

            return true;
        }

        /// <summary>
        /// 尝试弹出槽位上的物品到指定的容器的指定槽位，如果槽位无效或者物品为null，返回True
        /// 如果目标容器无法完全接收物品，则返回false，其他情况返回True
        /// </summary>
        /// <param name="container"></param>
        /// <param name="slotIndex"></param>
        /// <param name="targetContainer"></param>
        /// <param name="targetIndex"></param>
        /// <returns></returns>
        public static bool TryPopItemBySlotIndexTo(this IContainer container, int slotIndex,
            IContainer targetContainer, int targetIndex)
        {
            if (container.TryGetItem(slotIndex, out var item) == false)
            {
                return true;
            }

            if (item == null)
            {
                return true;
            }

            if (targetContainer.TryAddItem(targetIndex, item) == false)
            {
                return false;
            }

            container.SetItem(slotIndex, null);

            return true;
        }

        #endregion

        #region Try Pop First Valid Item

        /// <summary>
        /// 尝试弹出第一个有效的物品，如果没有有效物品，则返回false，其他情况返回true
        /// </summary>
        /// <param name="container"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryPopFirstValidItem(this IContainer container, out IContainerItem item)
        {
            if (container.TryGetFirstSlotIndex(out var slotIndex) == false)
            {
                item = null;
                return false;
            }
            
            return container.TryPopItemBySlotIndex(slotIndex, out item);
        }

        /// <summary>
        /// 尝试弹出第一个有效的物品到指定的容器，如果没有有效物品，则返回false，
        /// 若目标容器无法完全接收物品，则返回false，其他情况返回true
        /// </summary>
        /// <param name="container"></param>
        /// <param name="targetContainer"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryPopFirstValidItemTo(this IContainer container, IContainer targetContainer)
        {
            if (container.TryGetFirstSlotIndex(out var slotIndex) == false)
            {
                return false;
            }

            if (container.TryPopItemBySlotIndexTo(slotIndex, targetContainer) == false)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Try Pop Item By Preferred Count

        public static bool TryPopItemByPreferredCount(this IContainer container, int index, int preferredCount,
            out IContainerItem item)
        {
            if (container.TryGetItem(index, out var currentItem) == false)
            {
                item = null;
                return false;
            }

            if (currentItem == null)
            {
                item = null;
                return false;
            }

            if (currentItem.count <= preferredCount)
            {
                container.SetItem(index, null);
                item = currentItem;
                return true;
            }

            var splitResult = currentItem.Split(preferredCount);

            item = splitResult;
            return true;
        }

        #endregion

        public static void PopItemsTo(this IContainer container, int count, IContainer targetContainer,
            out int remainingCount)
        {
            remainingCount = count;

            while (count > 0)
            {
                if (container.TryPopItemByPreferredCount(count, out var item, out _) == false)
                {
                    if (container.isDebugging)
                    {
                        Debug.LogWarning($"{container}无法Pop出物品");
                    }

                    return;
                }

                var itemCount = item.count;

                if (targetContainer.TryAddItem(item) == false)
                {
                    if (container.isDebugging)
                    {
                        Debug.LogWarning($"{targetContainer}已满，无法继续添加物品");
                    }

                    if (container.TryAddItem(item) == false)
                    {
                        throw new Exception($"{container}在添加{item}时发生异常");
                    }

                    return;
                }

                count -= itemCount;
                remainingCount = count;
            }
        }

        public static void PopAllItemsTo(this IContainer container, IContainer targetContainer)
        {
            var validSlotIndices = container.validSlotIndices.ToArray();
            foreach (var slotIndex in validSlotIndices)
            {
                container.TryPopItemBySlotIndexTo(slotIndex, targetContainer);
            }
        }
    }
}