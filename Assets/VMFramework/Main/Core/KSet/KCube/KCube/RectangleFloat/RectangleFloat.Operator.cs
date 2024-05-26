using UnityEngine;

namespace VMFramework.Core
{
    public partial struct RectangleFloat
    {
        public static RectangleFloat operator +(RectangleFloat a, Vector2 b) =>
            new(a.min + b, a.max + b);

        public static RectangleFloat operator -(RectangleFloat a, Vector2 b) =>
            new(a.min - b, a.max - b);

        public static RectangleFloat operator *(RectangleFloat a, Vector2 b)
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

            return new RectangleFloat(xMin, yMin, xMax, yMax);
        }

        public static RectangleFloat operator *(RectangleFloat a, float b)
        {
            if (b >= 0)
            {
                return new(a.min * b, a.max * b);
            }

            return new(a.max * b, a.min * b);
        }

        public static RectangleFloat operator /(RectangleFloat a, Vector2 b)
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

            return new RectangleFloat(xMin, yMin, xMax, yMax);
        }

        public static RectangleFloat operator /(RectangleFloat a, float b)
        {
            if (b >= 0)
            {
                return new(a.min / b, a.max / b);
            }

            return new(a.max / b, a.min / b);
        }

        public static RectangleFloat operator -(RectangleFloat a) =>
            new(-a.max, -a.min);

        public static CubeFloat operator *(RectangleFloat a, RangeFloat b) =>
            new(a, b);

        public static CubeFloat operator *(RangeFloat a, RectangleFloat b) =>
            new(a, b);

        public static bool operator ==(RectangleFloat a, RectangleFloat b) =>
            a.Equals(b);

        public static bool operator !=(RectangleFloat a, RectangleFloat b) =>
            !a.Equals(b);
    }
}