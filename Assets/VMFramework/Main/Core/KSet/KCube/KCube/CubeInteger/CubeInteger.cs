using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core
{
    public readonly partial struct CubeInteger : IKCubeInteger<Vector3Int>, IEquatable<CubeInteger>, 
        IFormattable, IEnumerable<Vector3Int>
    {
        public static CubeInteger zero { get; } =
            new(Vector3Int.zero, Vector3Int.zero);

        public static CubeInteger one { get; } =
            new(Vector3Int.one, Vector3Int.one);

        public static CubeInteger unit { get; } =
            new(Vector3Int.zero, Vector3Int.one);

        public Vector3Int size => max - min + Vector3Int.one;

        public Vector3Int pivot => (max + min) / 2;

        public readonly Vector3Int min, max;

        public RangeInteger xRange => new(min.x, max.x);

        public RangeInteger yRange => new(min.y, max.y);

        public RangeInteger zRange => new(min.z, max.z);

        public RectangleInteger xyRectangle => new(min.x, min.y, max.x, max.y);

        public RectangleInteger xzRectangle => new(min.x, min.z, max.x, max.z);

        public RectangleInteger yzRectangle => new(min.y, min.z, max.y, max.z);

        #region Constructor

        public CubeInteger(RangeInteger xRange, RangeInteger yRange,
            RangeInteger zRange)
        {
            min = new Vector3Int(xRange.min, yRange.min, zRange.min);
            max = new Vector3Int(xRange.max, yRange.max, zRange.max);
        }

        public CubeInteger(RectangleInteger xyRectangle, RangeInteger zRange)
        {
            min = new Vector3Int(xyRectangle.min.x, xyRectangle.min.y, zRange.min);
            max = new Vector3Int(xyRectangle.max.x, xyRectangle.max.y, zRange.max);
        }

        public CubeInteger(RangeInteger xRange, RectangleInteger yzRectangle)
        {
            min = new Vector3Int(xRange.min, yzRectangle.min.x, yzRectangle.min.y);
            max = new Vector3Int(xRange.max, yzRectangle.max.x, yzRectangle.max.y);
        }

        public CubeInteger(int xMin, int yMin, int zMin, int xMax, int yMax,
            int zMax)
        {
            min = new Vector3Int(xMin, yMin, zMin);
            max = new Vector3Int(xMax, yMax, zMax);
        }

        public CubeInteger(Vector3Int min, Vector3Int max)
        {
            this.min = min;
            this.max = max;
        }

        public CubeInteger(int width, int length, int height)
        {
            min = Vector3Int.zero;
            max = new Vector3Int(width - 1, length - 1, height - 1);
        }

        public CubeInteger(Vector3Int size)
        {
            min = Vector3Int.zero;
            max = new(size.x - 1, size.y - 1, size.z - 1);
        }

        public CubeInteger(CubeInteger source)
        {
            min = source.min;
            max = source.max;
        }

        public CubeInteger(IKCube<Vector3Int> config)
        {
            if (config is null)
            {
                min = Vector3Int.zero;
                max = Vector3Int.zero;
                return;
            }

            min = config.min;
            max = config.max;
        }

        #endregion

        #region Equatable

        public bool Equals(CubeInteger other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public override bool Equals(object obj)
        {
            return obj is CubeInteger other && Equals(other);
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

        public IEnumerator<Vector3Int> GetEnumerator()
        {
            return this.GetAllPoints().GetEnumerator();
        }

        #endregion

        #region To String

        public override string ToString() => $"[{min}, {max}]";

        public string ToString(string format, IFormatProvider formatProvider) =>
            $"[{min.ToString(format, formatProvider)},{max.ToString(format, formatProvider)}]";

        #endregion
    }
}
