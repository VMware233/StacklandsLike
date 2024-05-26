using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static partial class Assert
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAbove<T>(this T comparable, T value, string comparableName,
            string valueName) where T : IComparable
        {
            if (comparable.Above(value) == false)
            {
                throw new ArgumentOutOfRangeException($"{comparableName} is not above {valueName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAboveOrEqual<T>(this T comparable, T value, string comparableName,
            string valueName) where T : IComparable
        {
            if (comparable.AboveOrEqual(value) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{comparableName} is not above or equal to {valueName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsBelow<T>(this T comparable, T value, string comparableName,
            string valueName) where T : IComparable
        {
            if (comparable.Below(value) == false)
            {
                throw new ArgumentOutOfRangeException($"{comparableName} is not below {valueName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsBelowOrEqual<T>(this T comparable, T value, string comparableName,
            string valueName) where T : IComparable
        {
            if (comparable.BelowOrEqual(value) == false)
            {
                throw new ArgumentOutOfRangeException(
                    $"{comparableName} is not below or equal to {valueName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAbove<T>(this T comparable, T value, string comparableName)
            where T : IComparable
        {
            comparable.AssertIsAbove(value, comparableName, value.ToString());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAboveOrEqual<T>(this T comparable, T value, string comparableName)
            where T : IComparable
        {
            comparable.AssertIsAboveOrEqual(value, comparableName, value.ToString());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsBelow<T>(this T comparable, T value, string comparableName)
            where T : IComparable
        {
            comparable.AssertIsBelow(value, comparableName, value.ToString());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsBelowOrEqual<T>(this T comparable, T value, string comparableName)
            where T : IComparable
        {
            comparable.AssertIsBelowOrEqual(value, comparableName, value.ToString());
        }
    }
}