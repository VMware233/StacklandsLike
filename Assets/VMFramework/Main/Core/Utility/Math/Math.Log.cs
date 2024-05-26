using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        #region Log

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(this int f, float p)
        {
            return Mathf.Log(f, p);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(this float f, float p)
        {
            return Mathf.Log(f, p);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Log(this double f, double p)
        {
            return System.Math.Log(f, p);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Log(this Vector2 f, float p)
        {
            return ForeachNumber(f, num => Mathf.Log(num, p));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Log(this Vector2Int f, float p)
        {
            return ForeachNumber(f, num => Mathf.Log(num, p));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Log(this Vector3 f, float p)
        {
            return ForeachNumber(f, num => Mathf.Log(num, p));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Log(this Vector3Int f, float p)
        {
            return ForeachNumber(f, num => Mathf.Log(num, p));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Log(this Vector4 f, float p)
        {
            return ForeachNumber(f, num => Mathf.Log(num, p));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Log(this Color f, float p)
        {
            return ForeachNumber(f, num => Mathf.Log(num, p));
        }

        #endregion

        #region Ln

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(this int f)
        {
            return Mathf.Log(f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(this float f)
        {
            return Mathf.Log(f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Log(this double f)
        {
            return System.Math.Log(f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Log(this Vector2 f)
        {
            return ForeachNumber(f, Log);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Log(this Vector2Int f)
        {
            return ForeachNumber(f, Log);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Log(this Vector3 f)
        {
            return ForeachNumber(f, Log);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Log(this Vector3Int f)
        {
            return ForeachNumber(f, Log);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Log(this Vector4 f)
        {
            return ForeachNumber(f, Log);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Log(this Color f)
        {
            return ForeachNumber(f, Log);
        }

        #endregion

        #region Log10

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log10(this int f)
        {
            return Mathf.Log10(f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log10(this float f)
        {
            return Mathf.Log10(f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Log10(this double f)
        {
            return System.Math.Log10(f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Log10(this Vector2 f)
        {
            return ForeachNumber(f, Log10);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Log10(this Vector2Int f)
        {
            return ForeachNumber(f, Log10);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Log10(this Vector3 f)
        {
            return ForeachNumber(f, Log10);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Log10(this Vector3Int f)
        {
            return ForeachNumber(f, Log10);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Log10(this Vector4 f)
        {
            return ForeachNumber(f, Log10);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Log10(this Color f)
        {
            return ForeachNumber(f, Log10);
        }

        #endregion

        #region Log2

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log2(this int f)
        {
            return Mathf.Log(f, 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log2(this float f)
        {
            return Mathf.Log(f, 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Log2(this double f)
        {
            return System.Math.Log(f, 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Log2(this Vector2 f)
        {
            return ForeachNumber(f, Log2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Log2(this Vector2Int f)
        {
            return ForeachNumber(f, Log2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Log2(this Vector3 f)
        {
            return ForeachNumber(f, Log2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Log2(this Vector3Int f)
        {
            return ForeachNumber(f, Log2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Log2(this Vector4 f)
        {
            return ForeachNumber(f, Log2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Log2(this Color f)
        {
            return ForeachNumber(f, Log2);
        }

        #endregion
    }
}