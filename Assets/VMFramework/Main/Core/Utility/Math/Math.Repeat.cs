using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static partial class Math
    {
        /// <summary>
        /// 重复一个数值，使其在一个范围内循环
        /// 例如：5.Repeat(0, 3) => 1
        /// 6.Repeat(0, 3) => 2
        /// 7.Repeat(0, 3) => 3
        /// 8.Repeat(0, 3) => 0
        /// </summary>
        /// <param name="num"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Repeat(this int num, int min, int max)
        {
            return num.Modulo(max - min + 1) + min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Repeat(this int num, int length)
        {
            return num.Repeat(0, length - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Repeat(this float num, float min, float max)
        {
            return num.Modulo(max - min) + min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Repeat(this float num, float length)
        {
            return num.Repeat(0, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Repeat(this Vector2Int vector, Vector2Int min,
            Vector2Int max)
        {
            return vector.Modulo(max - min + Vector2Int.one) + min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Repeat(this Vector2Int vector, Vector2Int size)
        {
            return vector.Repeat(Vector2Int.zero, size - Vector2Int.one);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Repeat(this Vector3Int vector, Vector3Int min,
            Vector3Int max)
        {
            return vector.Modulo(max - min + Vector3Int.one) + min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Repeat(this Vector3Int vector, Vector3Int size)
        {
            return vector.Repeat(Vector3Int.zero, size - Vector3Int.one);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Repeat(this Vector2 vector, Vector2 min, Vector2 max)
        {
            return vector.Modulo(max - min) + min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Repeat(this Vector2 vector, Vector2 size)
        {
            return vector.Repeat(Vector2.zero, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Repeat(this Vector3 vector, Vector3 min, Vector3 max)
        {
            return vector.Modulo(max - min) + min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Repeat(this Vector3 vector, Vector3 size)
        {
            return vector.Repeat(Vector3.zero, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Repeat(this Vector4 vector, Vector4 min, Vector4 max)
        {
            return vector.Modulo(max - min) + min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Repeat(this Vector4 vector, Vector4 size)
        {
            return vector.Repeat(Vector4.zero, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Repeat(this Color color, Color min, Color max)
        {
            return color.Modulo(max - min) + min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Repeat(this Color color, Color size)
        {
            return color.Repeat(Color.black, size);
        }
    }
}