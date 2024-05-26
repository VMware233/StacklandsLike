using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using VMFramework.Core.Linq;

namespace VMFramework.Containers
{
    public partial class ContainerUtility
    {
        #region Build Count Dictionary

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BuildCountDictionary(this IEnumerable<IContainerItem> items,
            out Dictionary<string, int> countDictionary)
        {
            countDictionary = new Dictionary<string, int>();
            foreach (var item in items)
            {
                if (countDictionary.TryAdd(item.id, item.count) == false)
                {
                    countDictionary[item.id] += item.count;
                }
            }
        }

        #endregion
        
        #region Container Item Count

        public static int GetItemCount(this IContainer container, string itemID)
        {
            int result = 0;

            if (itemID.IsNullOrEmpty())
            {
                return result;
            }

            foreach (var validSlotIndex in container.validSlotIndices)
            {
                var item = container.GetItem(validSlotIndex);

                if (item == null)
                {
                    continue;
                }
                
                if (item.id == itemID)
                {
                    result += item.count;
                }
            }

            return result;
        }

        public static int GetItemCount(this IContainer container, int minIndex, int maxIndex, string itemID)
        {
            int result = 0;

            if (itemID.IsNullOrEmpty())
            {
                return result;
            }

            for (int index = minIndex; index <= maxIndex; index++)
            {
                if (container.TryGetItem(index, out var item) == false)
                {
                    continue;
                }
                
                if (item == null)
                {
                    continue;
                }
                
                if (item.id == itemID)
                {
                    result += item.count;
                }
            }

            return result;
        }

        public static bool HasItemCount(this IContainer container, string itemID, int count)
        {
            int existedCount = 0;

            if (itemID.IsNullOrEmpty())
            {
                return false;
            }

            foreach (var validSlotIndex in container.validSlotIndices)
            {
                var item = container.GetItem(validSlotIndex);
                
                if (item == null)
                {
                    continue;
                }
                
                if (item.id == itemID)
                {
                    existedCount += item.count;
                }

                if (existedCount >= count)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HasItemCount(this IContainer container, int minIndex, int maxIndex, string itemID,
            int count)
        {
            int existedCount = 0;

            if (itemID.IsNullOrEmpty())
            {
                return false;
            }

            for (int index = minIndex; index <= maxIndex; index++)
            {
                if (container.TryGetItem(index, out var item) == false)
                {
                    continue;
                }
                
                if (item == null)
                {
                    continue;
                }

                if (item.id == itemID)
                {
                    existedCount += item.count;
                }

                if (existedCount >= count)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HasItemsCount(this IContainer container, IReadOnlyDictionary<string, int> itemCountDictionary)
        {
            if (itemCountDictionary.IsNullOrEmpty())
            {
                return true;
            }

            foreach (var (itemID, count) in itemCountDictionary)
            {
                if (container.HasItemCount(itemID, count) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool HasItemsCount(this IContainer container, int minIndex, int maxIndex,
            IReadOnlyDictionary<string, int> itemCountDictionary)
        {
            if (itemCountDictionary.IsNullOrEmpty())
            {
                return true;
            }

            foreach (var (itemID, count) in itemCountDictionary)
            {
                if (container.HasItemCount(minIndex, maxIndex, itemID, count) == false)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
        
        #region Get Item Count

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetItemCount(this IEnumerable<Container> containers, string itemID)
        {
            var count = 0;

            foreach (var container in containers)
            {
                count += container.GetItemCount(itemID);
            }

            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<string, int> GetItemsCount(this IEnumerable<Container> containers,
            IEnumerable<string> itemsID)
        {
            var itemsCount = new Dictionary<string, int>();

            foreach (var itemID in itemsID)
            {
                itemsCount[itemID] = GetItemCount(containers, itemID);
            }

            return itemsCount;
        }

        #endregion

        #region Has Item Count

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasItemCount(this IEnumerable<Container> containers, string itemID, int count)
        {
            var existedCount = 0;

            foreach (var container in containers)
            {
                existedCount += container.GetItemCount(itemID);

                if (existedCount >= count)
                {
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasItemsCount(this IEnumerable<Container> containers,
            IReadOnlyDictionary<string, int> itemDictionary)
        {
            return itemDictionary.All(kvp => HasItemCount(containers, kvp.Key, kvp.Value));
        }

        #endregion

        #region Query Item Count

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Container> QueryContainersWithEnoughItems(
            this IEnumerable<Container> containers, string itemID, int count)
        {
            return containers.Where(container => container.HasItemCount(itemID, count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Container> QueryContainersWithEnoughItems(
            this IEnumerable<Container> containers, IReadOnlyDictionary<string, int> itemDictionary)
        {
            return containers.Where(container => container.HasItemsCount(itemDictionary));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Container QueryContainerWithEnoughItems(this IEnumerable<Container> containers,
            string itemID, int count)
        {
            return containers.FirstOrDefault(container => container.HasItemCount(itemID, count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Container QueryContainerWithEnoughItems(this IEnumerable<Container> containers,
            IReadOnlyDictionary<string, int> itemDictionary)
        {
            return containers.FirstOrDefault(container => container.HasItemsCount(itemDictionary));
        }

        #endregion
    }
}