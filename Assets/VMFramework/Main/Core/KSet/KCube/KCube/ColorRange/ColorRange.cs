using System;
using UnityEngine;

namespace VMFramework.Core
{
    public readonly partial struct ColorRange : IKCubeFloat<Color>, IEquatable<ColorRange>, IFormattable
    {
        public static Color zeroColor { get; } = new(0, 0, 0, 0);

        public static ColorRange zero { get; } =
            new(zeroColor, zeroColor);

        public static ColorRange one { get; } =
            new(Color.white, Color.white);

        public static ColorRange unit { get; } =
            new(zeroColor, Color.white);

        public Color size => max - min;

        public Color pivot => (max + min) / 2;

        public Color extents => (max - min) / 2;

        public readonly Color min, max;

        public RangeFloat rRange => new(min.r, max.r);

        public RangeFloat gRange => new(min.g, max.g);

        public RangeFloat bRange => new(min.b, max.b);

        public RangeFloat aRange => new(min.a, max.a);

        #region Constructor

        public ColorRange(RangeFloat xRange, RangeFloat yRange, RangeFloat zRange)
        {
            min = new Color(xRange.min, yRange.min, zRange.min);
            max = new Color(xRange.max, yRange.max, zRange.max);
        }

        public ColorRange(float xMin, float yMin, float zMin, float xMax, float yMax,
            float zMax)
        {
            min = new Color(xMin, yMin, zMin);
            max = new Color(xMax, yMax, zMax);
        }

        public ColorRange(Color min, Color max)
        {
            this.min = min;
            this.max = max;
        }

        public ColorRange(float width, float length, float height)
        {
            min = zeroColor;
            max = new Color(width, length, height);
        }

        public ColorRange(Color size)
        {
            min = zeroColor;
            max = size;
        }

        public ColorRange(ColorRange source)
        {
            min = source.min;
            max = source.max;
        }

        public ColorRange(IKCube<Color> config)
        {
            if (config is null)
            {
                min = zeroColor;
                max = zeroColor;
                return;
            }
            min = config.min;
            max = config.max;
        }

        #endregion

        #region Equatable

        public bool Equals(ColorRange other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public override bool Equals(object obj)
        {
            return obj is ColorRange other && Equals(other);
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
