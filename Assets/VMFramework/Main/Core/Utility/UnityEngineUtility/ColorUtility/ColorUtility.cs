using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core {
    public static class ColorUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ChangeAlpha(this Color oldColor, float newAlpha) {
            return new Color(oldColor.r, oldColor.g, oldColor.b, newAlpha);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ChangeAlpha(this Color oldColor, int newAlpha) {
            return new Color(oldColor.r, oldColor.g, oldColor.b, newAlpha / 255f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int To256(this float a)
        {
            return (a.Clamp01() * 256).Round();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToRGBString(this Color color)
        {
            return $"({color.r.To256()},{color.g.To256()},{color.b.To256()})";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToRGBAString(this Color color)
        {
            return $"({color.r.To256()},{color.g.To256()},{color.b.To256()},{color.a.Percent()})";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToHexRGBString(this Color color)
        {
            return "#" + UnityEngine.ColorUtility.ToHtmlStringRGB(color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToHexRGBAString(this Color color)
        {
            return "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(color);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToHexAlphaString(this float alpha)
        {
            return "#" + alpha.ToHexString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToHexAlphaString(this int alpha)
        {
            return "#" + alpha.ToHexString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToString(this Color color, ColorStringFormat format)
        {
            return format switch
            {
                ColorStringFormat.Name => color.GetColorName(),
                ColorStringFormat.RGB => color.ToRGBString(),
                ColorStringFormat.RGBA => color.ToRGBAString(),
                ColorStringFormat.Hex => color.ToHexRGBString(),
                _ => throw new ArgumentOutOfRangeException(nameof(format), format,
                    null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetColorName(this Color color)
        {
            var nearestColor = color.MinDistanceBy(DistanceType.Manhattan, true,
                ColorDefinitions.colorDictionary, kvp => kvp.Value);

            return nearestColor.Key;
        }
    }
}

