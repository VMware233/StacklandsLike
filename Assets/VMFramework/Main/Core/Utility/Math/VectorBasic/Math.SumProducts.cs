using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        #region Products

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Products(this Vector3Int a)
        {
            return a.x * a.y * a.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Products(this Vector3 a)
        {
            return a.x * a.y * a.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Products(this Vector2Int a)
        {
            return a.x * a.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Products(this Vector2 a)
        {
            return a.x * a.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Products(this Vector4 a)
        {
            return a.x * a.y * a.z * a.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Products(this Color a)
        {
            return a.r * a.g * a.b * a.a;
        }

        #endregion

        #region Sum

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sum(this Vector3Int a)
        {
            return a.x + a.y + a.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sum(this Vector3 a)
        {
            return a.x + a.y + a.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sum(this Vector2Int a)
        {
            return a.x + a.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sum(this Vector2 a)
        {
            return a.x + a.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sum(this Vector4 a)
        {
            return a.x + a.y + a.z + a.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sum(this Color a)
        {
            return a.r + a.g + a.b + a.a;
        }

        #endregion
    }
}