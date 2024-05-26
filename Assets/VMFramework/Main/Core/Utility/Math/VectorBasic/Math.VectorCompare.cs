using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static partial class Math
    {
        #region Compare All

        #region AllBelowOrEqual

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual(this Color a, float comparison)
        {
            return a.r <= comparison && a.g <= comparison && a.b <= comparison && a.a <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual(this Vector4 a, float comparison)
        {
            return a.x <= comparison && a.y <= comparison && a.z <= comparison && a.w <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual(this Vector3Int a, int comparison)
        {
            return a.x <= comparison && a.y <= comparison && a.z <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual(this Vector3 a, float comparison)
        {
            return a.x <= comparison && a.y <= comparison && a.z <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual(this Vector2Int a, int comparison)
        {
            return a.x <= comparison && a.y <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual(this Vector2 a, float comparison)
        {
            return a.x <= comparison && a.y <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual(this Color a, Color comparison)
        {
            return a.r <= comparison.r && a.g <= comparison.g && a.b <= comparison.b && a.a <= comparison.a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual(this Vector4 a, Vector4 comparison)
        {
            return a.x <= comparison.x && a.y <= comparison.y && a.z <= comparison.z && a.w <= comparison.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual(this Vector3Int a, Vector3Int comparison)
        {
            return a.x <= comparison.x && a.y <= comparison.y && a.z <= comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual(this Vector3 a, Vector3 comparison)
        {
            return a.x <= comparison.x && a.y <= comparison.y && a.z <= comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual(this Vector2Int a, Vector2Int comparison)
        {
            return a.x <= comparison.x && a.y <= comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual(this Vector2 a, Vector2 comparison)
        {
            return a.x <= comparison.x && a.y <= comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual(this double a, double comparison)
        {
            return a <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual(this float a, float comparison)
        {
            return a <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelowOrEqual(this int a, int comparison)
        {
            return a <= comparison;
        }

        #endregion

        #region AllBelow

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow(this Color a, float comparison)
        {
            return a.r < comparison && a.g < comparison && a.b < comparison && a.a < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow(this Vector4 a, float comparison)
        {
            return a.x < comparison && a.y < comparison && a.z < comparison && a.w < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow(this Vector3Int a, int comparison)
        {
            return a.x < comparison && a.y < comparison && a.z < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow(this Vector3 a, float comparison)
        {
            return a.x < comparison && a.y < comparison && a.z < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow(this Vector2Int a, int comparison)
        {
            return a.x < comparison && a.y < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow(this Vector2 a, float comparison)
        {
            return a.x < comparison && a.y < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow(this Color a, Color comparison)
        {
            return a.r < comparison.r && a.g < comparison.g && a.b < comparison.b && a.a < comparison.a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow(this Vector4 a, Vector4 comparison)
        {
            return a.x < comparison.x && a.y < comparison.y && a.z < comparison.z && a.w < comparison.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow(this Vector3Int a, Vector3Int comparison)
        {
            return a.x < comparison.x && a.y < comparison.y && a.z < comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow(this Vector3 a, Vector3 comparison)
        {
            return a.x < comparison.x && a.y < comparison.y && a.z < comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow(this Vector2Int a, Vector2Int comparison)
        {
            return a.x < comparison.x && a.y < comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow(this Vector2 a, Vector2 comparison)
        {
            return a.x < comparison.x && a.y < comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow(this double a, double comparison)
        {
            return a < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow(this float a, float comparison)
        {
            return a < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberBelow(this int a, int comparison)
        {
            return a < comparison;
        }

        #endregion

        #region AllAboveOrEqual

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual(this Color a, float comparison)
        {
            return a.r >= comparison && a.g >= comparison && a.b >= comparison && a.a >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual(this Vector4 a, float comparison)
        {
            return a.x >= comparison && a.y >= comparison && a.z >= comparison && a.w >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual(this Vector3Int a, int comparison)
        {
            return a.x >= comparison && a.y >= comparison && a.z >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual(this Vector3 a, float comparison)
        {
            return a.x >= comparison && a.y >= comparison && a.z >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual(this Vector2Int a, int comparison)
        {
            return a.x >= comparison && a.y >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual(this Vector2 a, float comparison)
        {
            return a.x >= comparison && a.y >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual(this Color a, Color comparison)
        {
            return a.r >= comparison.r && a.g >= comparison.g && a.b >= comparison.b && a.a >= comparison.a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual(this Vector4 a, Vector4 comparison)
        {
            return a.x >= comparison.x && a.y >= comparison.y && a.z >= comparison.z && a.w >= comparison.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual(this Vector3Int a, Vector3Int comparison)
        {
            return a.x >= comparison.x && a.y >= comparison.y && a.z >= comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual(this Vector3 a, Vector3 comparison)
        {
            return a.x >= comparison.x && a.y >= comparison.y && a.z >= comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual(this Vector2Int a, Vector2Int comparison)
        {
            return a.x >= comparison.x && a.y >= comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual(this Vector2 a, Vector2 comparison)
        {
            return a.x >= comparison.x && a.y >= comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual(this double a, double comparison)
        {
            return a >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual(this float a, float comparison)
        {
            return a >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAboveOrEqual(this int a, int comparison)
        {
            return a >= comparison;
        }

        #endregion

        #region AllAbove

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove(this Color a, float comparison)
        {
            return a.r > comparison && a.g > comparison && a.b > comparison && a.a > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove(this Vector4 a, float comparison)
        {
            return a.x > comparison && a.y > comparison && a.z > comparison && a.w > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove(this Vector3Int a, int comparison)
        {
            return a.x > comparison && a.y > comparison && a.z > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove(this Vector3 a, float comparison)
        {
            return a.x > comparison && a.y > comparison && a.z > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove(this Vector2Int a, int comparison)
        {
            return a.x > comparison && a.y > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove(this Vector2 a, float comparison)
        {
            return a.x > comparison && a.y > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove(this Color a, Color comparison)
        {
            return a.r > comparison.r && a.g > comparison.g && a.b > comparison.b && a.a > comparison.a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove(this Vector4 a, Vector4 comparison)
        {
            return a.x > comparison.x && a.y > comparison.y && a.z > comparison.z && a.w > comparison.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove(this Vector3Int a, Vector3Int comparison)
        {
            return a.x > comparison.x && a.y > comparison.y && a.z > comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove(this Vector3 a, Vector3 comparison)
        {
            return a.x > comparison.x && a.y > comparison.y && a.z > comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove(this Vector2Int a, Vector2Int comparison)
        {
            return a.x > comparison.x && a.y > comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove(this Vector2 a, Vector2 comparison)
        {
            return a.x > comparison.x && a.y > comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove(this double a, double comparison)
        {
            return a > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove(this float a, float comparison)
        {
            return a > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllNumberAbove(this int a, int comparison)
        {
            return a > comparison;
        }

        #endregion

        #endregion

        #region Compare Any

        #region AnyBelowOrEqual

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual(this Color a, float comparison)
        {
            return a.r <= comparison || a.g <= comparison || a.b <= comparison || a.a <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual(this Vector4 a, float comparison)
        {
            return a.x <= comparison || a.y <= comparison || a.z <= comparison || a.w <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual(this Vector3Int a, int comparison)
        {
            return a.x <= comparison || a.y <= comparison || a.z <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual(this Vector3 a, float comparison)
        {
            return a.x <= comparison || a.y <= comparison || a.z <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual(this Vector2Int a, int comparison)
        {
            return a.x <= comparison || a.y <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual(this Vector2 a, float comparison)
        {
            return a.x <= comparison || a.y <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual(this Color a, Color comparison)
        {
            return a.r <= comparison.r || a.g <= comparison.g || a.b <= comparison.b || a.a <= comparison.a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual(this Vector4 a, Vector4 comparison)
        {
            return a.x <= comparison.x || a.y <= comparison.y || a.z <= comparison.z || a.w <= comparison.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual(this Vector3Int a, Vector3Int comparison)
        {
            return a.x <= comparison.x || a.y <= comparison.y || a.z <= comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual(this Vector3 a, Vector3 comparison)
        {
            return a.x <= comparison.x || a.y <= comparison.y || a.z <= comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual(this Vector2Int a, Vector2Int comparison)
        {
            return a.x <= comparison.x || a.y <= comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual(this Vector2 a, Vector2 comparison)
        {
            return a.x <= comparison.x || a.y <= comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual(this double a, double comparison)
        {
            return a <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual(this float a, float comparison)
        {
            return a <= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelowOrEqual(this int a, int comparison)
        {
            return a <= comparison;
        }

        #endregion

        #region AnyBelow

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow(this Color a, float comparison)
        {
            return a.r < comparison || a.g < comparison || a.b < comparison || a.a < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow(this Vector4 a, float comparison)
        {
            return a.x < comparison || a.y < comparison || a.z < comparison || a.w < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow(this Vector3Int a, int comparison)
        {
            return a.x < comparison || a.y < comparison || a.z < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow(this Vector3 a, float comparison)
        {
            return a.x < comparison || a.y < comparison || a.z < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow(this Vector2Int a, int comparison)
        {
            return a.x < comparison || a.y < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow(this Vector2 a, float comparison)
        {
            return a.x < comparison || a.y < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow(this Color a, Color comparison)
        {
            return a.r < comparison.r || a.g < comparison.g || a.b < comparison.b || a.a < comparison.a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow(this Vector4 a, Vector4 comparison)
        {
            return a.x < comparison.x || a.y < comparison.y || a.z < comparison.z || a.w < comparison.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow(this Vector3Int a, Vector3Int comparison)
        {
            return a.x < comparison.x || a.y < comparison.y || a.z < comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow(this Vector3 a, Vector3 comparison)
        {
            return a.x < comparison.x || a.y < comparison.y || a.z < comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow(this Vector2Int a, Vector2Int comparison)
        {
            return a.x < comparison.x || a.y < comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow(this Vector2 a, Vector2 comparison)
        {
            return a.x < comparison.x || a.y < comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow(this double a, double comparison)
        {
            return a < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow(this float a, float comparison)
        {
            return a < comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberBelow(this int a, int comparison)
        {
            return a < comparison;
        }

        #endregion

        #region AnyAboveOrEqual

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual(this Color a, float comparison)
        {
            return a.r >= comparison || a.g >= comparison || a.b >= comparison || a.a >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual(this Vector4 a, float comparison)
        {
            return a.x >= comparison || a.y >= comparison || a.z >= comparison || a.w >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual(this Vector3Int a, int comparison)
        {
            return a.x >= comparison || a.y >= comparison || a.z >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual(this Vector3 a, float comparison)
        {
            return a.x >= comparison || a.y >= comparison || a.z >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual(this Vector2Int a, int comparison)
        {
            return a.x >= comparison || a.y >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual(this Vector2 a, float comparison)
        {
            return a.x >= comparison || a.y >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual(this Color a, Color comparison)
        {
            return a.r >= comparison.r || a.g >= comparison.g || a.b >= comparison.b || a.a >= comparison.a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual(this Vector4 a, Vector4 comparison)
        {
            return a.x >= comparison.x || a.y >= comparison.y || a.z >= comparison.z || a.w >= comparison.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual(this Vector3Int a, Vector3Int comparison)
        {
            return a.x >= comparison.x || a.y >= comparison.y || a.z >= comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual(this Vector3 a, Vector3 comparison)
        {
            return a.x >= comparison.x || a.y >= comparison.y || a.z >= comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual(this Vector2Int a, Vector2Int comparison)
        {
            return a.x >= comparison.x || a.y >= comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual(this Vector2 a, Vector2 comparison)
        {
            return a.x >= comparison.x || a.y >= comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual(this double a, double comparison)
        {
            return a >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual(this float a, float comparison)
        {
            return a >= comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAboveOrEqual(this int a, int comparison)
        {
            return a >= comparison;
        }

        #endregion

        #region AnyAbove

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove(this Color a, float comparison)
        {
            return a.r > comparison || a.g > comparison || a.b > comparison || a.a > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove(this Vector4 a, float comparison)
        {
            return a.x > comparison || a.y > comparison || a.z > comparison || a.w > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove(this Vector3Int a, int comparison)
        {
            return a.x > comparison || a.y > comparison || a.z > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove(this Vector3 a, float comparison)
        {
            return a.x > comparison || a.y > comparison || a.z > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove(this Vector2Int a, int comparison)
        {
            return a.x > comparison || a.y > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove(this Vector2 a, float comparison)
        {
            return a.x > comparison || a.y > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove(this Color a, Color comparison)
        {
            return a.r > comparison.r || a.g > comparison.g || a.b > comparison.b || a.a > comparison.a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove(this Vector4 a, Vector4 comparison)
        {
            return a.x > comparison.x || a.y > comparison.y || a.z > comparison.z || a.w > comparison.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove(this Vector3Int a, Vector3Int comparison)
        {
            return a.x > comparison.x || a.y > comparison.y || a.z > comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove(this Vector3 a, Vector3 comparison)
        {
            return a.x > comparison.x || a.y > comparison.y || a.z > comparison.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove(this Vector2Int a, Vector2Int comparison)
        {
            return a.x > comparison.x || a.y > comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove(this Vector2 a, Vector2 comparison)
        {
            return a.x > comparison.x || a.y > comparison.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove(this double a, double comparison)
        {
            return a > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove(this float a, float comparison)
        {
            return a > comparison;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyNumberAbove(this int a, int comparison)
        {
            return a > comparison;
        }

        #endregion

        #endregion
    }
}
