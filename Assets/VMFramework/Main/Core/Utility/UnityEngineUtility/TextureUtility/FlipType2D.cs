using System;

namespace VMFramework.Core
{
    [Flags]
    public enum FlipType2D
    {
        None = 0,
        X = 1,
        Y = 2,
        XY = 4,
        ALL = X | Y | XY
    }
}