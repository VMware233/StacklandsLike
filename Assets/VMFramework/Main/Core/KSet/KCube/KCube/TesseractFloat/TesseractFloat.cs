using System;
using UnityEngine;

namespace VMFramework.Core
{
    public readonly partial struct TesseractFloat : IKCubeFloat<Vector4>, IEquatable<TesseractFloat>,
        IFormattable
    {
        public static TesseractFloat zero { get; } = new(Vector4.zero, Vector4.zero);

        public static TesseractFloat one { get; } = new(Vector4.one, Vector4.one);

        public static TesseractFloat unit { get; } = new(Vector4.zero, Vector4.one);

        public Vector4 size => max - min;

        public Vector4 pivot => (max + min) / 2;

        public Vector4 extents => (max - min) / 2;

        public readonly Vector4 min, max;

        public RangeFloat xRange => new(min.x, max.x);

        public RangeFloat yRange => new(min.y, max.y);

        public RangeFloat zRange => new(min.z, max.z);

        public RangeFloat wRange => new(min.w, max.w);

        public RectangleFloat xyRectangle =>
            new(min.x, min.y, max.x, max.y);

        public RectangleFloat xzRectangle =>
            new(min.x, min.z, max.x, max.z);

        public RectangleFloat yzRectangle =>
            new(min.y, min.z, max.y, max.z);

        public RectangleFloat xwRectangle =>
            new(min.x, min.w, max.x, max.w);

        public RectangleFloat ywRectangle =>
            new(min.y, min.w, max.y, max.w);

        public RectangleFloat zwRectangle =>
            new(min.z, min.w, max.z, max.w);

        public CubeFloat xyzCube =>
            new(min.x, min.y, min.z, max.x, max.y, max.z);

        public CubeFloat xywCube =>
            new(min.x, min.y, min.w, max.x, max.y, max.w);

        public CubeFloat xzwCube =>
            new(min.x, min.z, min.w, max.x, max.z, max.w);

        public CubeFloat yzwCube =>
            new(min.y, min.z, min.w, max.y, max.z, max.w);

        #region Constructor

        public TesseractFloat(RangeFloat xRange, RangeFloat yRange, RangeFloat zRange, RangeFloat wRange)
        {
            min = new Vector4(xRange.min, yRange.min, zRange.min, wRange.min);
            max = new Vector4(xRange.max, yRange.max, zRange.max, wRange.max);
        }

        public TesseractFloat(CubeFloat xyzCube, RangeFloat wRange)
        {
            min = new Vector4(xyzCube.min.x, xyzCube.min.y, xyzCube.min.z, wRange.min);
            max = new Vector4(xyzCube.max.x, xyzCube.max.y, xyzCube.max.z, wRange.max);
        }

        public TesseractFloat(RangeFloat xRange, CubeFloat yzwCube)
        {
            min = new Vector4(xRange.min, yzwCube.min.x, yzwCube.min.y, yzwCube.min.z);
            max = new Vector4(xRange.max, yzwCube.max.x, yzwCube.max.y, yzwCube.max.z);
        }

        public TesseractFloat(float xMin, float yMin, float zMin, float wMin, float xMax, float yMax,
            float zMax, float wMax)
        {
            min = new Vector4(xMin, yMin, zMin, wMin);
            max = new Vector4(xMax, yMax, zMax, wMax);
        }

        public TesseractFloat(Vector4 min, Vector4 max)
        {
            this.min = min;
            this.max = max;
        }

        public TesseractFloat(float sizeX, float sizeY, float sizeZ, float sizeW)
        {
            min = Vector4.zero;
            max = new Vector4(sizeX, sizeY, sizeZ, sizeW);
        }

        public TesseractFloat(Vector4 size)
        {
            min = Vector4.zero;
            max = size;
        }

        public TesseractFloat(TesseractFloat source)
        {
            min = source.min;
            max = source.max;
        }

        public TesseractFloat(IKCube<Vector4> config)
        {
            if (config is null)
            {
                min = Vector4.zero;
                max = Vector4.zero;
                return;
            }

            min = config.min;
            max = config.max;
        }

        #endregion

        #region Equatable

        public bool Equals(TesseractFloat other)
        {
            return min.Equals(other.min) && max.Equals(other.max);
        }

        public override bool Equals(object obj)
        {
            return obj is TesseractFloat other && Equals(other);
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