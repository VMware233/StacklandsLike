using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.GameEvents
{
    public static class InputActionGroupUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<InputActionGroupRuntime> ToRuntime(
            this IEnumerable<InputActionGroup> groups)
        {
            foreach (var group in groups)
            {
                yield return new InputActionGroupRuntime(group);
            }
        }
    }
}