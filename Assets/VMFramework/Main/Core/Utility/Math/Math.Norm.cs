using System;
using System.Runtime.CompilerServices;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        #region Abs

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Abs(this int num)
        {
            return Mathf.Abs(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(this float num)
        {
            return Mathf.Abs(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Abs(this double num)
        {
            return System.Math.Abs(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Abs(this Vector4 a)
        {
            return a.ForeachNumber(num => num.Abs());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Abs(this Vector3Int a)
        {
            return a.ForeachNumber(num => num.Abs());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Abs(this Vector3 a)
        {
            return a.ForeachNumber(num => num.Abs());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Abs(this Vector2Int a)
        {
            return a.ForeachNumber(num => num.Abs());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Abs(this Vector2 a)
        {
            return a.ForeachNumber(num => num.Abs());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Abs(this Color a)
        {
            return a.ForeachNumber(num => num.Abs());
        }

        #endregion

        #region L1Norm

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float L1Norm(this Vector2 a)
        {
            return a.Abs().Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float L1Norm(this Vector3 a)
        {
            return a.Abs().Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float L1Norm(this Vector4 a)
        {
            return a.Abs().Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int L1Norm(this Vector2Int a)
        {
            return a.Abs().Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int L1Norm(this Vector3Int a)
        {
            return a.Abs().Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float L1Norm(this Color a)
        {
            return a.Abs().Sum();
        }

        #endregion

        #region L2Norm

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float L2Norm(this Vector2 a)
        {
            return a.magnitude;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float L2Norm(this Vector3 a)
        {
            return a.magnitude;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float L2Norm(this Vector4 a)
        {
            return a.magnitude;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float L2Norm(this Vector2Int a)
        {
            return a.magnitude;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float L2Norm(this Vector3Int a)
        {
            return a.magnitude;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float L2Norm(this Color a)
        {
            return ((Vector4)a).magnitude;
        }

        #endregion

        #region Infinity Norm

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float InfinityNorm(this Vector2 a)
        {
            return a.Abs().Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float InfinityNorm(this Vector3 a)
        {
            return a.Abs().Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float InfinityNorm(this Vector4 a)
        {
            return a.Abs().Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int InfinityNorm(this Vector2Int a)
        {
            return a.Abs().Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int InfinityNorm(this Vector3Int a)
        {
            return a.Abs().Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float InfinityNorm(this Color a)
        {
            return a.Abs().Max();
        }

        #endregion

        #region Norm

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Norm(this Vector2 a, double p)
        {
            var vector = Vector<float>.Build.DenseOfArray(new[] { a.x, a.y });

            return (float)vector.Norm(p);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Norm(this Vector3 a, double p)
        {
            var vector = Vector<float>.Build.DenseOfArray(new[] { a.x, a.y, a.z });

            return (float)vector.Norm(p);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Norm(this Vector4 a, double p)
        {
            var vector = Vector<float>.Build.DenseOfArray(new[] { a.x, a.y, a.z, a.w });

            return (float)vector.Norm(p);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Norm(this Vector2Int a, double p)
        {
            var vector = Vector<int>.Build.DenseOfArray(new[] { a.x, a.y });

            return (float)vector.Norm(p);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Norm(this Vector3Int a, double p)
        {
            var vector = Vector<int>.Build.DenseOfArray(new[] { a.x, a.y, a.z });

            return (float)vector.Norm(p);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Norm(this Color a, double p)
        {
            var vector = Vector<float>.Build.DenseOfArray(new[] { a.r, a.g, a.b, a.a });

            return (float)vector.Norm(p);
        }

        #endregion
    }
}