using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static partial class ReflectionUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ConvertTo<T>(this object value) => (T)Convert.ChangeType(value, typeof(T));
    }
}