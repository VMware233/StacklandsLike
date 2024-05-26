using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Pool
{
    public static class PoolUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmpty<TItem>(this IPool<TItem> pool)
        {
            return pool.count == 0;
        }
        
        /// <summary>
        /// Get an item from the pool.
        /// If the pool is empty, a new item will be created using the provided creator.
        /// </summary>
        /// <param name="pool"></param>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TItem Get<TItem>(this IPool<TItem> pool)
        {
            return pool.Get(out _);
        }
        
        /// <summary>
        /// prewarm the pool with the specified number of items.
        /// i.e. create the specified number of items and add them to the pool.
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="count"></param>
        /// <typeparam name="TItem"></typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Prewarm<TItem>(this IPool<TItem> pool, int count)
        {
            var temp = new List<TItem>();
            for (int i = 0; i < count; i++)
            {
                var item = pool.Get();
                temp.Add(item);
            }
            
            foreach (var item in temp)
            {
                pool.Return(item);
            }
        }
    }
}