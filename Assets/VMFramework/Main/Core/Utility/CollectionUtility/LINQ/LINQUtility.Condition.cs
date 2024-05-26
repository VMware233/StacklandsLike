using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Linq
{
    public static partial class LINQUtility
    {
        #region Is Null Or Empty

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.Any() == false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty([NotNullWhen(false)] this IEnumerable enumerable)
        {
            return enumerable == null || enumerable.Cast<object>().Any() == false;
        }

        #endregion
        
        #region Is All Null

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAllNull<T>(this IEnumerable<T> enumerable)
            where T : class
        {
            return enumerable.All(item => item == null);
        }

        #endregion

        #region Is Null Or Empty Or All Null

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmptyOrAllNull<T>([NotNullWhen(false)] this IEnumerable<T> enumerable)
            where T : class
        {
            if (enumerable == null)
            {
                return true;
            }

            foreach (var item in enumerable)
            {
                if (item != null)
                {
                    return false;
                }
            }
            
            return true;
        }

        #endregion
        
        #region Contains

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsAll<T>(this IEnumerable<T> enumerable,
            IEnumerable<T> items)
        {
            return items.All(enumerable.Contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsAny<T>(this IEnumerable<T> enumerable,
            IEnumerable<T> items)
        {
            return items.Any(enumerable.Contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsSame<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ContainsSame(item => item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsSame<T, TSelector>(this IEnumerable<T> enumerable,
            Func<T, TSelector> selector)
        {
            var array = enumerable.ToArray();

            if (array.Length <= 1)
            {
                return false;
            }

            return array.Distinct(selector).Count() < array.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsNull<T>(this IEnumerable<T> enumerable)
            where T : class
        {
            if (enumerable == null)
            {
                return false;
            }

            return enumerable.Any(item => item == null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsNull<T, TSelector>(this IEnumerable<T> enumerable,
            Func<T, TSelector> selector) where T : class
        {
            if (enumerable == null)
            {
                return false;
            }

            return enumerable.Select(selector).Any(item => item == null);
        }

        #endregion

        #region Any & All

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyIs<T>(this IEnumerable<object> enumerable)
        {
            return enumerable.Any(item => item is T);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllIs<T>(this IEnumerable<object> enumerable)
        {
            return enumerable.All(item => item is T);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(this IEnumerable<bool> enumerable)
        {
            var result = false;
            foreach (var item in enumerable)
            {
                result |= item;
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this IEnumerable<bool> enumerable)
        {
            var result = true;
            foreach (var item in enumerable)
            {
                result &= item;
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<T>(this IEnumerable<T> enumerable,
            Func<int, T, bool> predicate)
        {
            foreach (var (i, item) in enumerable.Enumerate())
            {
                if (predicate(i, item))
                {
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All<T>(this IEnumerable<T> enumerable,
            Func<int, T, bool> predicate)
        {
            foreach (var (i, item) in enumerable.Enumerate())
            {
                if (predicate(i, item) == false)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
        
        #region Where

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> enumerable)
            where T : class
        {
            return enumerable.Where(item => item != null);
        }

        #endregion
    }
}