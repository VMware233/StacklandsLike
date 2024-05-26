using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial struct RangeInteger : IKSphere<int, int>
    {
        public readonly int center
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => pivot;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init
            {
                var radius = this.radius;
                min = value - radius;
                max = value + radius;
            }
        }

        public readonly int radius
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (max + min) / 2;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init
            {
                var pivot = this.pivot;
                min = pivot - value;
                max = pivot + value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int Clamp(int pos) => pos.Clamp(min, max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetRandomPointInside() => (min + 1).RandomRange(max - 1);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int GetRandomPointOnSurface()
        {
            var r = Random.value;

            if (r < 0.5f)
            {
                return min;
            }
            else
            {
                return max;
            }
        }
    }
}