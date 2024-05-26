using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        #region For Three Values

        #region Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MinDistance(this int origin, int b, int c)
        {
            return origin.Distance(b).Min(origin.Distance(c));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MaxDistance(this int origin, int b, int c)
        {
            return origin.Distance(b).Max(origin.Distance(c));
        }

        #endregion

        #region Float

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this float origin, float b, float c)
        {
            return origin.Distance(b).Min(origin.Distance(c));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this float origin, float b, float c)
        {
            return origin.Distance(b).Max(origin.Distance(c));
        }

        #endregion

        #region Vector2

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Vector2 origin, Vector2 b, Vector2 c, DistanceType distanceType)
        {
            return origin.Distance(b, distanceType).Min(origin.Distance(c, distanceType));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Vector2 origin, Vector2 b, Vector2 c, DistanceType distanceType)
        {
            return origin.Distance(b, distanceType).Max(origin.Distance(c, distanceType));
        }

        #endregion

        #region Vector3

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Vector3 origin, Vector3 b, Vector3 c, DistanceType distanceType)
        {
            return origin.Distance(b, distanceType).Min(origin.Distance(c, distanceType));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Vector3 origin, Vector3 b, Vector3 c, DistanceType distanceType)
        {
            return origin.Distance(b, distanceType).Max(origin.Distance(c, distanceType));
        }

        #endregion

        #region Vector2Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Vector2Int origin, Vector2Int b, Vector2Int c,
            DistanceType distanceType)
        {
            return origin.Distance(b, distanceType).Min(origin.Distance(c, distanceType));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Vector2Int origin, Vector2Int b, Vector2Int c,
            DistanceType distanceType)
        {
            return origin.Distance(b, distanceType).Max(origin.Distance(c, distanceType));
        }

        #endregion

        #region Vector3Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Vector3Int origin, Vector3Int b, Vector3Int c,
            DistanceType distanceType)
        {
            return origin.Distance(b, distanceType).Min(origin.Distance(c, distanceType));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Vector3Int origin, Vector3Int b, Vector3Int c,
            DistanceType distanceType)
        {
            return origin.Distance(b, distanceType).Max(origin.Distance(c, distanceType));
        }

        #endregion

        #region Vector4

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Vector4 origin, Vector4 b, Vector4 c, DistanceType distanceType)
        {
            return origin.Distance(b, distanceType).Min(origin.Distance(c, distanceType));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Vector4 origin, Vector4 b, Vector4 c, DistanceType distanceType)
        {
            return origin.Distance(b, distanceType).Max(origin.Distance(c, distanceType));
        }

        #endregion

        #region Color

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Color origin, Color b, Color c, DistanceType distanceType,
            bool ignoreAlpha = false)
        {
            return origin.Distance(b, distanceType, ignoreAlpha)
                .Min(origin.Distance(c, distanceType, ignoreAlpha));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Color origin, Color b, Color c, DistanceType distanceType,
            bool ignoreAlpha = false)
        {
            return origin.Distance(b, distanceType, ignoreAlpha)
                .Max(origin.Distance(c, distanceType, ignoreAlpha));
        }

        #endregion

        #endregion

        #region For Enumerable

        #region Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MinDistance<T>(this int origin, IEnumerable<T> enumerable, Func<T, int> selector)
        {
            var minDistance = int.MaxValue;

            foreach (var t in enumerable)
            {
                var distance = selector(t).Distance(origin);

                if (distance < minDistance)
                {
                    if (minDistance == 0)
                    {
                        return 0;
                    }

                    minDistance = distance;
                }
            }

            return minDistance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MinDistance(this int origin, IEnumerable<int> enumerable)
        {
            return origin.MinDistance(enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MinDistance(this int origin, params int[] array)
        {
            return origin.MinDistance(array.AsEnumerable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MaxDistance<T>(this int origin, IEnumerable<T> enumerable, Func<T, int> selector)
        {
            return enumerable.Select(t => selector(t).Distance(origin)).Prepend(0).Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MaxDistance(this int origin, IEnumerable<int> enumerable)
        {
            return origin.MaxDistance(enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MaxDistance(this int origin, params int[] array)
        {
            return origin.MaxDistance(array.AsEnumerable());
        }

        #endregion

        #region Float

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance<T>(this float origin, IEnumerable<T> enumerable,
            Func<T, float> selector)
        {
            var minDistance = float.MaxValue;

            foreach (var t in enumerable)
            {
                var distance = selector(t).Distance(origin);

                if (distance < minDistance)
                {
                    if (minDistance == 0)
                    {
                        return 0;
                    }

                    minDistance = distance;
                }
            }

            return minDistance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this float origin, IEnumerable<float> enumerable)
        {
            return origin.MinDistance(enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this float origin, params float[] array)
        {
            return origin.MinDistance(array.AsEnumerable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance<T>(this float origin, IEnumerable<T> enumerable,
            Func<T, float> selector)
        {
            return enumerable.Select(t => selector(t).Distance(origin)).Prepend(0).Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this float origin, IEnumerable<float> enumerable)
        {
            return origin.MaxDistance(enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this float origin, params float[] array)
        {
            return origin.MaxDistance(array.AsEnumerable());
        }

        #endregion

        #region Vector2

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance<T>(this Vector2 origin, DistanceType distanceType,
            IEnumerable<T> enumerable, Func<T, Vector2> selector)
        {
            var minDistance = float.MaxValue;

            foreach (var t in enumerable)
            {
                var distance = selector(t).Distance(origin, distanceType);

                if (distance < minDistance)
                {
                    if (minDistance == 0)
                    {
                        return 0;
                    }

                    minDistance = distance;
                }
            }

            return minDistance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Vector2 origin, DistanceType distanceType,
            IEnumerable<Vector2> enumerable)
        {
            return origin.MinDistance(distanceType, enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Vector2 origin, DistanceType distanceType,
            params Vector2[] array)
        {
            return origin.MinDistance(distanceType, array.AsEnumerable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance<T>(this Vector2 origin, DistanceType distanceType,
            IEnumerable<T> enumerable, Func<T, Vector2> selector)
        {
            return enumerable.Select(t => selector(t).Distance(origin, distanceType)).Prepend(0).Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Vector2 origin, DistanceType distanceType,
            IEnumerable<Vector2> enumerable)
        {
            return origin.MaxDistance(distanceType, enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Vector2 origin, DistanceType distanceType,
            params Vector2[] array)
        {
            return origin.MaxDistance(distanceType, array.AsEnumerable());
        }

        #endregion

        #region Vector2Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance<T>(this Vector2Int origin, DistanceType distanceType,
            IEnumerable<T> enumerable, Func<T, Vector2Int> selector)
        {
            var minDistance = float.MaxValue;

            foreach (var t in enumerable)
            {
                var distance = selector(t).Distance(origin, distanceType);

                if (distance < minDistance)
                {
                    if (minDistance == 0)
                    {
                        return 0;
                    }

                    minDistance = distance;
                }
            }

            return minDistance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Vector2Int origin, DistanceType distanceType,
            IEnumerable<Vector2Int> enumerable)
        {
            return origin.MinDistance(distanceType, enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Vector2Int origin, DistanceType distanceType,
            params Vector2Int[] array)
        {
            return origin.MinDistance(distanceType, array.AsEnumerable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance<T>(this Vector2Int origin, DistanceType distanceType,
            IEnumerable<T> enumerable, Func<T, Vector2Int> selector)
        {
            return enumerable.Select(t => selector(t).Distance(origin, distanceType)).Prepend(0).Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Vector2Int origin, DistanceType distanceType,
            IEnumerable<Vector2Int> enumerable)
        {
            return origin.MaxDistance(distanceType, enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Vector2Int origin, DistanceType distanceType,
            params Vector2Int[] array)
        {
            return origin.MaxDistance(distanceType, array.AsEnumerable());
        }

        #endregion

        #region Vector3

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance<T>(this Vector3 origin, DistanceType distanceType,
            IEnumerable<T> enumerable, Func<T, Vector3> selector)
        {
            var minDistance = float.MaxValue;

            foreach (var t in enumerable)
            {
                var distance = selector(t).Distance(origin, distanceType);

                if (distance < minDistance)
                {
                    if (minDistance == 0)
                    {
                        return 0;
                    }

                    minDistance = distance;
                }
            }

            return minDistance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Vector3 origin, DistanceType distanceType,
            IEnumerable<Vector3> enumerable)
        {
            return origin.MinDistance(distanceType, enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Vector3 origin, DistanceType distanceType,
            params Vector3[] array)
        {
            return origin.MinDistance(distanceType, array.AsEnumerable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance<T>(this Vector3 origin, DistanceType distanceType,
            IEnumerable<T> enumerable, Func<T, Vector3> selector)
        {
            return enumerable.Select(t => selector(t).Distance(origin, distanceType)).Prepend(0).Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Vector3 origin, DistanceType distanceType,
            IEnumerable<Vector3> enumerable)
        {
            return origin.MaxDistance(distanceType, enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Vector3 origin, DistanceType distanceType,
            params Vector3[] array)
        {
            return origin.MaxDistance(distanceType, array.AsEnumerable());
        }

        #endregion

        #region Vector3Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance<T>(this Vector3Int origin, DistanceType distanceType,
            IEnumerable<T> enumerable, Func<T, Vector3Int> selector)
        {
            var minDistance = float.MaxValue;

            foreach (var t in enumerable)
            {
                var distance = selector(t).Distance(origin, distanceType);

                if (distance < minDistance)
                {
                    if (minDistance == 0)
                    {
                        return 0;
                    }

                    minDistance = distance;
                }
            }

            return minDistance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Vector3Int origin, DistanceType distanceType,
            IEnumerable<Vector3Int> enumerable)
        {
            return origin.MinDistance(distanceType, enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Vector3Int origin, DistanceType distanceType,
            params Vector3Int[] array)
        {
            return origin.MinDistance(distanceType, array.AsEnumerable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance<T>(this Vector3Int origin, DistanceType distanceType,
            IEnumerable<T> enumerable, Func<T, Vector3Int> selector)
        {
            return enumerable.Select(t => selector(t).Distance(origin, distanceType)).Prepend(0).Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Vector3Int origin, DistanceType distanceType,
            IEnumerable<Vector3Int> enumerable)
        {
            return origin.MaxDistance(distanceType, enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Vector3Int origin, DistanceType distanceType,
            params Vector3Int[] array)
        {
            return origin.MaxDistance(distanceType, array.AsEnumerable());
        }

        #endregion

        #region Vector4

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 MinDistanceBy<T>(this Vector4 origin, DistanceType distanceType,
            IEnumerable<T> enumerable, Func<T, Vector4> selector)
        {
            var list = enumerable.ToList();

            list.Count.AssertIsAbove(0, nameof(list.Count));

            var minDistance = float.MaxValue;
            Vector4 minDistanceOne = default;

            foreach (var t in list)
            {
                var one = selector(t);
                var distance = one.Distance(origin, distanceType);

                if (distance < minDistance)
                {
                    if (minDistance == 0)
                    {
                        return one;
                    }

                    minDistance = distance;
                    minDistanceOne = one;
                }
            }

            return minDistanceOne;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 MinDistanceBy(this Vector4 origin, DistanceType distanceType,
            IEnumerable<Vector4> enumerable)
        {
            return origin.MinDistanceBy(distanceType, enumerable, color => color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 MinDistanceBy(this Vector4 origin, DistanceType distanceType,
            params Vector4[] array)
        {
            return origin.MinDistanceBy(distanceType, array.AsEnumerable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 MaxDistanceBy<T>(this Vector4 origin, DistanceType distanceType,
            IEnumerable<T> enumerable, Func<T, Vector4> selector)
        {
            var list = enumerable.ToList();

            list.Count.AssertIsAbove(0, nameof(list.Count));

            float maxDistance = -1;
            Vector4 maxDistanceOne = default;

            foreach (var t in list)
            {
                var one = selector(t);
                var distance = one.Distance(origin, distanceType);

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    maxDistanceOne = one;
                }
            }

            return maxDistanceOne;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 MaxDistanceBy(this Vector4 origin, DistanceType distanceType,
            IEnumerable<Vector4> enumerable)
        {
            return origin.MaxDistanceBy(distanceType, enumerable, color => color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 MaxDistanceBy(this Vector4 origin, DistanceType distanceType,
            params Vector4[] array)
        {
            return origin.MaxDistanceBy(distanceType, array.AsEnumerable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance<T>(this Vector4 origin, DistanceType distanceType,
            IEnumerable<T> enumerable, Func<T, Vector4> selector)
        {
            var minDistance = float.MaxValue;

            foreach (var t in enumerable)
            {
                var distance = selector(t).Distance(origin, distanceType);

                if (distance < minDistance)
                {
                    if (minDistance == 0)
                    {
                        return 0;
                    }

                    minDistance = distance;
                }
            }

            return minDistance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Vector4 origin, DistanceType distanceType,
            IEnumerable<Vector4> enumerable)
        {
            return origin.MinDistance(distanceType, enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Vector4 origin, DistanceType distanceType,
            params Vector4[] array)
        {
            return origin.MinDistance(distanceType, array.AsEnumerable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance<T>(this Vector4 origin, DistanceType distanceType,
            IEnumerable<T> enumerable, Func<T, Vector4> selector)
        {
            return enumerable.Select(t => selector(t).Distance(origin, distanceType)).Prepend(0).Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Vector4 origin, DistanceType distanceType,
            IEnumerable<Vector4> enumerable)
        {
            return origin.MaxDistance(distanceType, enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Vector4 origin, DistanceType distanceType,
            params Vector4[] array)
        {
            return origin.MaxDistance(distanceType, array.AsEnumerable());
        }

        #endregion

        #region Color

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance<T>(this Color origin, DistanceType distanceType, bool ignoreAlpha,
            IEnumerable<T> enumerable, Func<T, Color> selector)
        {
            var minDistance = float.MaxValue;

            foreach (var t in enumerable)
            {
                var distance = selector(t).Distance(origin, distanceType, ignoreAlpha);

                if (distance < minDistance)
                {
                    if (minDistance == 0)
                    {
                        return 0;
                    }

                    minDistance = distance;
                }
            }

            return minDistance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Color origin, DistanceType distanceType, bool ignoreAlpha,
            IEnumerable<Color> enumerable)
        {
            return origin.MinDistance(distanceType, ignoreAlpha, enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinDistance(this Color origin, DistanceType distanceType, bool ignoreAlpha,
            params Color[] array)
        {
            return origin.MinDistance(distanceType, ignoreAlpha, array.AsEnumerable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance<T>(this Color origin, DistanceType distanceType, bool ignoreAlpha,
            IEnumerable<T> enumerable, Func<T, Color> selector)
        {
            return enumerable.Select(t => selector(t).Distance(origin, distanceType, ignoreAlpha)).Prepend(0)
                .Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Color origin, DistanceType distanceType, bool ignoreAlpha,
            IEnumerable<Color> enumerable)
        {
            return origin.MaxDistance(distanceType, ignoreAlpha, enumerable, i => i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxDistance(this Color origin, DistanceType distanceType, bool ignoreAlpha,
            params Color[] array)
        {
            return origin.MaxDistance(distanceType, ignoreAlpha, array.AsEnumerable());
        }

        #endregion

        #endregion
    }
}