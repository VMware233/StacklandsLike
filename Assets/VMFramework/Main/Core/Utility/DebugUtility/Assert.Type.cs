using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static partial class Assert
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsClass(this Type type, string name)
        {
            if (type.IsClass == false)
            {
                throw new ArgumentException($"{name} is not a class");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsInterface(this Type type, string name)
        {
            if (type.IsInterface == false)
            {
                throw new ArgumentException($"{name} is not a class");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsDerivedFrom(this Type type, Type parentType,
            bool includingSelf, bool includingGeneric)
        {
            if (type.IsDerivedFrom(parentType, includingSelf,
                    includingGeneric) == false)
            {
                throw new ArgumentException(
                    $"{type} is not derived from {parentType}");
            }
        }
    }
}