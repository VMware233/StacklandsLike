using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VMFramework.Core.Linq
{
    public static partial class LINQUtility
    {
        #region Repeat

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Repeat<T>(this int n, Func<T> func)
        {
            for (int i = 0; i < n; i++)
            {
                yield return func();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Repeat(this int n, Action func)
        {
            for (int i = 0; i < n; i++)
            {
                func();
            }
        }

        #endregion

        #region Examine

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Examine<T>(this IEnumerable<T> enumerable,
            Action<T> action)
        {
            if (enumerable == null)
            {
                return;
            }
            
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<bool> ExamineIf<T>(this IEnumerable<T> enumerable,
            Func<T, bool> condition, Action<T> action)
        {
            var results = new List<bool>();
            enumerable.Examine(item =>
            {
                var result = condition(item);
                if (result) action(item);
                results.Add(result);
            });

            return results;
        }

        #endregion
    }
}