using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Linq
{
    public static partial class LINQUtility
    {
        #region Pair With Next

        /// <summary>
        /// 将一个IEnumerable中的元素两两相邻地组合起来
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(T, T)> PairWithNext<T>(this IEnumerable<T> source)
        {
            var items = source.ToList();
            var previousItem = items.First();

            foreach (var currentItem in items.Skip(1))
            {
                yield return (previousItem, currentItem);
                previousItem = currentItem;
            }
        }

        /// <summary>
        /// 将一个IEnumerable中的元素两两相邻地组合起来
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="firstBoundary"></param>
        /// <param name="lastBoundary"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(T, T)> PairWithNext<T>(this IEnumerable<T> source,
            T firstBoundary, T lastBoundary)
        {
            var items = source.ToList();
            var previousItem = items.First();

            yield return (firstBoundary, previousItem);

            foreach (var currentItem in items.Skip(1))
            {
                yield return (previousItem, currentItem);
                previousItem = currentItem;
            }

            yield return (previousItem, lastBoundary);
        }

        #endregion

        #region Find Bounding

        /// <summary>
        /// 从一个排序好的数组中找到两个元素，使得目标值在这两个元素之间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="target"></param>
        /// <param name="sortedEnumerable"></param>
        /// <param name="extractBound"></param>
        /// <returns></returns>
        public static (T, T) FindBoundingElements<T, TTarget>(this TTarget target,
            IEnumerable<T> sortedEnumerable, Func<T, TTarget> extractBound)
            where TTarget : IComparable
            where T : class
        {
            T lastElement = null;

            foreach (var item in sortedEnumerable)
            {
                if (target.Below(extractBound(item)))
                {
                    return (lastElement, item);
                }

                lastElement = item;
            }

            return (lastElement, null);
        }

        /// <summary>
        /// 从一个排序好的数组中找到两个元素，使得目标值在这两个元素之间
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="target"></param>
        /// <param name="sortedEnumerable"></param>
        /// <param name="firstBoundary"></param>
        /// <param name="lastBoundary"></param>
        /// <returns></returns>
        public static (TTarget, TTarget) FindBoundingElements<TTarget>(
            this TTarget target, IEnumerable<TTarget> sortedEnumerable, 
            TTarget firstBoundary, TTarget lastBoundary)
            where TTarget : IComparable
        {
            TTarget lastElement = firstBoundary;

            foreach (var item in sortedEnumerable)
            {
                if (target.Below(item))
                {
                    return (lastElement, item);
                }

                lastElement = item;
            }

            return (lastElement, lastBoundary);
        }

        /// <summary>
        /// 从一个排序好的数组中找到两个Index，使得目标值在这两个Index对应的值之间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="target"></param>
        /// <param name="sortedEnumerable"></param>
        /// <param name="extractBound"></param>
        /// <returns></returns>
        public static (int, int) FindBoundingIndices<T, TTarget>(this TTarget target,
            IEnumerable<T> sortedEnumerable, Func<T, TTarget> extractBound)
            where TTarget : IComparable
        {
            int lastIndex = 0;
            foreach (var (index, item) in sortedEnumerable.Enumerate())
            {
                if (target.Below(extractBound(item)))
                {
                    return (index - 1, index);
                }

                lastIndex = index;
            }

            return (lastIndex, lastIndex + 1);
        }

        /// <summary>
        /// 从一个排序好的数组中找到两个Index，使得目标值在这两个Index对应的值之间
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="target"></param>
        /// <param name="sortedEnumerable"></param>
        /// <returns></returns>
        public static (int, int) FindBoundingIndices<TTarget>(this TTarget target,
            IEnumerable<TTarget> sortedEnumerable)
            where TTarget : IComparable
        {
            int lastIndex = 0;
            foreach (var (index, item) in sortedEnumerable.Enumerate())
            {
                if (target.Below(item))
                {
                    return (index - 1, index);
                }

                lastIndex = index;
            }

            return (lastIndex, lastIndex + 1);
        }

        #endregion
        
        #region Zip

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(T1, T2)> Zip<T1, T2>(
            this IEnumerable<T1> enumerableOne, IEnumerable<T2> enumerableTwo)
        {
            return enumerableOne.Zip(enumerableTwo, (t1, t2) => (t1, t2));
        }

        #endregion
    }
}