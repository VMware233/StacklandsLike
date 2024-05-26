using System.Collections.Generic;
using System.Linq;
using VMFramework.Core;

namespace VMFramework.Containers
{
    public partial class ContainerUtility
    {
        #region Contains Enough Items

        public static bool ContainsEnoughItems(this Container container,
            IItemConsumption consumption)
        {
            return container.HasItemCount(consumption.itemID,
                consumption.count);
        }

        public static bool ContainsEnoughItems(this IEnumerable<Container> containers,
            IItemConsumption consumption)
        {
            return containers.HasItemCount(consumption.itemID,
                consumption.count);
        }

        public static bool ContainsEnoughItems(this Container container,
            IEnumerable<IItemConsumption> consumptions)
        {
            return consumptions.All(container.ContainsEnoughItems);
        }

        public static bool ContainsEnoughItems(this IEnumerable<Container> containers,
            IEnumerable<IItemConsumption> consumptions)
        {
            return consumptions.All(containers.ContainsEnoughItems);
        }


        #endregion

        #region Build Dictionary

        public static Dictionary<string, int> ToDictionary(
            this IEnumerable<IItemConsumption> consumptions)
        {
            var itemCountDictionary = new Dictionary<string, int>();

            foreach (var consumption in consumptions)
            {
                if (consumption.itemID.IsNullOrEmpty())
                {
                    continue;
                }

                if (itemCountDictionary.TryAdd(consumption.itemID,
                        consumption.count) == false)
                {
                    itemCountDictionary[consumption.itemID] += consumption.count;
                }
            }

            return itemCountDictionary;
        }

        #endregion
    }
}