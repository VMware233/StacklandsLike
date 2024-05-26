using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Containers
{
    public static class OutputsContainerUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IContainerItem> GetValidOutputs(this IOutputsContainer outputsContainer)
        {
            return outputsContainer.GetRangeValidItems(outputsContainer.outputsRange);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAddOutput(this IOutputsContainer outputsContainer, IContainerItem item)
        {
            return outputsContainer.TryAddItem(item, outputsContainer.outputsRange);
        }
    }
}