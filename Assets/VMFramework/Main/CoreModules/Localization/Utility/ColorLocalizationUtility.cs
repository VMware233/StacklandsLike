using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Localization
{
    public static class ColorLocalizationUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToLocalizedString(this Color color, ColorStringFormat format)
        {
            return format switch
            {
                ColorStringFormat.Name => color.GetLocalizedColorName(),
                ColorStringFormat.RGB => color.ToRGBString(),
                ColorStringFormat.RGBA => color.ToRGBAString(),
                ColorStringFormat.Hex => color.ToHexRGBString(),
                _ => throw new ArgumentOutOfRangeException(nameof(format), format,
                    null)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetLocalizedColorName(this Color color)
        {
            return new LocalizedString(color.GetColorName());
        }
    }
}