using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static partial class Math
    {
        #region For Each Number

        #region Color

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ForeachNumber(this Color a, Color b, Color c,
            Func<float, float, float, float> func)
        {
            return new(func(a.r, b.r, c.r), func(a.g, b.g, c.g), func(a.b, b.b, c.b),
                func(a.a, b.a, c.a));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ForeachNumber(this Color a, Color b,
            Func<float, float, float> func)
        {
            return new(func(a.r, b.r), func(a.g, b.g), func(a.b, b.b),
                func(a.a, b.a));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ForeachNumber(this Color a, float b,
            Func<float, float, float> func)
        {
            return new(func(a.r, b), func(a.g, b), func(a.b, b), func(a.a, b));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ForeachNumber(this Color a, Func<float, float> func)
        {
            return new(func(a.r), func(a.g), func(a.b), func(a.a));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForeachNumber(this Color a, Action<float> func)
        {
            func(a.r);
            func(a.g);
            func(a.b);
            func(a.a);
        }

        #endregion

        #region Vector4

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ForeachNumber(this Vector4 a, Vector4 b, Vector4 c,
            Func<float, float, float, float> func)
        {
            return new(func(a.x, b.x, c.x), func(a.y, b.y, c.y), func(a.z, b.z, c.z),
                func(a.w, b.w, c.w));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ForeachNumber(this Vector4 a, Vector4 b,
            Func<float, float, float> func)
        {
            return new(func(a.x, b.x), func(a.y, b.y), func(a.z, b.z),
                func(a.w, b.w));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ForeachNumber(this Vector4 a, float b,
            Func<float, float, float> func)
        {
            return new(func(a.x, b), func(a.y, b), func(a.z, b), func(a.w, b));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ForeachNumber(this Vector4 a, Func<float, float> func)
        {
            return new(func(a.x), func(a.y), func(a.z), func(a.w));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForeachNumber(this Vector4 a, Action<float> func)
        {
            func(a.x);
            func(a.y);
            func(a.z);
            func(a.w);
        }

        #endregion

        #region Vector3Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ForeachNumber(this Vector3Int a, Vector3Int b,
            Vector3 c, Func<int, int, float, int> func)
        {
            return new(func(a.x, b.x, c.x), func(a.y, b.y, c.y),
                func(a.z, b.z, c.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ForeachNumber(this Vector3Int a, Vector3Int b,
            Vector3 c, Func<int, int, float, float> func)
        {
            return new(func(a.x, b.x, c.x), func(a.y, b.y, c.y),
                func(a.z, b.z, c.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ForeachNumber(this Vector3Int a, Vector3Int b,
            Vector3Int c, Func<int, int, int, int> func)
        {
            return new(func(a.x, b.x, c.x), func(a.y, b.y, c.y),
                func(a.z, b.z, c.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ForeachNumber(this Vector3Int a, Vector3Int b,
            Vector3Int c, Func<int, int, int, float> func)
        {
            return new(func(a.x, b.x, c.x), func(a.y, b.y, c.y),
                func(a.z, b.z, c.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ForeachNumber(this Vector3Int a, Vector3Int b,
            Func<int, int, int> func)
        {
            return new(func(a.x, b.x), func(a.y, b.y), func(a.z, b.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ForeachNumber(this Vector3Int a, Vector3Int b,
            Func<int, int, float> func)
        {
            return new(func(a.x, b.x), func(a.y, b.y), func(a.z, b.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ForeachNumber(this Vector3Int a, int b,
            Func<int, int, int> func)
        {
            return new(func(a.x, b), func(a.y, b), func(a.z, b));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ForeachNumber(this Vector3Int a, int b,
            Func<int, int, float> func)
        {
            return new(func(a.x, b), func(a.y, b), func(a.z, b));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ForeachNumber(this Vector3Int a,
            Func<int, int> func)
        {
            return new(func(a.x), func(a.y), func(a.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ForeachNumber(this Vector3Int a, Func<int, float> func)
        {
            return new(func(a.x), func(a.y), func(a.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForeachNumber(this Vector3Int a, Action<int> func)
        {
            func(a.x);
            func(a.y);
            func(a.z);
        }

        #endregion

        #region Vector3

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ForeachNumber(this Vector3 a, Vector3 b, Vector3 c,
            Func<float, float, float, float> func)
        {
            return new(func(a.x, b.x, c.x), func(a.y, b.y, c.y),
                func(a.z, b.z, c.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ForeachNumber(this Vector3 a, Vector3 b, Vector3 c,
            Func<float, float, float, int> func)
        {
            return new(func(a.x, b.x, c.x), func(a.y, b.y, c.y),
                func(a.z, b.z, c.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ForeachNumber(this Vector3 a, Vector3 b,
            Func<float, float, float> func)
        {
            return new(func(a.x, b.x), func(a.y, b.y), func(a.z, b.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ForeachNumber(this Vector3 a, Vector3 b,
            Func<float, float, int> func)
        {
            return new(func(a.x, b.x), func(a.y, b.y), func(a.z, b.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ForeachNumber(this Vector3 a, float b,
            Func<float, float, float> func)
        {
            return new(func(a.x, b), func(a.y, b), func(a.z, b));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ForeachNumber(this Vector3 a, float b,
            Func<float, float, int> func)
        {
            return new(func(a.x, b), func(a.y, b), func(a.z, b));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ForeachNumber(this Vector3 a, Func<float, float> func)
        {
            return new(func(a.x), func(a.y), func(a.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ForeachNumber(this Vector3 a, Func<float, int> func)
        {
            return new(func(a.x), func(a.y), func(a.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForeachNumber(this Vector3 a, Action<float> func)
        {
            func(a.x);
            func(a.y);
            func(a.z);
        }

        #endregion

        #region Vector2Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ForeachNumber(this Vector2Int a, Vector2Int b,
            Vector2 c, Func<int, int, float, int> func)
        {
            return new(func(a.x, b.x, c.x), func(a.y, b.y, c.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ForeachNumber(this Vector2Int a, Vector2Int b,
            Vector2 c, Func<int, int, float, float> func)
        {
            return new(func(a.x, b.x, c.x), func(a.y, b.y, c.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ForeachNumber(this Vector2Int a, Vector2Int b,
            Vector2Int c, Func<int, int, int, int> func)
        {
            return new(func(a.x, b.x, c.x), func(a.y, b.y, c.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ForeachNumber(this Vector2Int a, Vector2Int b,
            Vector2Int c, Func<int, int, int, float> func)
        {
            return new(func(a.x, b.x, c.x), func(a.y, b.y, c.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ForeachNumber(this Vector2Int a, Vector2Int b,
            Func<int, int, int> func)
        {
            return new(func(a.x, b.x), func(a.y, b.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ForeachNumber(this Vector2Int a, Vector2Int b,
            Func<int, int, float> func)
        {
            return new(func(a.x, b.x), func(a.y, b.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ForeachNumber(this Vector2Int a, int b,
            Func<int, int, int> func)
        {
            return new(func(a.x, b), func(a.y, b));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ForeachNumber(this Vector2Int a, int b,
            Func<int, int, float> func)
        {
            return new(func(a.x, b), func(a.y, b));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ForeachNumber(this Vector2Int a,
            Func<int, int> func)
        {
            return new(func(a.x), func(a.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ForeachNumber(this Vector2Int a, Func<int, float> func)
        {
            return new(func(a.x), func(a.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForeachNumber(this Vector2Int a, Action<int> func)
        {
            func(a.x);
            func(a.y);
        }

        #endregion

        #region Vector2

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ForeachNumber(this Vector2 a, Vector2 b, Vector2 c,
            Func<float, float, float, float> func)
        {
            return new(func(a.x, b.x, c.x), func(a.y, b.y, c.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ForeachNumber(this Vector2 a, Vector2 b, Vector2 c,
            Func<float, float, float, int> func)
        {
            return new(func(a.x, b.x, c.x), func(a.y, b.y, c.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ForeachNumber(this Vector2 a, Vector2 b,
            Func<float, float, float> func)
        {
            return new(func(a.x, b.x), func(a.y, b.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ForeachNumber(this Vector2 a, Vector2 b,
            Func<float, float, int> func)
        {
            return new(func(a.x, b.x), func(a.y, b.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ForeachNumber(this Vector2 a, float b,
            Func<float, float, float> func)
        {
            return new(func(a.x, b), func(a.y, b));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ForeachNumber(this Vector2 a, float b,
            Func<float, float, int> func)
        {
            return new(func(a.x, b), func(a.y, b));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ForeachNumber(this Vector2 a, Func<float, float> func)
        {
            return new(func(a.x), func(a.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ForeachNumber(this Vector2 a, Func<float, int> func)
        {
            return new(func(a.x), func(a.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForeachNumber(this Vector2 a, Action<float> func)
        {
            func(a.x);
            func(a.y);
        }

        #endregion

        #endregion
        
        #region All

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this Color a, Func<float, bool> func)
        {
            return func(a.r) && func(a.g) && func(a.b) && func(a.a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this Vector4 a, Func<float, bool> func)
        {
            return func(a.x) && func(a.y) && func(a.z) && func(a.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this Vector3Int a, Func<int, bool> func)
        {
            return func(a.x) && func(a.y) && func(a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this Vector3 a, Func<float, bool> func)
        {
            return func(a.x) && func(a.y) && func(a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this Vector2Int a, Func<int, bool> func)
        {
            return func(a.x) && func(a.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this Vector2 a, Func<float, bool> func)
        {
            return func(a.x) && func(a.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this Color a, Color b, Func<float, float, bool> func)
        {
            return func(a.r, b.r) && func(a.g, b.g) && func(a.b, b.b) &&
                   func(a.a, b.a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this Vector4 a, Vector4 b,
            Func<float, float, bool> func)
        {
            return func(a.x, b.x) && func(a.y, b.y) && func(a.z, b.z) &&
                   func(a.w, b.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this Vector3Int a, Vector3Int b,
            Func<int, int, bool> func)
        {
            return func(a.x, b.x) && func(a.y, b.y) && func(a.z, b.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this Vector3 a, Vector3 b,
            Func<float, float, bool> func)
        {
            return func(a.x, b.x) && func(a.y, b.y) && func(a.z, b.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this Vector2Int a, Vector2Int b,
            Func<int, int, bool> func)
        {
            return func(a.x, b.x) && func(a.y, b.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this Vector2 a, Vector2 b,
            Func<float, float, bool> func)
        {
            return func(a.x, b.x) && func(a.y, b.y);
        }

        #endregion

        #region Any

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(this Color a, Func<float, bool> func)
        {
            return func(a.r) || func(a.g) || func(a.b) || func(a.a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(this Vector4 a, Func<float, bool> func)
        {
            return func(a.x) || func(a.y) || func(a.z) || func(a.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(this Vector3Int a, Func<int, bool> func)
        {
            return func(a.x) || func(a.y) || func(a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(this Vector3 a, Func<float, bool> func)
        {
            return func(a.x) || func(a.y) || func(a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(this Vector2Int a, Func<int, bool> func)
        {
            return func(a.x) || func(a.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(this Vector2 a, Func<float, bool> func)
        {
            return func(a.x) || func(a.y);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(this Color a, Color b, Func<float, float, bool> func)
        {
            return func(a.r, b.r) || func(a.g, b.g) || func(a.b, b.b) ||
                   func(a.a, b.a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(this Vector4 a, Vector4 b,
            Func<float, float, bool> func)
        {
            return func(a.x, b.x) || func(a.y, b.y) || func(a.z, b.z) ||
                   func(a.w, b.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(this Vector3Int a, Vector3Int b,
            Func<int, int, bool> func)
        {
            return func(a.x, b.x) || func(a.y, b.y) || func(a.z, b.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(this Vector3 a, Vector3 b,
            Func<float, float, bool> func)
        {
            return func(a.x, b.x) || func(a.y, b.y) || func(a.z, b.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(this Vector2Int a, Vector2Int b,
            Func<int, int, bool> func)
        {
            return func(a.x, b.x) || func(a.y, b.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(this Vector2 a, Vector2 b,
            Func<float, float, bool> func)
        {
            return func(a.x, b.x) || func(a.y, b.y);
        }

        #endregion
    }
}
