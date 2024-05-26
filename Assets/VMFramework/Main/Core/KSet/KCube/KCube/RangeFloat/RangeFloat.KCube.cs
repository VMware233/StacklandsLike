using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public partial struct RangeFloat
    {
        float IKCube<float>.min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => min = value;
        }

        float IKCube<float>.max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(float pos) => pos >= min && pos <= max;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetRelativePos(float pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ClampMin(float pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ClampMax(float pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetRandomPoint() => min.RandomRange(max);
    }
}