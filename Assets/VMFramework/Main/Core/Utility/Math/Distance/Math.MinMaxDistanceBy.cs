using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        #region Int

        #endregion

        #region Float

        #endregion

        #region Vector2

        #endregion

        #region Vector3

        #endregion

        #region Vector2Int

        #endregion

        #region Vector3Int

        #endregion

        #region Vector4

        #endregion

        #region Color

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinDistanceBy<T>(this Color origin, DistanceType distanceType, bool ignoreAlpha,
            IEnumerable<T> enumerable, Func<T, Color> selector)
        {
            var list = enumerable.ToList();

            list.Count.AssertIsAbove(0, nameof(list.Count));

            var minDistance = float.MaxValue;
            T minDistanceOne = default;

            foreach (var t in list)
            {
                var distance = selector(t).Distance(origin, distanceType, ignoreAlpha);

                if (distance < minDistance)
                {
                    if (minDistance == 0)
                    {
                        return t;
                    }

                    minDistance = distance;
                    minDistanceOne = t;
                }
            }

            return minDistanceOne;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color MinDistanceBy(this Color origin, DistanceType distanceType, bool ignoreAlpha,
            IEnumerable<Color> enumerable)
        {
            return origin.MinDistanceBy(distanceType, ignoreAlpha, enumerable, color => color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color MinDistanceBy(this Color origin, DistanceType distanceType, bool ignoreAlpha,
            params Color[] array)
        {
            return origin.MinDistanceBy(distanceType, ignoreAlpha, array.AsEnumerable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxDistanceBy<T>(this Color origin, DistanceType distanceType, bool ignoreAlpha,
            IEnumerable<T> enumerable, Func<T, Color> selector)
        {
            var list = enumerable.ToList();

            list.Count.AssertIsAbove(0, nameof(list.Count));

            float maxDistance = -1;
            T maxDistanceOne = default;

            foreach (var t in list)
            {
                var distance = selector(t).Distance(origin, distanceType, ignoreAlpha);

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    maxDistanceOne = t;
                }
            }

            return maxDistanceOne;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color MaxDistanceBy(this Color origin, DistanceType distanceType, bool ignoreAlpha,
            IEnumerable<Color> enumerable)
        {
            return origin.MaxDistanceBy(distanceType, ignoreAlpha, enumerable, color => color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color MaxDistanceBy(this Color origin, DistanceType distanceType, bool ignoreAlpha,
            params Color[] array)
        {
            return origin.MaxDistanceBy(distanceType, ignoreAlpha, array.AsEnumerable());
        }

        #endregion
    }
}