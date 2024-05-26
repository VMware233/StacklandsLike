using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Linq
{
    public static partial class LINQUtility
    {
        #region Count

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Count<T>(this IEnumerable<T> enumerable, T itemToCount)
        {
            return enumerable.Count(item => item.Equals(itemToCount));
        }

        #endregion

        #region Unique Count
        
        public static bool IsUnique<T>(this IEnumerable<T> enumerable)
        {
            var hashSet = new HashSet<T>();

            foreach (var item in enumerable)
            {
                if (hashSet.Add(item) == false)
                {
                    return false;
                }
            }
            
            return true;
        }

        public static int UniqueCount<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Distinct().Count();
        }

        #endregion
    }
}