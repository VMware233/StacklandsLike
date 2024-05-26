using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public partial struct RangeInteger
    {
        int IKCube<int>.min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => min = value;
        }

        int IKCube<int>.max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(int pos) => pos >= min && pos <= max;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetRelativePos(int pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ClampMin(int pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ClampMax(int pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetPointsCount() => size;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetRandomPoint() => min.RandomRange(max);
    }
}