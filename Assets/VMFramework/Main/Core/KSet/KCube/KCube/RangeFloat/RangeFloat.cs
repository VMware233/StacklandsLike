using System;
using UnityEngine;

namespace VMFramework.Core
{
    public readonly partial struct RangeFloat : IKCubeFloat<float>, IEquatable<RangeFloat>, IFormattable
    {
        public static RangeFloat zero { get; } = new(0, 0);

        public static RangeFloat one { get; } = new(1, 1);

        public static RangeFloat unit { get; } = new(0, 1);

        public float size => max - min;

        public float pivot => (max + min) / 2;

        public float extents => (max - min) / 2;

        public readonly float min, max;

        #region Constructor

        public RangeFloat(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public RangeFloat(float length)
        {
            min = 0;
            max = length;
        }

        public RangeFloat(RangeFloat source)
        {
            min = source.min;
            max = source.max;
        }

        public RangeFloat(IKCube<float> config)
        {
            if (config == null)
            {
                min = 0;
                max = 0;
                return;
            }
            min = config.min;
            max = config.max;
        }

        public RangeFloat(Vector2 range)
        {
            min = range.x;
            max = range.y;
        }

        #endregion

        #region Equatable

        public bool Equals(RangeFloat other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public override bool Equals(object obj)
        {
            return obj is RangeFloat other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(min, max);
        }

        #endregion

        #region To String

        public override string ToString() => $"[{min}, {max}]";

        public string ToString(string format, IFormatProvider formatProvider) =>
            $"[{min.ToString(format, formatProvider)}, {max.ToString(format, formatProvider)}]";

        #endregion
    }
}
