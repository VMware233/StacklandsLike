using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public readonly partial struct RectangleInteger : IKCubeInteger<Vector2Int>, IEquatable<RectangleInteger>, 
        IFormattable, IEnumerable<Vector2Int>
    {
        /// <summary>
        /// 创建一个[0, 0]x[0, 0]的整数矩形
        /// </summary>
        public static RectangleInteger zero { get; } =
            new(Vector2Int.zero, Vector2Int.zero);

        /// <summary>
        /// 创建一个[1, 1]x[1, 1]的整数矩形
        /// </summary>
        public static RectangleInteger one { get; } =
            new(Vector2Int.one, Vector2Int.one);

        /// <summary>
        /// 创建一个[0, 1]x[0, 1]的整数矩形
        /// </summary>
        public static RectangleInteger unit { get; } =
            new(Vector2Int.zero, Vector2Int.one);

        public Vector2Int size => max - min + Vector2Int.one;

        public Vector2Int pivot => (max + min) / 2;

        public readonly Vector2Int min, max;

        public RangeInteger xRange => new(min.x, max.x);

        public RangeInteger yRange => new(min.y, max.y);

        #region Constructor

        public RectangleInteger(RangeInteger xRange, RangeInteger yRange)
        {
            min = new Vector2Int(xRange.min, yRange.min);
            max = new Vector2Int(xRange.max, yRange.max);
        }

        public RectangleInteger(int xMin, int yMin, int xMax, int yMax)
        {
            min = new Vector2Int(xMin, yMin);
            max = new Vector2Int(xMax, yMax);
        }

        public RectangleInteger(Vector2Int min, Vector2Int max)
        {
            this.min = min;
            this.max = max;
        }

        public RectangleInteger(int width, int length)
        {
            min = Vector2Int.zero;
            max = new Vector2Int(width - 1, length - 1);
        }

        public RectangleInteger(Vector2Int size)
        {
            min = Vector2Int.zero;
            max = new(size.x - 1, size.y - 1);
        }

        public RectangleInteger(RectangleInteger source)
        {
            min = source.min;
            max = source.max;
        }

        public RectangleInteger(IKCube<Vector2Int> config)
        {
            if (config is null)
            {
                min = Vector2Int.zero;
                max = Vector2Int.zero;
                return;
            }
            min = config.min;
            max = config.max;
        }

        #endregion

        #region Geometry Extension

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsOnBoundary(Vector2Int pos, out FourTypesDirection2D directions)
        {
            return pos.IsOnBoundary(min, max, out directions);
        }

        #endregion

        #region Equatable

        public bool Equals(RectangleInteger other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public override bool Equals(object obj)
        {
            return obj is RectangleInteger other && Equals(other);
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

        public IEnumerator<Vector2Int> GetEnumerator()
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
