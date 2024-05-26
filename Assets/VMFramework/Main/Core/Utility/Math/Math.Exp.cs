using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        #region Exp

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Exp(this int power)
        {
            return Mathf.Exp(power);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Exp(this float power)
        {
            return Mathf.Exp(power);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Exp(this double power)
        {
            return System.Math.Exp(power);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Exp(this Vector2 power)
        {
            return ForeachNumber(power, Exp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Exp(this Vector2Int power)
        {
            return ForeachNumber(power, Exp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Exp(this Vector3 power)
        {
            return ForeachNumber(power, Exp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Exp(this Vector3Int power)
        {
            return ForeachNumber(power, Exp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Exp(this Vector4 power)
        {
            return ForeachNumber(power, Exp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Exp(this Color power)
        {
            return ForeachNumber(power, Exp);
        }

        #endregion
    }
}