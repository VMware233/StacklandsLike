using UnityEngine;

namespace VMFramework.Core
{
    public partial struct TesseractFloat
    {
        public static TesseractFloat operator +(TesseractFloat a, Vector4 b) =>
            new(a.min + b, a.max + b);

        public static TesseractFloat operator -(TesseractFloat a, Vector4 b) =>
            new(a.min - b, a.max - b);

        public static TesseractFloat operator *(TesseractFloat a, Vector4 b)
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

            var zMin = a.min.z;
            var zMax = a.max.z;

            if (b.z >= 0)
            {
                zMin *= b.z;
                zMax *= b.z;
            }
            else
            {
                (zMin, zMax) = (zMax * b.z, zMin * b.z);
            }

            var wMin = a.min.w;
            var wMax = a.max.w;

            if (b.w >= 0)
            {
                wMin *= b.w;
                wMax *= b.w;
            }
            else
            {
                (wMin, wMax) = (wMax * b.w, wMin * b.w);
            }

            return new(xMin, yMin, zMin, wMin, xMax, yMax, zMax, wMax);
        }

        public static TesseractFloat operator *(TesseractFloat a, float b)
        {
            if (b >= 0)
            {
                return new(a.min * b, a.max * b);
            }

            return new(a.max * b, a.min * b);
        }

        public static TesseractFloat operator /(TesseractFloat a, Vector4 b)
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

            var zMin = a.min.z;
            var zMax = a.max.z;

            if (b.z >= 0)
            {
                zMin /= b.z;
                zMax /= b.z;
            }
            else
            {
                (zMin, zMax) = (zMax / b.z, zMin / b.z);
            }

            var wMin = a.min.w;
            var wMax = a.max.w;

            if (b.w >= 0)
            {
                wMin /= b.w;
                wMax /= b.w;
            }
            else
            {
                (wMin, wMax) = (wMax / b.w, wMin / b.w);
            }

            return new(xMin, yMin, zMin, wMin, xMax, yMax, zMax, wMax);
        }

        public static TesseractFloat operator /(TesseractFloat a, float b)
        {
            if (b >= 0)
            {
                return new(a.min / b, a.max / b);
            }

            return new(a.max / b, a.min / b);
        }

        public static TesseractFloat operator -(TesseractFloat a) =>
            new(-a.max, -a.min);

        public static bool operator ==(TesseractFloat a, TesseractFloat b) =>
            a.Equals(b);

        public static bool operator !=(TesseractFloat a, TesseractFloat b) =>
            !a.Equals(b);
    }
}