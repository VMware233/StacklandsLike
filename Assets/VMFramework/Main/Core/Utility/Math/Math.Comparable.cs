using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class ComparableUtility
    {
        #region Min Select

        /// <summary>
        /// 从enumerable中选择出扔到selector中产生的值里最小的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static T SelectMin<T, TSelector>(this IEnumerable<T> enumerable,
            Func<T, TSelector> selector)
            where TSelector : IComparable<TSelector>
        {
            int count = 0;
            TSelector minValue = default;
            T minItem = default;

            foreach (var item in enumerable)
            {
                if (count == 0)
                {
                    minValue = selector(item);
                    minItem = item;
                }
                else if (selector(item).CompareTo(minValue) < 0)
                {
                    minValue = selector(item);
                    minItem = item;
                }

                count++;
            }

            return minItem;
        }

        /// <summary>
        /// 从enumerable中选择出扔到selector中产生的值里最大的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static T SelectMax<T, TSelector>(this IEnumerable<T> enumerable,
            Func<T, TSelector> selector) where TSelector : IComparable<TSelector>
        {
            int count = 0;
            TSelector maxValue = default;
            T maxItem = default;

            foreach (var item in enumerable)
            {
                if (count == 0)
                {
                    maxValue = selector(item);
                    maxItem = item;
                }
                else if (selector(item).CompareTo(maxValue) > 0)
                {
                    maxValue = selector(item);
                    maxItem = item;
                }

                count++;
            }

            return maxItem;
        }

        #endregion

        #region Select Min Or Max Group

        /// <summary>
        /// 从enumerable中选择出扔到selector中产生的值里最小的所对应的元素组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<T> SelectMinGroup<T, TSelector>(
            this IEnumerable<T> enumerable,
            Func<T, TSelector> selector) where TSelector : IComparable
        {
            int count = 0;
            TSelector minValue = default;
            List<T> minItems = new();

            foreach (var item in enumerable)
            {
                if (count == 0)
                {
                    minValue = selector(item);
                    minItems.Add(item);
                }
                else if (selector(item).CompareTo(minValue) < 0)
                {
                    minValue = selector(item);
                    minItems.Clear();
                    minItems.Add(item);
                }
                else if (selector(item).CompareTo(minValue) == 0)
                {
                    minItems.Add(item);
                }

                count++;
            }

            return minItems;
        }

        /// <summary>
        /// 从enumerable中选择出扔到selector中产生的值里最大的所对应的元素组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<T> SelectMaxGroup<T, TSelector>(
            this IEnumerable<T> enumerable,
            Func<T, TSelector> selector) where TSelector : IComparable
        {
            int count = 0;
            TSelector maxValue = default;
            List<T> maxItems = new();

            foreach (var item in enumerable)
            {
                if (count == 0)
                {
                    maxValue = selector(item);
                    maxItems.Add(item);
                }
                else if (selector(item).CompareTo(maxValue) > 0)
                {
                    maxValue = selector(item);
                    maxItems.Clear();
                    maxItems.Add(item);
                }
                else if (selector(item).CompareTo(maxValue) == 0)
                {
                    maxItems.Add(item);
                }

                count++;
            }

            return maxItems;
        }

        #endregion

        #region Compare

        /// <summary>
        /// return true if num is in range [min, max]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="num"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BetweenInclusive<T>(this T num, T min, T max)
            where T : IComparable
        {
            return num.CompareTo(min) >= 0 && num.CompareTo(max) <= 0;
        }

        /// <summary>
        /// return true if num is in range (min, max)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="num"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BetweenExclusive<T>(this T num, T min, T max)
            where T : IComparable
        {
            return num.CompareTo(min) > 0 && num.CompareTo(max) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Below<T>(this T num, T comparison) where T : IComparable
        {
            return num.CompareTo(comparison) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BelowOrEqual<T>(this T num, T comparison)
            where T : IComparable
        {
            return num.CompareTo(comparison) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Above<T>(this T num, T comparison) where T : IComparable
        {
            return num.CompareTo(comparison) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AboveOrEqual<T>(this T num, T comparison)
            where T : IComparable
        {
            return num.CompareTo(comparison) >= 0;
        }

        #endregion

        #region Get Indices Of Min/Max Values

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetIndicesOfMaxValues<TItem, TValue>(
            this IList<TItem> list, Func<TItem, TValue> selector)
            where TValue : IComparable
        {
            return list.GetAllIndices()
                .SelectMaxGroup(index => selector(list[index]));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetIndicesOfMinValues<TItem, TValue>(
            this IList<TItem> list, Func<TItem, TValue> selector)
            where TValue : IComparable
        {
            return list.GetAllIndices()
                .SelectMinGroup(index => selector(list[index]));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetIndicesOfMaxValues<TValue>(
            this IList<TValue> list)
            where TValue : IComparable
        {
            return list.GetAllIndices().SelectMaxGroup(index => list[index]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetIndicesOfMinValues<TValue>(
            this IList<TValue> list)
            where TValue : IComparable
        {
            return list.GetAllIndices().SelectMinGroup(index => list[index]);
        }

        #endregion

        #region Two Max Values

        public static (T max, T secondMax) TwoMaxValues<T>(this IEnumerable<T> enumerable)
            where T : IComparable<T>
        {
            T max = default;
            T secondMax = default;
            bool hasValues = false;

            foreach (var item in enumerable)
            {
                if (hasValues == false)
                {
                    // 初始化最大值和次大值为集合的第一个元素
                    max = item;
                    secondMax = item;
                    hasValues = true;
                }
                else if (item.CompareTo(max) > 0)
                {
                    // 发现新的最大值，更新次大值和最大值
                    secondMax = max;
                    max = item;
                }
                else if (item.CompareTo(secondMax) > 0 || secondMax.Equals(max))
                {
                    // 当前项不大于最大值但大于次大值，或最大值和次大值相同时更新次大值
                    secondMax = item;
                }
            }

            if (hasValues == false)
            {
                throw new InvalidOperationException("集合不能为空");
            }

            return (max, secondMax);
        }

        #endregion
    }
}