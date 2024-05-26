using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        #region Min With Other

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Min(this int a, int b)
        {
            return a < b ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Min(this long a, long b)
        {
            return a < b ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Min(this short a, short b)
        {
            return a < b ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(this float a, float b)
        {
            return a < b ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(this double a, double b)
        {
            return a < b ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Min(this Vector2 a, Vector2 b)
        {
            Vector2 minVector = new()
            {
                x = a.x < b.x ? a.x : b.x,
                y = a.y < b.y ? a.y : b.y
            };
            return minVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Min(this Vector3 a, Vector3 b)
        {
            Vector3 minVector = new()
            {
                x = a.x < b.x ? a.x : b.x,
                y = a.y < b.y ? a.y : b.y,
                z = a.z < b.z ? a.z : b.z
            };
            return minVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Min(this Vector2Int a, Vector2Int b)
        {
            Vector2Int minVector = new()
            {
                x = a.x < b.x ? a.x : b.x,
                y = a.y < b.y ? a.y : b.y
            };
            return minVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Min(this Vector3Int a, Vector3Int b)
        {
            Vector3Int minVector = new()
            {
                x = a.x < b.x ? a.x : b.x,
                y = a.y < b.y ? a.y : b.y,
                z = a.z < b.z ? a.z : b.z
            };
            return minVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Min(this Vector4 a, Vector4 b)
        {
            Vector4 minVector = new()
            {
                x = a.x < b.x ? a.x : b.x,
                y = a.y < b.y ? a.y : b.y,
                z = a.z < b.z ? a.z : b.z,
                w = a.w < b.w ? a.w : b.w
            };
            return minVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Min(this Color a, Color b)
        {
            Color minColor = new()
            {
                r = a.r < b.r ? a.r : b.r,
                g = a.g < b.g ? a.g : b.g,
                b = a.b < b.b ? a.b : b.b,
                a = a.a < b.a ? a.a : b.a
            };
            return minColor;
        }

        #endregion

        #region Min Self

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(this Vector2 vector)
        {
            return Mathf.Min(vector.x, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(this Vector3 vector)
        {
            return Mathf.Min(vector.x, vector.y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(this Vector4 vector)
        {
            return Mathf.Min(vector.x, vector.y, vector.z, vector.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(this Color color)
        {
            return Mathf.Min(color.r, color.g, color.b, color.a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Min(this Vector2Int vector)
        {
            return Mathf.Min(vector.x, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Min(this Vector3Int vector)
        {
            return Mathf.Min(vector.x, vector.y, vector.z);
        }

        #endregion

        #region Max With Other

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Max(this int a, int b)
        {
            return a > b ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Max(this long a, long b)
        {
            return a > b ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Max(this short a, short b)
        {
            return a > b ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(this float a, float b)
        {
            return a > b ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(this double a, double b)
        {
            return a > b ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Max(this Vector2 a, Vector2 b)
        {
            Vector2 minVector = new()
            {
                x = a.x > b.x ? a.x : b.x,
                y = a.y > b.y ? a.y : b.y
            };
            return minVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Max(this Vector3 a, Vector3 b)
        {
            Vector3 minVector = new()
            {
                x = a.x > b.x ? a.x : b.x,
                y = a.y > b.y ? a.y : b.y,
                z = a.z > b.z ? a.z : b.z
            };
            return minVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Max(this Vector2Int a, Vector2Int b)
        {
            Vector2Int minVector = new()
            {
                x = a.x > b.x ? a.x : b.x,
                y = a.y > b.y ? a.y : b.y
            };
            return minVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Max(this Vector3Int a, Vector3Int b)
        {
            Vector3Int minVector = new()
            {
                x = a.x > b.x ? a.x : b.x,
                y = a.y > b.y ? a.y : b.y,
                z = a.z > b.z ? a.z : b.z
            };
            return minVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Max(this Vector4 a, Vector4 b)
        {
            Vector4 minVector = new()
            {
                x = a.x > b.x ? a.x : b.x,
                y = a.y > b.y ? a.y : b.y,
                z = a.z > b.z ? a.z : b.z,
                w = a.w > b.w ? a.w : b.w
            };
            return minVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Max(this Color a, Color b)
        {
            Color minColor = new()
            {
                r = a.r > b.r ? a.r : b.r,
                g = a.g > b.g ? a.g : b.g,
                b = a.b > b.b ? a.b : b.b,
                a = a.a > b.a ? a.a : b.a
            };
            return minColor;
        }

        #endregion

        #region Max Self

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(this Vector2 vector)
        {
            return Mathf.Max(vector.x, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(this Vector3 vector)
        {
            return Mathf.Max(vector.x, vector.y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(this Vector4 vector)
        {
            return Mathf.Max(vector.x, vector.y, vector.z, vector.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(this Color color)
        {
            return Mathf.Max(color.r, color.g, color.b, color.a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Max(this Vector2Int vector)
        {
            return Mathf.Max(vector.x, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Max(this Vector3Int vector)
        {
            return Mathf.Max(vector.x, vector.y, vector.z);
        }

        #endregion
    }
}