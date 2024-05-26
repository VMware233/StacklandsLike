using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class FlipType2DUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool flipX, bool flipY) ToBool(this FlipType2D flipType)
        {
            return flipType switch
            {
                FlipType2D.None => (false, false),
                FlipType2D.X => (true, false),
                FlipType2D.Y => (false, true),
                FlipType2D.XY => (true, true),
                _ => throw new ArgumentOutOfRangeException(nameof(flipType), flipType, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FlipType2D ToFlipType2D(this (bool flipX, bool flipY) flipBool)
        {
            return flipBool switch
            {
                (false, false) => FlipType2D.None,
                (true, false) => FlipType2D.X,
                (false, true) => FlipType2D.Y,
                (true, true) => FlipType2D.XY,
            };
        }
    }
}