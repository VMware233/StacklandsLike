using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class RichTextTagUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string AlignTag(this string text, TextAlignment alignment)
        {
            var alignmentText = alignment switch
            {
                TextAlignment.Left => "left",
                TextAlignment.Center => "center",
                TextAlignment.Right => "right",
                _ => throw new ArgumentOutOfRangeException(nameof(alignment),
                    alignment, null)
            };
            return $@"<align=""{alignmentText}"">{text}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ColorTag(this string text, string color)
        {
            return $@"<color=""{color}"">{text}</color>";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ColorTag(this string text, Color color)
        {
            return $@"<color={color.ToHexRGBAString()}>{text}</color>";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string AlphaTag(this string text, float alpha)
        {
            return $"<alpha=#{alpha.ToHexAlphaString()}>{text}</color>";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BoldTag(this string text)
        {
            return $"<b>{text}</b>";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ItalicTag(this string text)
        {
            return $"<i>{text}</i>";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string MarkTag(this string text, Color color)
        {
            return $"<mark={color.ToHexRGBAString()}>{text}</mark>";
        }
    }
}
