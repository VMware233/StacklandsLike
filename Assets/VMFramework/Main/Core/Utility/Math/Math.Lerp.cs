using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        #region Lerp Unclamped

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LerpUnclamped(this int from, int to, float t)
        {
            return LerpUnclamped((float)from, to, t).Round();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float LerpUnclamped(this float from, float to, float t)
        {
            return from + (to - from) * t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LerpUnclamped(this double from, double to, double t)
        {
            return from + (to - from) * t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 LerpUnclamped(this Vector3 a, Vector3 b, float t)
        {
            return Vector3.LerpUnclamped(a, b, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 LerpUnclamped(this Vector3 a, Vector3 b, Vector3 t)
        {
            return a.ForeachNumber(b, t, LerpUnclamped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 LerpUnclamped(this Vector2 a, Vector2 b, float t)
        {
            return Vector2.LerpUnclamped(a, b, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 LerpUnclamped(this Vector2 a, Vector2 b, Vector2 t)
        {
            return a.ForeachNumber(b, t, LerpUnclamped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int LerpUnclamped(this Vector3Int a, Vector3Int b,
            float t)
        {
            return a.ForeachNumber(b, (aNum, bNum) => aNum.LerpUnclamped(bNum, t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int LerpUnclamped(this Vector3Int a, Vector3Int b,
            Vector3 t)
        {
            return a.ForeachNumber(b, t, LerpUnclamped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int LerpUnclamped(this Vector2Int a, Vector2Int b,
            float t)
        {
            return a.ForeachNumber(b, (aNum, bNum) => aNum.LerpUnclamped(bNum, t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int LerpUnclamped(this Vector2Int a, Vector2Int b,
            Vector2 t)
        {
            return a.ForeachNumber(b, t, LerpUnclamped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 LerpUnclamped(this Vector4 a, Vector4 b, float t)
        {
            return Vector4.LerpUnclamped(a, b, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 LerpUnclamped(this Vector4 a, Vector4 b, Vector4 t)
        {
            return a.ForeachNumber(b, t, LerpUnclamped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color LerpUnclamped(this Color a, Color b, float t)
        {
            return Color.LerpUnclamped(a, b, t);
        }

        #endregion

        #region Linear Lerp

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Lerp(this int from, int to, float t)
        {
            return Mathf.Lerp(from, to, t).Round();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(this float from, float to, float t)
        {
            return Mathf.Lerp(from, to, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Lerp(this double from, double to, double t)
        {
            return from + (to - from) * t.Clamp01();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Lerp(this Vector3 a, Vector3 b, float t)
        {
            return Vector3.Lerp(a, b, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Lerp(this Vector3 a, Vector3 b, Vector3 t)
        {
            return a.ForeachNumber(b, t, Lerp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Lerp(this Vector2 a, Vector2 b, float t)
        {
            return Vector2.Lerp(a, b, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Lerp(this Vector2 a, Vector2 b, Vector2 t)
        {
            return a.ForeachNumber(b, t, Lerp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Lerp(this Vector3Int a, Vector3Int b, float t)
        {
            return a.ForeachNumber(b, (aNum, bNum) => aNum.Lerp(bNum, t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Lerp(this Vector3Int a, Vector3Int b, Vector3 t)
        {
            return a.ForeachNumber(b, t, Lerp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Lerp(this Vector2Int a, Vector2Int b, float t)
        {
            return a.ForeachNumber(b, (aNum, bNum) => aNum.Lerp(bNum, t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Lerp(this Vector2Int a, Vector2Int b, Vector2 t)
        {
            return a.ForeachNumber(b, t, Lerp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Lerp(this Vector4 a, Vector4 b, float t)
        {
            return Vector4.Lerp(a, b, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Lerp(this Vector4 a, Vector4 b, Vector4 t)
        {
            return a.ForeachNumber(b, t, Lerp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Lerp(this Color a, Color b, float t)
        {
            return Color.Lerp(a, b, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Lerp(this Color a, Color b, Color t)
        {
            return a.ForeachNumber(b, t, Lerp);
        }

        #endregion

        #region Lerp With Power

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Lerp(this int from, int to, float t, float power)
        {
            return Mathf.Lerp(from, to, t.Power(power)).Round();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(this float from, float to, float t, float power)
        {
            return Mathf.Lerp(from, to, t.Power(power));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Lerp(this double from, double to, float t, float power)
        {
            return from + (to - from) * t.Clamp01().Power(power);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Lerp(this Vector4 a, Vector4 b, float t, float power)
        {
            return Vector4.Lerp(a, b, t.Power(power));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Lerp(this Color a, Color b, float t, float power)
        {
            return Color.Lerp(a, b, t.Power(power));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Lerp(this Vector3 a, Vector3 b, float t, float power)
        {
            return Vector3.Lerp(a, b, t.Power(power));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Lerp(this Vector2 a, Vector2 b, float t, float power)
        {
            return Vector2.Lerp(a, b, t.Power(power));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Lerp(this Vector3Int a, Vector3Int b, float t,
            float power)
        {
            return a.ForeachNumber(b,
                (aNum, bNum) => aNum.Lerp(bNum, t.Power(power)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Lerp(this Vector2Int a, Vector2Int b, float t,
            float power)
        {
            return a.ForeachNumber(b,
                (aNum, bNum) => aNum.Lerp(bNum, t.Power(power)));
        }

        #endregion
    }
}