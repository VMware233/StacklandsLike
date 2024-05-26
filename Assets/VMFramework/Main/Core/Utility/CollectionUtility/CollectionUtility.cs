using JetBrains.Annotations;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core.Linq;

namespace VMFramework.Core
{
    public static class CollectionUtility
    {
        #region Add

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddIfNotContains<T>(this ICollection<T> collection,
            T item)
        {
            if (!collection.Contains(item))
            {
                collection.Add(item);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(this ICollection<T> collection, [CanBeNull] T item,
            int count)
        {
            count.Repeat(() => { collection.Add(item); });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this ICollection<T> collection,
            IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        #endregion
    }
}
