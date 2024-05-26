using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public readonly struct RangeChooser<T> : IChooser<T> where T : struct, IEquatable<T>
    {
        public readonly IKCube<T> range;

        public RangeChooser(IKCube<T> range)
        {
            this.range = range;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue()
        {
            return range.GetRandomPoint();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        object IChooser.GetValue()
        {
            return range.GetRandomPoint();
        }
    }
}