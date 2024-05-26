using System;
using System.Runtime.CompilerServices;
using EnumsNET;

namespace VMFramework.Core
{
    public static partial class Assert
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsSingleFlag<TEnum>(this TEnum flag, string name)
            where TEnum : struct, Enum
        {
            if (flag.GetFlagCount() != 1)
            {
                throw new ArgumentException(
                    $"{name} is not a single flag, it has {flag.GetFlagCount()} flags.");
            }
        }
    }
}