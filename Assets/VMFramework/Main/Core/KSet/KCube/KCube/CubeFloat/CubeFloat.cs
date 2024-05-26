using System;
using UnityEngine;

namespace VMFramework.Core
{
    public readonly partial struct CubeFloat : IKCubeFloat<Vector3>, IEquatable<CubeFloat>, IFormattable
    {
        public static CubeFloat zero { get; } = new(Vector3.zero, Vector3.zero);

        public static CubeFloat one { get; } = new(Vector3.one, Vector3.one);

        public static CubeFloat unit { get; } = new(Vector3.zero, Vector3.one);

        public Vector3 size => max - min;

        public Vector3 pivot => (max + min) / 2;

        public Vector3 extents => (max - min) / 2;

        public readonly Vector3 min, max;

        public RangeFloat xRange => new(min.x, max.x);

        public RangeFloat yRange => new(min.y, max.y);

        public RangeFloat zRange => new(min.z, max.z);

        public RectangleFloat xyRectangle => new(min.x, min.y, max.x, max.y);

        public RectangleFloat xzRectangle => new(min.x, min.z, max.x, max.z);

        public RectangleFloat yzRectangle => new(min.y, min.z, max.y, max.z);


        #region Constructor

        public CubeFloat(RangeFloat xRange, RangeFloat yRange, RangeFloat zRange)
        {
            min = new Vector3(xRange.min, yRange.min, zRange.min);
            max = new Vector3(xRange.max, yRange.max, zRange.max);
        }

        public CubeFloat(RectangleFloat xyRectangle, RangeFloat zRange)
        {
            min = new Vector3(xyRectangle.min.x, xyRectangle.min.y, zRange.min);
            max = new Vector3(xyRectangle.max.x, xyRectangle.max.y, zRange.max);
        }

        public CubeFloat(RangeFloat xRange, RectangleFloat yzRectangle)
        {
            min = new Vector3(xRange.min, yzRectangle.min.x, yzRectangle.min.y);
            max = new Vector3(xRange.max, yzRectangle.max.x, yzRectangle.max.y);
        }

        public CubeFloat(float xMin, float yMin, float zMin, float xMax, float yMax, float zMax)
        {
            min = new Vector3(xMin, yMin, zMin);
            max = new Vector3(xMax, yMax, zMax);
        }

        public CubeFloat(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public CubeFloat(float width, float length, float height)
        {
            min = Vector3.zero;
            max = new Vector3(width, length, height);
        }

        public CubeFloat(Vector3 size)
        {
            min = Vector3.zero;
            max = size;
        }

        public CubeFloat(CubeFloat source)
        {
            min = source.min;
            max = source.max;
        }

        public CubeFloat(IKCube<Vector3> config)
        {
            if (config is null)
            {
                min = Vector3.zero;
                max = Vector3.zero;
                return;
            }

            min = config.min;
            max = config.max;
        }

        #endregion

        #region Equatable

        public bool Equals(CubeFloat other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public override bool Equals(object obj)
        {
            return obj is CubeFloat other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(min, max);
        }

        #endregion

        #region To String

        public override string ToString()
        {
            return $"[{min}, {max}]";
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"[{min.ToString(format, formatProvider)},{max.ToString(format, formatProvider)}]";
        }

        #endregion
    }
}