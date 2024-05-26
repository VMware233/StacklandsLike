using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial struct RangeFloat : IKSphere<float, float>
    {
        public readonly float radius
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => extents;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init
            {
                var pivot = this.pivot;
                min = pivot - value;
                max = pivot + value;
            }
        }

        public readonly float center
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => pivot;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init
            {
                var radius = extents;
                min = value - radius;
                max = value + radius;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly float Clamp(float pos) => pos.Clamp(min, max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly float GetRandomPointInside() => this.GetRandomPoint();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly float GetRandomPointOnSurface()
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