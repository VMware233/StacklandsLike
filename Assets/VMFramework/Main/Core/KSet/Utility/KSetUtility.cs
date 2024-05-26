using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class KSetUtility
    {
        #region Contains

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsAll<TPoint>(this IKSet<TPoint> cube, params TPoint[] poses)
            where TPoint : struct, IEquatable<TPoint>
        {
            return poses.All(cube.Contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsAny<TPoint>(this IKSet<TPoint> cube, params TPoint[] poses)
            where TPoint : struct, IEquatable<TPoint>
        {
            return poses.Any(cube.Contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsAll<TPoint>(this IKSet<TPoint> cube, IEnumerable<TPoint> poses)
            where TPoint : struct, IEquatable<TPoint>
        {
            return poses.All(cube.Contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsAny<TPoint>(this IKSet<TPoint> cube, IEnumerable<TPoint> poses)
            where TPoint : struct, IEquatable<TPoint>
        {
            return poses.Any(cube.Contains);
        }

        #endregion
    }
}