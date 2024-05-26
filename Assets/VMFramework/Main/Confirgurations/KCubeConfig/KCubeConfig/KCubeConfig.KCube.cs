using System.Runtime.CompilerServices;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class KCubeConfig<TPoint>
    {
        TPoint IKCubeConfig<TPoint>.min
        {
            get => min;
            set => min = value;
        }

        TPoint IKCubeConfig<TPoint>.max
        {
            get => max;
            set => max = value;
        }

        TPoint IKCube<TPoint>.min
        {
            get => min;
            init => min = value;
        }

        TPoint IKCube<TPoint>.max
        {
            get => max;
            init => max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract TPoint ClampMin(TPoint pos);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract TPoint ClampMax(TPoint pos);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract TPoint GetRelativePos(TPoint pos);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract bool Contains(TPoint pos);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract TPoint GetRandomPoint();
    }
}