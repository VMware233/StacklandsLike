using UnityEngine;

namespace VMFramework.Core
{
    public partial struct RectangleInteger
    {
        public static RectangleInteger operator
            +(RectangleInteger a, Vector2Int b) =>
            new(a.min + b, a.max + b);

        public static RectangleInteger operator
            -(RectangleInteger a, Vector2Int b) =>
            new(a.min - b, a.max - b);

        public static RectangleInteger operator *(RectangleInteger a, int b)
        {
            if (b >= 0)
            {
                return new RectangleInteger(a.min * b, a.max * b);
            }

            return new RectangleInteger(a.max * b, a.min * b);
        }

        public static RectangleInteger operator
            *(RectangleInteger a, Vector2Int b)
        {
            var xMin = a.min.x;
            var xMax = a.max.x;

            if (b.x >= 0)
            {
                xMin *= b.x;
                xMax *= b.x;
            }
            else
            {
                (xMin, xMax) = (xMax * b.x, xMin * b.x);
            }

            var yMin = a.min.y;
            var yMax = a.max.y;

            if (b.y >= 0)
            {
                yMin *= b.y;
                yMax *= b.y;
            }
            else
            {
                (yMin, yMax) = (yMax * b.y, yMin * b.y);
            }

            return new RectangleInteger(xMin, yMin, xMax, yMax);
        }

        public static RectangleInteger operator /(RectangleInteger a, int b)
        {
            if (b >= 0)
            {
                return new(a.min / b, a.max / b);
            }

            return new(a.max / b, a.min / b);
        }

        public static RectangleInteger operator
            /(RectangleInteger a, Vector2Int b)
        {
            var xMin = a.min.x;
            var xMax = a.max.x;

            if (b.x >= 0)
            {
                xMin /= b.x;
                xMax /= b.x;
            }
            else
            {
                (xMin, xMax) = (xMax / b.x, xMin / b.x);
            }

            var yMin = a.min.y;
            var yMax = a.max.y;

            if (b.y >= 0)
            {
                yMin /= b.y;
                yMax /= b.y;
            }
            else
            {
                (yMin, yMax) = (yMax / b.y, yMin / b.y);
            }

            return new RectangleInteger(xMin, yMin, xMax, yMax);
        }

        public static RectangleInteger operator -(RectangleInteger a) =>
            new(-a.max, -a.min);

        public static CubeInteger operator *(RectangleInteger a, RangeInteger b) =>
            new(a, b);

        public static CubeInteger operator *(RangeInteger a, RectangleInteger b) =>
            new(a, b);

        public static bool operator ==(RectangleInteger a, RectangleInteger b) =>
            a.Equals(b);

        public static bool operator !=(RectangleInteger a, RectangleInteger b) =>
            !a.Equals(b);
    }
}