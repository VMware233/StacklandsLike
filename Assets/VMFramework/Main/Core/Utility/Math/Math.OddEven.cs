using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        #region Is Odd

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOdd(this int num)
        {
            return num % 2 == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOdd(this long num)
        {
            return num % 2 == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOdd(this short num)
        {
            return num % 2 == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAllOdd(this Vector2Int vector2)
        {
            return vector2.All(IsOdd);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAllOdd(this Vector3Int vector3)
        {
            return vector3.All(IsOdd);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAnyOdd(this Vector2Int vector2)
        {
            return vector2.Any(IsOdd);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAnyOdd(this Vector3Int vector3)
        {
            return vector3.Any(IsOdd);
        }

        #endregion

        #region Is Even

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEven(this int num)
        {
            return num % 2 == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEven(this long num)
        {
            return num % 2 == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEven(this short num)
        {
            return num % 2 == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAllEven(this Vector2Int vector2)
        {
            return vector2.All(IsEven);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAllEven(this Vector3Int vector3)
        {
            return vector3.All(IsEven);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAnyEven(this Vector2Int vector2)
        {
            return vector2.Any(IsEven);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAnyEven(this Vector3Int vector3)
        {
            return vector3.Any(IsEven);
        }

        #endregion
    }
}