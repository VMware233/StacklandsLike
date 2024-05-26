using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Linq
{
    public static partial class LINQUtility
    {
        #region Distinct

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TSource> Distinct<TSource, TSelector>(
            this IEnumerable<TSource> enumerable, Func<TSource, TSelector> selector)
        {
            var seenKeys = new HashSet<TSelector>();
            foreach (TSource element in enumerable)
            {
                if (seenKeys.Add(selector(element)))
                {
                    yield return element;
                }
            }
        }

        #endregion

        #region Enumerate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(int index, T)> Enumerate<T>(
            this IEnumerable<T> array, int indexOffset = 0)
        {
            int index = 0;
            foreach (var item in array)
            {
                yield return (index + indexOffset, item);
                index++;
            }
        }

        #endregion

        #region First && Last

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> FirstItems<T>(this IEnumerable<T> enumerable,
            int totalCount)
        {
            int count = 0;
            foreach (T item in enumerable)
            {
                if (count < totalCount)
                {
                    yield return item;
                    count++;
                }
                else
                {
                    yield break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable,
            int startIndex) where T : class
        {
            return enumerable.FirstOrDefault(item => item != null, startIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable,
            Func<T, bool> selector, int startIndex)
        {
            return enumerable.FirstOrDefault((i, item) =>
                i >= startIndex && selector(item));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable,
            Func<int, T, bool> selector,
            int indexOffset = 0)
        {
            foreach (var (i, item) in enumerable.Enumerate(indexOffset))
            {
                if (selector(i, item))
                {
                    return item;
                }
            }

            return default;
        }

        #endregion

        #region Select

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> Select<T, TResult>(
            this IEnumerable<T> enumerable)
        {
            foreach (var item in enumerable)
            {
                if (item is TResult result)
                {
                    yield return result;
                }
            }
        }

        #endregion

        #region Join

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Join<T>(this IEnumerable<T> enumerable, T sep)
        {
            foreach (var (i, item) in enumerable.Enumerate())
            {
                if (i > 0)
                {
                    yield return sep;
                }

                yield return item;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Join(this IEnumerable<float> enumerable, float sep)
        {
            return enumerable.Join<float>(sep).Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Join(this IEnumerable<int> enumerable, int sep)
        {
            return enumerable.Join<int>(sep).Sum();
        }

        #endregion

        #region Sum

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sum<T>(this IEnumerable<T> enumerable,
            Func<T, int> selector, int fromIndex, int endIndex)
        {
            int result = 0;
            foreach (var (index, item) in enumerable.Enumerate())
            {
                if (index.BetweenInclusive(fromIndex, endIndex))
                {
                    result += selector(item);
                }
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sum<T>(this IEnumerable<T> enumerable,
            Func<T, float> selector, int fromIndex, int endIndex)
        {
            float result = 0;
            foreach (var (index, item) in enumerable.Enumerate())
            {
                if (index.BetweenInclusive(fromIndex, endIndex))
                {
                    result += selector(item);
                }
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sum<T>(this IEnumerable<T> enumerable,
            Func<T, int> selector, int length)
        {
            return enumerable.Sum(selector, 0, length - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sum<T>(this IEnumerable<T> enumerable,
            Func<T, float> selector, int length)
        {
            return enumerable.Sum(selector, 0, length - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sum(this IEnumerable<int> enumerable, int fromIndex,
            int endIndex)
        {
            return enumerable.Sum(item => item, fromIndex, endIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sum(this IEnumerable<float> enumerable, int fromIndex,
            int endIndex)
        {
            return enumerable.Sum(item => item, fromIndex, endIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sum(this IEnumerable<int> enumerable, int length)
        {
            return enumerable.Sum(item => item, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sum(this IEnumerable<float> enumerable, int length)
        {
            return enumerable.Sum(item => item, length);
        }

        #endregion

        #region Sort

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Sort<T>(this List<T> list, Func<T, int> weightGetter)
        {
            list.Sort((item1, item2) =>
                weightGetter(item1).CompareTo(weightGetter(item2)));
        }

        #endregion
    }
}
