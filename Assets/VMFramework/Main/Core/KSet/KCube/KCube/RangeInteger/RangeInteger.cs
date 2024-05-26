using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core
{
    public readonly partial struct RangeInteger : IKCubeInteger<int>, IEquatable<RangeInteger>, IFormattable, 
        IEnumerable<int>
    {
        public static RangeInteger zero { get; } = new(0, 0);

        public static RangeInteger one { get; } = new(1, 1);

        public static RangeInteger unit { get; } = new(0, 1);

        public int size => max - min + 1;

        public int pivot => (max + min) / 2;

        public readonly int min, max;

        #region Constructor

        public RangeInteger(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public RangeInteger(int length)
        {
            min = 0;
            max = length - 1;
        }

        public RangeInteger(RangeInteger source)
        {
            min = source.min;
            max = source.max;
        }

        public RangeInteger(IKCube<int> config)
        {
            if (config is null)
            {
                min = 0;
                max = 0;
                return;
            }

            min = config.min;
            max = config.max;
        }

        public RangeInteger(Vector2Int range)
        {
            min = range.x;
            max = range.y;
        }

        #endregion

        #region Equatable

        public bool Equals(RangeInteger other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public override bool Equals(object obj)
        {
            return obj is RangeInteger other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(min, max);
        }

        #endregion

        #region Enumerator

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<int> GetEnumerator()
        {
            return this.GetAllPoints().GetEnumerator();
        }

        #endregion

        #region To String

        public override string ToString() => $"[{min}, {max}]";

        public string ToString(string format, IFormatProvider formatProvider) =>
            $"[{min.ToString(format, formatProvider)}, {max.ToString(format, formatProvider)}]";

        #endregion
    }
}
