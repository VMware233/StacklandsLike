using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class ChooserUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this IEnumerable<IChooser<T>> choosers)
        {
            foreach (var chooser in choosers)
            {
                yield return chooser.GetValue();
            }
        }
    }
}