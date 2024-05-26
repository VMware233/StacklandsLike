using System.Runtime.CompilerServices;

namespace VMFramework.Core.Pool
{
    public static class LimitedPoolUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFull<TItem>(this ILimitedPool<TItem> pool)
        {
            return pool.count >= pool.capacity;
        }
    }
}