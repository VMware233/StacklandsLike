using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core.Linq;

namespace VMFramework.Core
{
    public static partial class Assert
    {
        #region Is & Is Not

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsNot<T>(this T obj, T target, string objName,
            string targetName)
        {
            if (obj == null)
            {
                if (target == null)
                {
                    throw new NullReferenceException($"{objName} is {targetName}");
                }

                return;
            }
            if (obj.Equals(target))
            {
                throw new ArgumentException($"{objName} is {targetName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsNot<T>(this T obj, T target, string objName)
        {
            if (obj == null)
            {
                if (target == null)
                {
                    throw new NullReferenceException($"{objName} is Null");
                }

                return;
            }
            if (obj.Equals(target))
            {
                throw new ArgumentException($"{objName} is {target}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIs<T>(this T obj, T target, string objName,
            string targetName)
        {
            if (obj == null)
            {
                if (target != null)
                {
                    throw new NullReferenceException(
                        $"{objName} is not {targetName}, is Null");
                }

                return;
            }
            if (obj.Equals(target) == false)
            {
                throw new ArgumentException($"{objName} is not {targetName}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIs<T>(this T obj, T target, string objName)
        {
            if (obj == null)
            {
                if (target != null)
                {
                    throw new NullReferenceException(
                        $"{objName} is not {target}, is Null");
                }

                return;
            }
            if (obj.Equals(target) == false)
            {
                throw new ArgumentException($"{objName} is not {target}");
            }
        }

        #endregion

        #region Is Null

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsNotNull<T>(this T obj, string name)
        {
            if (obj == null)
            {
                throw new NullReferenceException($"{name} is null");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsNull<T>(this T obj, string name)
        {
            if (obj != null)
            {
                throw new NullReferenceException($"{name} is not null");
            }
        }

        #endregion

        #region Enumerable

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsNotNullOrEmpty<T>(this IEnumerable<T> enumerable, string name)
        {
            if (enumerable.IsNullOrEmpty())
            {
                throw new ArgumentException($"{name} is null or empty");
            }
        }

        #endregion

        #region Is True Or False

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsTrue(this bool value, string name)
        {
            if (value == false)
            {
                throw new ArgumentException($"{name} is not true");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsFalse(this bool value, string name)
        {
            if (value)
            {
                throw new ArgumentException($"{name} is not false");
            }
        }

        #endregion
    }
}
