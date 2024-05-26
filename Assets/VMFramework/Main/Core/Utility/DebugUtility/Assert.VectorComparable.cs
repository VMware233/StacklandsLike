using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static partial class Assert
    {
        #region Vector3Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAbove(this Vector3Int vector, Vector3Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberAbove(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number below or equal to {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAbove(this Vector3Int vector, Vector3Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AllNumberAbove(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number above {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelow(this Vector3Int vector, Vector3Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberBelow(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number above or equal to {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelow(this Vector3Int vector, Vector3Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AllNumberBelow(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number below {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAboveOrEqual(this Vector3Int vector, Vector3Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberAboveOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number below {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAboveOrEqual(this Vector3Int vector, Vector3Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AllNumberAboveOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number above or equal to {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelowOrEqual(this Vector3Int vector, Vector3Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberBelowOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number above {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelowOrEqual(this Vector3Int vector, Vector3Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AllNumberBelowOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number below or equal to {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAbove(this Vector3Int vector, Vector3Int comparison,
            string vectorName)
        {
            if (vector.AnyNumberAbove(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number below or equal to {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAbove(this Vector3Int vector, Vector3Int comparison,
            string vectorName)
        {
            if (vector.AllNumberAbove(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number above {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelow(this Vector3Int vector, Vector3Int comparison,
            string vectorName)
        {
            if (vector.AnyNumberBelow(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number above or equal to {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelow(this Vector3Int vector, Vector3Int comparison,
            string vectorName)
        {
            if (vector.AllNumberBelow(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number below {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAboveOrEqual(this Vector3Int vector, Vector3Int comparison,
            string vectorName)
        {
            if (vector.AnyNumberAboveOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number below {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAboveOrEqual(this Vector3Int vector, Vector3Int comparison,
            string vectorName)
        {
            if (vector.AllNumberAboveOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number above or equal to {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelowOrEqual(this Vector3Int vector, Vector3Int comparison,
            string vectorName)
        {
            if (vector.AnyNumberBelowOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number above {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelowOrEqual(this Vector3Int vector, Vector3Int comparison,
            string vectorName)
        {
            if (vector.AllNumberBelowOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number below or equal to {comparison}");
            }
        }

        #endregion

        #region Vector3

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAbove(this Vector3 vector, Vector3 comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberAbove(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number below or equal to {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAbove(this Vector3 vector, Vector3 comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AllNumberAbove(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number above {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelow(this Vector3 vector, Vector3 comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberBelow(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number above or equal to {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelow(this Vector3 vector, Vector3 comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AllNumberBelow(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number below {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAboveOrEqual(this Vector3 vector, Vector3 comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberAboveOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number below {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAboveOrEqual(this Vector3 vector, Vector3 comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AllNumberAboveOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number above or equal to {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelowOrEqual(this Vector3 vector, Vector3 comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberBelowOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number above {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelowOrEqual(this Vector3 vector, Vector3 comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AllNumberBelowOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number below or equal to {comparisonName}");
            }
        }

        // Simplified Methods (No comparisonName parameter)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAbove(this Vector3 vector, Vector3 comparison,
            string vectorName)
        {
            if (vector.AnyNumberAbove(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number below or equal to {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAbove(this Vector3 vector, Vector3 comparison,
            string vectorName)
        {
            if (vector.AllNumberAbove(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number above {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelow(this Vector3 vector, Vector3 comparison,
            string vectorName)
        {
            if (vector.AnyNumberBelow(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number above or equal to {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelow(this Vector3 vector, Vector3 comparison,
            string vectorName)
        {
            if (vector.AllNumberBelow(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number below {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAboveOrEqual(this Vector3 vector, Vector3 comparison,
            string vectorName)
        {
            if (vector.AnyNumberAboveOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number below {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAboveOrEqual(this Vector3 vector, Vector3 comparison,
            string vectorName)
        {
            if (vector.AllNumberAboveOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number above or equal to {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelowOrEqual(this Vector3 vector, Vector3 comparison,
            string vectorName)
        {
            if (vector.AnyNumberBelowOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all number above {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelowOrEqual(this Vector3 vector, Vector3 comparison,
            string vectorName)
        {
            if (vector.AllNumberBelowOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all number below or equal to {comparison}");
            }
        }

        #endregion

        #region Vector2Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAbove(this Vector2Int vector, Vector2Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberAbove(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers below or equal to {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAbove(this Vector2Int vector, Vector2Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AllNumberAbove(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers above {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelow(this Vector2Int vector, Vector2Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberBelow(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers above or equal to {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelow(this Vector2Int vector, Vector2Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AllNumberBelow(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers below {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAboveOrEqual(this Vector2Int vector, Vector2Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberAboveOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers below {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAboveOrEqual(this Vector2Int vector, Vector2Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AllNumberAboveOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers above or equal to {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelowOrEqual(this Vector2Int vector, Vector2Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberBelowOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers above {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelowOrEqual(this Vector2Int vector, Vector2Int comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AllNumberBelowOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers below or equal to {comparisonName}");
            }
        }

        // Simplified Methods (No comparisonName parameter)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAbove(this Vector2Int vector, Vector2Int comparison,
            string vectorName)
        {
            if (vector.AnyNumberAbove(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers below or equal to {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAbove(this Vector2Int vector, Vector2Int comparison,
            string vectorName)
        {
            if (vector.AllNumberAbove(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers above {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelow(this Vector2Int vector, Vector2Int comparison,
            string vectorName)
        {
            if (vector.AnyNumberBelow(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers above or equal to {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelow(this Vector2Int vector, Vector2Int comparison,
            string vectorName)
        {
            if (vector.AllNumberBelow(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers below {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAboveOrEqual(this Vector2Int vector, Vector2Int comparison,
            string vectorName)
        {
            if (vector.AnyNumberAboveOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers below {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAboveOrEqual(this Vector2Int vector, Vector2Int comparison,
            string vectorName)
        {
            if (vector.AllNumberAboveOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers above or equal to {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelowOrEqual(this Vector2Int vector, Vector2Int comparison,
            string vectorName)
        {
            if (vector.AnyNumberBelowOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers above {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelowOrEqual(this Vector2Int vector, Vector2Int comparison,
            string vectorName)
        {
            if (vector.AllNumberBelowOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers below or equal to {comparison}");
            }
        }

        #endregion

        #region Vector2

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAbove(this Vector2 vector, Vector2 comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberAbove(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers below or equal to {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAbove(this Vector2 vector, Vector2 comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AllNumberAbove(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers above {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelow(this Vector2 vector, Vector2 comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberBelow(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers above or equal to {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelow(this Vector2 vector, Vector2 comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AllNumberBelow(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers below {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAboveOrEqual(this Vector2 vector, Vector2 comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberAboveOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers below {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAboveOrEqual(this Vector2 vector, Vector2 comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AllNumberAboveOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers above or equal to {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelowOrEqual(this Vector2 vector, Vector2 comparison,
            string vectorName, string comparisonName)
        {
            if (vector.AnyNumberBelowOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers above {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelowOrEqual(this Vector2 vector, Vector2 comparison,
                    string vectorName, string comparisonName)
        {
            if (vector.AllNumberBelowOrEqual(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers below or equal to {comparisonName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAbove(this Vector2 vector, Vector2 comparison,
            string vectorName)
        {
            if (vector.AnyNumberAbove(comparison) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers below or equal to {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAbove(this Vector2 vector, Vector2 comparison,
                    string vectorName)
        {
            if (vector.AllNumberAbove(comparison) is false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers above {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelow(this Vector2 vector, Vector2 comparison,
            string vectorName)
        {
            if (vector.AnyNumberBelow(comparison) is false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers above or equal to {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelow(this Vector2 vector, Vector2 comparison,
            string vectorName)
        {
            if (vector.AllNumberBelow(comparison) is false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers below {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberAboveOrEqual(this Vector2 vector, Vector2 comparison,
            string vectorName)
        {
            if (vector.AnyNumberAboveOrEqual(comparison) is false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers below {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberAboveOrEqual(this Vector2 vector, Vector2 comparison,
            string vectorName)
        {
            if (vector.AllNumberAboveOrEqual(comparison) is false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers above or equal to {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyNumberBelowOrEqual(this Vector2 vector, Vector2 comparison,
            string vectorName)
        {
            if (vector.AnyNumberBelowOrEqual(comparison) is false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is all numbers above {comparison}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllNumberBelowOrEqual(this Vector2 vector, Vector2 comparison,
            string vectorName)
        {
            if (vector.AllNumberBelowOrEqual(comparison) is false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{vectorName} is not all numbers below or equal to {comparison}");
            }
        }

        #endregion
    }
}