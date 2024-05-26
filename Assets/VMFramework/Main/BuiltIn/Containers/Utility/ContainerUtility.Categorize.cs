using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    public partial class ContainerUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<string, List<IContainerItem>> CategorizeByGameType(this IContainer container)
        {
            container.GetAllItems().BuildGameTypeDictionary(out var itemsCategorizedByType);
            return itemsCategorizedByType;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<string, List<TItem>> CategorizeByGameType<TItem>(this IContainer container)
            where TItem : IContainerItem
        {
            container.GetAllItems<TItem>().BuildGameTypeDictionary(out var itemsCategorizedByType);
            return itemsCategorizedByType;
        }
    }
}