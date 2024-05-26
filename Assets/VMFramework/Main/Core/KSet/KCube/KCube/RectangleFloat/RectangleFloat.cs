using System;
using UnityEngine;

namespace VMFramework.Core
{
    public readonly partial struct RectangleFloat : IKCubeFloat<Vector2>, IEquatable<RectangleFloat>, IFormattable
    {
        public static RectangleFloat zero { get; } =
            new(Vector2.zero, Vector2.zero);

        public static RectangleFloat one { get; } =
            new(Vector2.one, Vector2.one);

        public static RectangleFloat unit { get; } =
            new(Vector2.zero, Vector2.one);

        public Vector2 size => max - min;

        public Vector2 pivot => (max + min) / 2;

        public Vector2 extents => (max - min) / 2;

        public readonly Vector2 min, max;

        public RangeFloat xRange => new(min.x, max.x);

        public RangeFloat yRange => new(min.y, max.y);

        public Vector2 leftTop => new(min.x, max.y);

        public Vector2 rightBottom => new(max.x, min.y);

        #region Constructor

        public RectangleFloat(RangeFloat xRange, RangeFloat yRange)
        {
            min = new Vector2(xRange.min, yRange.min);
            max = new Vector2(xRange.max, yRange.max);
        }

        public RectangleFloat(float xMin, float yMin, float xMax, float yMax)
        {
            min = new Vector2(xMin, yMin);
            max = new Vector2(xMax, yMax);
        }

        public RectangleFloat(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }

        public RectangleFloat(float width, float length)
        {
            min = Vector2.zero;
            max = new Vector2(width, length);
        }

        public RectangleFloat(Vector2 size)
        {
            min = Vector2.zero;
            max = size;
        }

        public RectangleFloat(RectangleFloat source)
        {
            min = source.min;
            max = source.max;
        }

        public RectangleFloat(IKCube<Vector2> config)
        {
            if (config is null)
            {
                min = Vector2.zero;
                max = Vector2.zero;
                return;
            }
            min = config.min;
            max = config.max;
        }

        public RectangleFloat(Rect rect)
        {
            min = rect.min;
            max = rect.max;
        }

        #endregion

        #region To Rect

        public Rect ToRect() => new Rect(min, size);

        public static implicit operator Rect(RectangleFloat r) => r.ToRect();

        #endregion

        #region Equatable

        public bool Equals(RectangleFloat other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public override bool Equals(object obj)
        {
            return obj is RectangleFloat other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(min, max);
        }

        #endregion

        #region To String

        public override string ToString() => $"[{min}, {max}]";

        public string ToString(string format, IFormatProvider formatProvider) =>
            $"[{min.ToString(format, formatProvider)},{max.ToString(format, formatProvider)}]";

        #endregion
    }
}
