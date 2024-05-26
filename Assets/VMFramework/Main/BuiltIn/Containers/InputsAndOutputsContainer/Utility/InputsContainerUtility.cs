using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Containers
{
    public static class InputsContainerUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IContainerItem> GetValidInputs(this IInputsContainer inputsContainer)
        {
            return inputsContainer.GetRangeValidItems(inputsContainer.inputsRange);
        }
    }
}