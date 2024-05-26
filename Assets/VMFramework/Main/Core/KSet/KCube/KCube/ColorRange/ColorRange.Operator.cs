using UnityEngine;

namespace VMFramework.Core
{
    public partial struct ColorRange
    {
        public static ColorRange operator +(ColorRange a, Color b) =>
            new(a.min + b, a.max + b);

        public static ColorRange operator -(ColorRange a, Color b) =>
            new(a.min - b, a.max - b);

        public static ColorRange operator *(ColorRange a, Color b) =>
            new(a.min.Multiply(b), a.max.Multiply(b));

        public static ColorRange operator *(ColorRange a, float b) =>
            new(a.min * b, a.max * b);

        public static ColorRange operator /(ColorRange a, Color b) =>
            new(a.min.Divide(b), a.max.Divide(b));

        public static ColorRange operator /(ColorRange a, float b) =>
            new(a.min / b, a.max / b);

        public static bool operator ==(ColorRange a, ColorRange b) =>
            a.Equals(b);

        public static bool operator !=(ColorRange a, ColorRange b) =>
            !a.Equals(b);
    }
}