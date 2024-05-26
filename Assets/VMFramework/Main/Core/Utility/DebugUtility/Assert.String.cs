using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static partial class Assert
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsNotNullOrEmpty(this string str, string name)
        {
            if (str.IsNullOrEmpty())
            {
                throw new ArgumentException($"{name} is null or empty");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsNotNullOrWhiteSpace(this string str, string name)
        {
            if (str.IsNullOrWhiteSpace())
            {
                throw new ArgumentException($"{name} is null or white space");
            }
        }
    }
}