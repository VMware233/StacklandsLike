using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class ReflectionLINQExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> ExcludeAbstract(this IEnumerable<Type> types)
        {
            return types.Where(type => type.IsAbstract == false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> ExcludeAbstractAndInterface(this IEnumerable<Type> types)
        {
            return types.Where(type => type.IsAbstract == false && type.IsInterface == false);
        }
    }
}