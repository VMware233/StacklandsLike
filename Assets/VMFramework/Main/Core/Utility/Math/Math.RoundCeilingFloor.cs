using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        #region Round

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Round(this float value)
        {
            return Mathf.RoundToInt(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Round(this double value)
        {
            return (int)System.Math.Round(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Round(this Vector2 vector)
        {
            return vector.ForeachNumber(num => num.Round());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Round(this Vector3 vector)
        {
            return vector.ForeachNumber(num => num.Round());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Round(this Vector4 vector)
        {
            return vector.ForeachNumber(num => num.Round());
        }

        #endregion

        #region Ceiling

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Ceiling(this float value)
        {
            return Mathf.CeilToInt(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Ceiling(this double value)
        {
            return (int)System.Math.Ceiling(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Ceiling(this Vector2 vector)
        {
            return vector.ForeachNumber(num => num.Ceiling());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Ceiling(this Vector3 vector)
        {
            return vector.ForeachNumber(num => num.Ceiling());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Ceiling(this Vector4 vector)
        {
            return vector.ForeachNumber(num => num.Ceiling());
        }

        #endregion

        #region Floor

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Floor(this float value)
        {
            return Mathf.FloorToInt(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Floor(this double value)
        {
            return (int)System.Math.Floor(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Floor(this Vector2 vector)
        {
            return vector.ForeachNumber(num => num.Floor());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Floor(this Vector3 vector)
        {
            return vector.ForeachNumber(num => num.Floor());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Floor(this Vector4 vector)
        {
            return vector.ForeachNumber(num => num.Floor());
        }

        #endregion
    }
}