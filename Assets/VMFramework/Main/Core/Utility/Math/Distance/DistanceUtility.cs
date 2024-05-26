using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math 
    {
        #region Universal Distance

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Distance(this int a, int b)
        {
            return (a - b).Abs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(this float a, float b)
        {
            return (a - b).Abs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(this double a, double b)
        {
            return (a - b).Abs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(this Color a, Color b,
            DistanceType distanceType,
            bool ignoreAlpha = false)
        {
            if (distanceType == DistanceType.Euclidean)
            {
                return a.EuclideanDistance(b, ignoreAlpha);
            }

            return a.ManhattanDistance(b, ignoreAlpha);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(this Vector4 a, Vector4 b,
            DistanceType distanceType)
        {
            if (distanceType == DistanceType.Euclidean)
            {
                return a.EuclideanDistance(b);
            }

            return a.ManhattanDistance(b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(this Vector3Int a, Vector3Int b,
            DistanceType distanceType)
        {
            if (distanceType == DistanceType.Euclidean)
            {
                return a.EuclideanDistance(b);
            }

            return a.ManhattanDistance(b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(this Vector2Int a, Vector2Int b,
            DistanceType distanceType)
        {
            if (distanceType == DistanceType.Euclidean)
            {
                return a.EuclideanDistance(b);
            }

            return a.ManhattanDistance(b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(this Vector3 a, Vector3 b,
            DistanceType distanceType)
        {
            if (distanceType == DistanceType.Euclidean)
            {
                return a.EuclideanDistance(b);
            }

            return a.ManhattanDistance(b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(this Vector2 a, Vector2 b,
            DistanceType distanceType)
        {
            if (distanceType == DistanceType.Euclidean)
            {
                return a.EuclideanDistance(b);
            }

            return a.ManhattanDistance(b);
        }

        #endregion

        #region Euclidean Distance

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EuclideanDistance(this Vector4 a, Vector4 b)
        {
            return Vector4.Distance(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EuclideanDistance(this Color a, Color b,
            bool ignoreAlpha = false)
        {
            return ignoreAlpha
                ? (a.To3D() - b.To3D()).Abs().Sum()
                : (a.To4D() - b.To4D()).Abs().Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EuclideanDistance(this Vector3Int a, Vector3Int b)
        {
            return Vector3Int.Distance(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EuclideanDistance(this Vector2Int a, Vector2Int b)
        {
            return Vector2Int.Distance(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EuclideanDistance(this Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EuclideanDistance(this Vector2 a, Vector2 b)
        {
            return Vector2.Distance(a, b);
        }

        #endregion

        #region Manhattan Distance

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ManhattanDistance(this Vector3Int a, Vector3Int b)
        {
            return (a - b).Abs().Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ManhattanDistance(this Vector2Int a, Vector2Int b)
        {
            return (a - b).Abs().Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ManhattanDistance(this Vector2 a, Vector2 b)
        {
            return (a - b).Abs().Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ManhattanDistance(this Vector3 a, Vector3 b)
        {
            return (a - b).Abs().Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ManhattanDistance(this Vector4 a, Vector4 b)
        {
            return (a - b).Abs().Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ManhattanDistance(this Color a, Color b,
            bool ignoreAlpha = true)
        {
            return ignoreAlpha
                ? (a.To3D() - b.To3D()).Abs().Sum()
                : (a.To4D() - b.To4D()).Abs().Sum();
        }

        #endregion
    }
}