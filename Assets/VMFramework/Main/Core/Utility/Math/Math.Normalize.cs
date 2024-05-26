using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        #region Normalize01

        /// <summary>
        ///     归一化到0和1之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Normalize01(this double t, double min, double max)
        {
            return (min - max).Abs() > double.Epsilon ? ((t - min) / (max - min)).Clamp01() : 0.0f;
        }

        /// <summary>
        ///     归一化到0和1之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Normalize01(this float t, float min, float max)
        {
            return Mathf.InverseLerp(min, max, t);
        }

        /// <summary>
        ///     归一化到0和1之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Normalize01(this int t, int min, int max)
        {
            return max != min ? ((t - min).F() / (max - min)).Clamp01() : 0.0f;
        }

        /// <summary>
        ///     归一化到零向量和单位向量之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Normalize01(this Vector2 t, Vector2 min, Vector2 max)
        {
            return t.ForeachNumber(min, max, (num, minNum, maxNum) => num.Normalize01(minNum, maxNum));
        }

        /// <summary>
        ///     归一化到零向量和单位向量之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalize01(this Vector3 t, Vector3 min, Vector3 max)
        {
            return t.ForeachNumber(min, max, (num, minNum, maxNum) => num.Normalize01(minNum, maxNum));
        }

        /// <summary>
        ///     归一化到零向量和单位向量之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Normalize01(this Vector4 t, Vector4 min, Vector4 max)
        {
            return t.ForeachNumber(min, max, (num, minNum, maxNum) => num.Normalize01(minNum, maxNum));
        }

        /// <summary>
        ///     归一化到零向量和单位向量之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Normalize01(this Vector2Int t, Vector2Int min, Vector2Int max)
        {
            return t.ForeachNumber(min, max, (num, minNum, maxNum) => num.Normalize01(minNum, maxNum));
        }

        /// <summary>
        ///     归一化到零向量和单位向量之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalize01(this Vector3Int t, Vector3Int min, Vector3Int max)
        {
            return t.ForeachNumber(min, max, (num, minNum, maxNum) => num.Normalize01(minNum, maxNum));
        }

        /// <summary>
        ///     归一化到Color(0, 0, 0, 0)和Color(1, 1, 1, 1)之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Normalize01(this Color t, Color min, Color max)
        {
            return t.ForeachNumber(min, max, (num, minNum, maxNum) => num.Normalize01(minNum, maxNum));
        }

        #endregion

        #region Normalize To

        /// <summary>
        ///     从旧的最小最大值范围归一化到新的最小和最大值之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="newMin"></param>
        /// <param name="newMax"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NormalizeTo(this float t, float min, float max, float newMin, float newMax)
        {
            return LerpUnclamped(newMin, newMax, t.Normalize01(min, max));
        }

        /// <summary>
        ///     从旧的最小最大值范围归一化到新的最小和最大值之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="newMin"></param>
        /// <param name="newMax"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NormalizeTo(this double t, double min, double max, double newMin, double newMax)
        {
            return LerpUnclamped(newMin, newMax, t.Normalize01(min, max));
        }

        /// <summary>
        ///     从旧的最小最大值范围归一化到新的最小和最大值之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="newMin"></param>
        /// <param name="newMax"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NormalizeTo(this int t, int min, int max, int newMin, int newMax)
        {
            return LerpUnclamped(newMin, newMax, t.Normalize01(min, max));
        }

        /// <summary>
        ///     从旧的最小最大向量范围归一化到新的最小和最大向量之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="newMin"></param>
        /// <param name="newMax"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 NormalizeTo(this Vector2 t, Vector2 min, Vector2 max, Vector2 newMin,
            Vector2 newMax)
        {
            return LerpUnclamped(newMin, newMax, t.Normalize01(min, max));
        }

        /// <summary>
        ///     从旧的最小最大向量范围归一化到新的最小和最大向量之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="newMin"></param>
        /// <param name="newMax"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 NormalizeTo(this Vector3 t, Vector3 min, Vector3 max, Vector3 newMin,
            Vector3 newMax)
        {
            return LerpUnclamped(newMin, newMax, t.Normalize01(min, max));
        }

        /// <summary>
        ///     从旧的最小最大向量范围归一化到新的最小和最大向量之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="newMin"></param>
        /// <param name="newMax"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 NormalizeTo(this Vector4 t, Vector4 min, Vector4 max, Vector4 newMin,
            Vector4 newMax)
        {
            return LerpUnclamped(newMin, newMax, t.Normalize01(min, max));
        }

        /// <summary>
        ///     从旧的最小最大向量范围归一化到新的最小和最大向量之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="newMin"></param>
        /// <param name="newMax"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int NormalizeTo(this Vector2Int t, Vector2Int min, Vector2Int max,
            Vector2Int newMin, Vector2Int newMax)
        {
            return LerpUnclamped(newMin, newMax, t.Normalize01(min, max));
        }

        /// <summary>
        ///     从旧的最小最大向量范围归一化到新的最小和最大向量之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="newMin"></param>
        /// <param name="newMax"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int NormalizeTo(this Vector3Int t, Vector3Int min, Vector3Int max,
            Vector3Int newMin, Vector3Int newMax)
        {
            return LerpUnclamped(newMin, newMax, t.Normalize01(min, max));
        }

        /// <summary>
        ///     从旧的最小最大颜色范围归一化到新的最小和最大颜色之间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="newMin"></param>
        /// <param name="newMax"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color NormalizeTo(this Color t, Color min, Color max, Color newMin, Color newMax)
        {
            return LerpUnclamped(newMin, newMax, t.Normalize01(min, max));
        }

        #endregion
    }
}