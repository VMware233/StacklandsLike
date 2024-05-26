using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial struct ColorRange
    {
        Color IKCube<Color>.min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => min = value;
        }

        Color IKCube<Color>.max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Color pos) => pos.r >= min.r && pos.r <= max.r &&
                                           pos.g >= min.g && pos.g <= max.g &&
                                           pos.b >= min.b && pos.b <= max.b &&
                                           pos.a >= min.a && pos.a <= max.a;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color GetRelativePos(Color pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color ClampMin(Color pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color ClampMax(Color pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color GetRandomPoint() => min.RandomRange(max);
    }
}