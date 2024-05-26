using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        #region Cos

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(this int num)
        {
            return Mathf.Cos(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(this float num)
        {
            return Mathf.Cos(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cos(this double num)
        {
            return System.Math.Cos(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Cos(this Vector2 vector)
        {
            return vector.ForeachNumber(Cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cos(this Vector3 vector)
        {
            return vector.ForeachNumber(Cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Cos(this Vector4 vector)
        {
            return vector.ForeachNumber(Cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Cos(this Vector2Int vector)
        {
            return vector.ForeachNumber(Cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cos(this Vector3Int vector)
        {
            return vector.ForeachNumber(Cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Cos(this Color color)
        {
            return color.ForeachNumber(Cos);
        }

        #endregion

        #region Sin

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(this int num)
        {
            return Mathf.Sin(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(this float num)
        {
            return Mathf.Sin(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sin(this double num)
        {
            return System.Math.Sin(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Sin(this Vector2 vector)
        {
            return vector.ForeachNumber(Sin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Sin(this Vector3 vector)
        {
            return vector.ForeachNumber(Sin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Sin(this Vector4 vector)
        {
            return vector.ForeachNumber(Sin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Sin(this Vector2Int vector)
        {
            return vector.ForeachNumber(Sin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Sin(this Vector3Int vector)
        {
            return vector.ForeachNumber(Sin);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Sin(this Color color)
        {
            return color.ForeachNumber(Sin);
        }

        #endregion

        #region Tan

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tan(this int num)
        {
            return Mathf.Tan(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tan(this float num)
        {
            return Mathf.Tan(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Tan(this double num)
        {
            return System.Math.Tan(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Tan(this Vector2 vector)
        {
            return vector.ForeachNumber(Tan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Tan(this Vector3 vector)
        {
            return vector.ForeachNumber(Tan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Tan(this Vector4 vector)
        {
            return vector.ForeachNumber(Tan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Tan(this Vector2Int vector)
        {
            return vector.ForeachNumber(Tan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Tan(this Vector3Int vector)
        {
            return vector.ForeachNumber(Tan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Tan(this Color color)
        {
            return color.ForeachNumber(Tan);
        }

        #endregion
    }
}