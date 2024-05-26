using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using VMFramework.Core.Linq;

namespace VMFramework.Containers
{
    public partial class ContainerUtility
    {
        #region Get Item

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TItem GetItem<TItem>(this IContainer container, int index)
        {
            return (TItem) container.GetItem(index);
        }
        
        /// <summary>
        ///     尝试获取TItem，如果index无效或者为null，返回false，否则返回true
        /// </summary>
        /// <param name="container"></param>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetItem<TItem>(this IContainer container, int index, out TItem item)
            where TItem : IContainerItem
        {
            if (container.TryGetItem(index, out var containerItem))
            {
                if (containerItem is TItem typedItem)
                {
                    item = typedItem;
                    return true;
                }
            }

            item = default;
            return false;
        }

        #endregion

        #region Get Items

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TItem> GetAllItems<TItem>(this IContainer container)
            where TItem : IContainerItem
        {
            foreach (var containerItem in container.GetAllItems())
            {
                if (containerItem is TItem item)
                {
                    yield return item;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] GetItemArray<T>(this IContainer container) where T : IContainerItem
        {
            return container.GetItemArray().Cast<T>().ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(int slotIndex, IContainerItem item)> EnumerateAllItems(
            this IContainer container)
        {
            return container.GetAllItems().Enumerate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(int slotIndex, TItem item)> EnumerateAllItems<TItem>(
            this IContainer container) where TItem : IContainerItem
        {
            return container.GetAllItems<TItem>().Enumerate();
        }

        #endregion

        #region Get Range Items

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IContainerItem> GetRangeItems(this IContainer container, IKCube<int> range)
        {
            foreach (var index in range.GetAllPoints())
            {
                yield return container.GetItem(index);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IContainerItem> GetRangeItems(this IContainer container, int start, int end)
        {
            foreach (var index in start.GetAllPointsOfRange(end))
            {
                yield return container.GetItem(index);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IContainerItem> GetRangeValidItems(this IContainer container,
            IKCube<int> range)
        {
            foreach (var index in range.GetAllPoints())
            {
                if (container.validSlotIndices.Contains(index))
                {
                    yield return container.GetItemWithoutCheck(index);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IContainerItem> GetRangeValidItems(this IContainer container, int start,
            int end)
        {
            foreach (var slotIndex in container.validSlotIndices)
            {
                if (slotIndex.BetweenInclusive(start, end))
                {
                    yield return container.GetItemWithoutCheck(slotIndex);
                }
            }
        }

        #endregion

        #region Get All Valid Items

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IContainerItem> GetAllValidItems(this IContainer container)
        {
            return container.validSlotIndices.Select(container.GetItemWithoutCheck);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TItem> GetAllValidItems<TItem>(this IContainer container)
            where TItem : IContainerItem
        {
            foreach (var slotIndex in container.validSlotIndices)
            {
                if (container.GetItemWithoutCheck(slotIndex) is TItem item)
                {
                    yield return item;
                }
            }
        }

        #endregion

        #region Get Slot Index

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetSlotIndex(this IContainer container, IContainerItem item, out int slotIndex)
        {
            foreach (var validSlotIndex in container.validSlotIndices)
            {
                if (container.GetItem(validSlotIndex) == item)
                {
                    slotIndex = validSlotIndex;
                    return true;
                }
            }

            slotIndex = -1;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetSlotIndices(this IContainer container, string itemID,
            out IReadOnlyList<int> slotIndices)
        {
            if (LINQUtility.IsNullOrEmpty(itemID))
            {
                slotIndices = null;
                return false;
            }

            var result = new List<int>();

            foreach (var validSlotIndex in container.validSlotIndices)
            {
                if (container.GetItem(validSlotIndex)?.id == itemID)
                {
                    result.Add(validSlotIndex);
                }
            }

            slotIndices = result;
            return result.Count > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFirstSlotIndex(this IContainer container, string itemID, out int slotIndex)
        {
            if (container.TryGetSlotIndices(itemID, out var slotIndices))
            {
                slotIndex = slotIndices.First();
                return true;
            }

            slotIndex = -1;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFirstSlotIndex(this IContainer container, out int slotIndex)
        {
            if (container.validSlotIndices.Count <= 0)
            {
                slotIndex = -1;
                return false;
            }

            slotIndex = container.validSlotIndices.Min();
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetLastSlotIndex(this IContainer container, out int slotIndex)
        {
            if (container.validSlotIndices.Count <= 0)
            {
                slotIndex = -1;
                return false;
            }

            slotIndex = container.validSlotIndices.Max();
            return true;
        }

        #endregion

        #region Has Valid Item

        /// <summary>
        ///     判断容器是否有有效的物品
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValidItem(this IContainer container)
        {
            return container.validSlotIndices.Count > 0;
        }

        #endregion

        #region Contains

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsItem(this IContainer container, IContainerItem item)
        {
            return container.TryGetSlotIndex(item, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsItem(this IContainer container, string itemID)
        {
            return container.TryGetSlotIndices(itemID, out _);
        }

        #endregion

        #region Get Game Types

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetAllItemGameTypes(this IContainer container)
        {
            var itemIDs = new HashSet<string>();
            var itemTypeIDs = new HashSet<string>();

            foreach (var item in container.GetAllItems())
            {
                if (item == null)
                {
                    continue;
                }

                if (itemIDs.Add(item.id))
                {
                    itemTypeIDs.AddRange(item.gameTypeSet.gameTypesID);
                }
            }

            return itemTypeIDs;
        }

        #endregion

        #region Get All Items By Game Type

        public static IEnumerable<(int slotIndex, IContainerItem item)> GetItemsByGameType(
            this IContainer container, string typeID)
        {
            foreach (var (slotIndex, item) in container.EnumerateAllItems())
            {
                if (item == null)
                {
                    continue;
                }

                if (item.gameTypeSet.HasGameType(typeID))
                {
                    yield return (slotIndex, item);
                }
            }
        }

        public static IEnumerable<(int slotIndex, IContainerItem item)> GetItemsByAnyGameType(
            this IContainer container, IEnumerable<string> typeIDs)
        {
            if (typeIDs == null)
            {
                yield break;
            }

            foreach (var (slotIndex, item) in container.EnumerateAllItems())
            {
                if (item == null)
                {
                    continue;
                }

                if (item.gameTypeSet.HasAnyGameType(typeIDs))
                {
                    yield return (slotIndex, item);
                }
            }
        }

        public static IEnumerable<(int slotIndex, IContainerItem item)> GetItemsByAllGameType(
            this IContainer container, IEnumerable<string> typeIDs)
        {
            if (typeIDs == null)
            {
                yield break;
            }

            foreach (var (slotIndex, item) in container.EnumerateAllItems())
            {
                if (item == null)
                {
                    continue;
                }

                if (item.gameTypeSet.HasAllGameTypes(typeIDs))
                {
                    yield return (slotIndex, item);
                }
            }
        }

        #endregion
    }
}