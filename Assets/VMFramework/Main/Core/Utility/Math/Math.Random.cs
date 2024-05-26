using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EnumsNET;
using UnityEngine;
using VMFramework.Core.Linq;
using Random = UnityEngine.Random;

namespace VMFramework.Core
{
    public static class RandomUtility
    {
        #region Choose

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Choose<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToArray().Choose();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Choose<T>(this IList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (list.Count == 0)
            {
                throw new ArgumentException("List is empty.", nameof(list));
            }
            
            return list[Random.Range(0, list.Count)];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Choose<T>(this IList<T> list, int count)
        {
            return list.Get(GenerateUniqueIntegers(count, 0, list.Count - 1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> Choose<TKey, TValue>(
            this Dictionary<TKey, TValue> dict)
        {
            var resultKey = Choose(dict.Keys.ToList());
            return new KeyValuePair<TKey, TValue>(resultKey, dict[resultKey]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TKey ChooseKey<TKey, TValue>(
            this Dictionary<TKey, TValue> dict)
        {
            return Choose(dict.Keys.ToList());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue ChooseValue<TKey, TValue>(
            this Dictionary<TKey, TValue> dict)
        {
            return Choose(dict.Values.ToList());
        }

        public static T Choose<T>(this IList<T> objects, IList<int> rates)
        {
            if (objects.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(objects));
            }

            if (objects.Count == 1)
            {
                return objects[0];
            }

            if (rates.Count == 1)
            {
                return objects[0];
            }

            int length = rates.Count.ClampMax(objects.Count);

            int sum = rates.Sum(length);

            if (sum == 0)
            {
                return objects[0];
            }

            int randomRate = sum.RandomRange();

            int cumulativeRate = 0;
            for (int i = 0; i < length; i++)
            {
                cumulativeRate += rates[i];
                if (cumulativeRate >= randomRate)
                {
                    return objects[i];
                }
            }

            return default;
        }

        public static T Choose<T>(this IList<T> objects, IList<float> rates)
        {
            if (objects.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(objects));
            }

            if (objects.Count == 1)
            {
                return objects[0];
            }

            if (rates.Count == 1)
            {
                return objects[0];
            }

            int length = rates.Count.ClampMax(objects.Count);

            float sum = rates.Sum(length);

            if (sum == 0)
            {
                return objects[0];
            }

            float randomRate = sum.RandomRange();

            float cumulativeRate = 0;
            for (int i = 0; i < length; i++)
            {
                cumulativeRate += rates[i];
                if (cumulativeRate >= randomRate)
                {
                    return objects[i];
                }
            }

            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEnum ChooseFlag<TEnum>(this TEnum enumValue)
            where TEnum : struct, Enum
        {
            return enumValue.GetFlags().Choose();
        }

        #endregion

        #region Choose Or Default

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ChooseOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue = default)
        {
            if (enumerable == null)
            {
                return defaultValue;
            }
            return enumerable.ToArray().ChooseOrDefault(defaultValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ChooseOrDefault<T>(this IList<T> list, T defaultValue = default)
        {
            if (list.IsNullOrEmpty())
            {
                return defaultValue;
            }
            
            return list[Random.Range(0, list.Count)];
        }

        #endregion

        #region Shuffle

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        #endregion

        #region RandomRange

        #region Range

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandomRange(this int min, int max)
        {
            return Random.Range(min, max + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandomRange(this int length)
        {
            return RandomRange(0, length - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandomRange(this float min, float max)
        {
            return Random.Range(min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandomRange(this float length)
        {
            return RandomRange(0, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int RandomRange(this Vector2Int minPos,
            Vector2Int maxPos)
        {
            return new(minPos.x.RandomRange(maxPos.x),
                minPos.y.RandomRange(maxPos.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int RandomRange(this Vector2Int size)
        {
            return RandomRange(Vector2Int.zero, size - Vector2Int.one);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int RandomRange(this Vector3Int minPos,
            Vector3Int maxPos)
        {
            return new(minPos.x.RandomRange(maxPos.x),
                minPos.y.RandomRange(maxPos.y), minPos.z.RandomRange(maxPos.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int RandomRange(this Vector3Int size)
        {
            return RandomRange(Vector3Int.zero, size - Vector3Int.one);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 RandomRange(this Vector2 minPos, Vector2 maxPos)
        {
            return new(minPos.x.RandomRange(maxPos.x),
                minPos.y.RandomRange(maxPos.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 RandomRange(this Vector2 size)
        {
            return RandomRange(Vector2.zero, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 RandomRange(this Vector3 minPos, Vector3 maxPos)
        {
            return new(minPos.x.RandomRange(maxPos.x),
                minPos.y.RandomRange(maxPos.y), minPos.z.RandomRange(maxPos.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 RandomRange(this Vector3 size)
        {
            return RandomRange(Vector3.zero, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 RandomRange(this Vector4 minPos, Vector4 maxPos)
        {
            return new(minPos.x.RandomRange(maxPos.x),
                minPos.y.RandomRange(maxPos.y), minPos.z.RandomRange(maxPos.z),
                minPos.w.RandomRange(maxPos.w));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 RandomRange(this Vector4 size)
        {
            return RandomRange(Vector4.zero, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color RandomRange(this Color minPos, Color maxPos)
        {
            return new(minPos.r.RandomRange(maxPos.r),
                minPos.g.RandomRange(maxPos.g), minPos.b.RandomRange(maxPos.b),
                minPos.a.RandomRange(maxPos.a));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color RandomRange(this Color size)
        {
            return RandomRange(new(0, 0, 0, 0), size);
        }

        #endregion

        public static IEnumerable<int> SeveralRandomRange(this int count, int min,
            int max)
        {
            return count.Repeat(() => RandomRange(min, max));
        }

        public static IEnumerable<int> SeveralRandomRange(this int count, int length)
        {
            return SeveralRandomRange(count, 0, length - 1);
        }

        #region Unique Numbers

        /// <summary>
        /// 生成一定范围内一组不重复的随机整数
        /// </summary>
        /// <param name="count"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static IEnumerable<int> GenerateUniqueIntegers(this int count,
            int min, int max)
        {
            if (count <= 0)
            {
                return Enumerable.Empty<int>();
            }

            int range = max - min + 1;

            if (range <= 0)
            {
                return Enumerable.Empty<int>();
            }

            count = count.ClampMax(range);

            if (count < range / 10)
            {
                var numbers = new HashSet<int>();

                while (numbers.Count < count)
                {
                    int number = RandomRange(min, max);
                    numbers.Add(number);
                }

                return numbers;
            }

            var container = min.GetAllPointsOfRange(max).ToList();

            container.Shuffle();

            return container.GetRange(0, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GenerateUniqueIntegers(this int count,
            int length)
        {
            return GenerateUniqueIntegers(count, 0, length - 1);
        }

        /// <summary>
        /// 生成一定范围内一组不重复的随机Vector2Int
        /// </summary>
        /// <param name="count"></param>
        /// <param name="minPos"></param>
        /// <param name="maxPos"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GenerateUniqueVector2Ints(
            this int count, Vector2Int minPos, Vector2Int maxPos)
        {
            Vector2Int range = maxPos - minPos + Vector2Int.one;

            int totalLength = range.Products();

            foreach (var index in GenerateUniqueIntegers(count, totalLength))
            {
                int x = index / range.y;
                int y = index - x * range.y;
                yield return new(x + minPos.x, y + minPos.y);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GenerateUniqueVector2Ints(
            this int count, Vector2Int size)
        {
            return GenerateUniqueVector2Ints(count, Vector2Int.zero,
                size - Vector2Int.one);
        }

        /// <summary>
        /// 生成一定范围内一组不重复的随机Vector3Int
        /// </summary>
        /// <param name="count"></param>
        /// <param name="minPos"></param>
        /// <param name="maxPos"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GenerateUniqueVector3Ints(
            this int count, Vector3Int minPos, Vector3Int maxPos)
        {
            Vector3Int range = maxPos - minPos + Vector3Int.one;

            int totalLength = range.Products();

            foreach (var index in GenerateUniqueIntegers(count, totalLength))
            {
                int rangeYZ = range.y * range.z;
                int x = index / rangeYZ;
                int rangeDivideByYZ = index - x * rangeYZ;
                int y = rangeDivideByYZ / range.z;
                int z = rangeDivideByYZ - y * range.z;
                yield return new(x + minPos.x, y + minPos.y, z + minPos.z);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GenerateUniqueVector3Ints(
            this int count, Vector3Int size)
        {
            return GenerateUniqueVector3Ints(count, Vector3Int.zero,
                size - Vector3Int.one);
        }

        #endregion

        #endregion

        #region RandomKCubeShape

        #region Range

        /// <summary>
        /// 获得区间[from, to]内的一个随机的整数区间
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RangeInteger RandomRangeInteger(this int from, int to)
        {
            to.AssertIsAboveOrEqual(from, nameof(to), nameof(from));

            int start = RandomRange(from, to);
            int length = RandomRange(0, to - from + 1);

            int end = (start + length).Repeat(from, to);

            return start > end ? new(end, start) : new(start, end);
        }

        /// <summary>
        /// 获得区间[from, to]内的一个随机的浮点数区间
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RangeFloat RandomRangeFloat(this float from, float to)
        {
            to.AssertIsAboveOrEqual(from, nameof(to), nameof(from));

            float start = RandomRange(from, to);
            float length = RandomRange(0, to - from);

            float end = (start + length).Repeat(from, to);

            return start > end ? new(end, start) : new(start, end);
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RangeInteger> SeveralRandomRangeInteger(
            this int count, int from, int to)
        {
            return count.Repeat(() => RandomRangeInteger(from, to));
        }

        #endregion

        #region Random Point On/Inside Circle/Sphere

        #region Inside

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 RandomPointInsideCircle(this float radius)
        {
            return Random.insideUnitCircle * radius;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int RandomIntPointInsideCircle(this float radius)
        {
            return (Random.insideUnitCircle * radius).Round();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 RandomPointInsideSphere(this float radius)
        {
            return Random.insideUnitSphere * radius;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int RandomIntPointInsideSphere(this float radius)
        {
            return (Random.insideUnitSphere * radius).Round();
        }

        #endregion

        #region On

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 RandomPointOnCircle(this float radius)
        {
            float angle = Random.value * 2 * Mathf.PI;

            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            return new Vector2(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 RandomPointOnSphere(this float radius)
        {
            return Random.onUnitSphere * radius;
        }

        #endregion

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RandomTrue01(this float ratio)
        {
            return Random.value <= ratio;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RandomTruePercents(this float ratio)
        {
            return Random.value * 100 <= ratio;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RandomTruePercents(this int ratio)
        {
            return RandomTruePercents((float)ratio);
        }
    }
}

