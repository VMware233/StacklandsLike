using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Configuration
{
    public abstract class KCubeIntegerConfig<TPoint> : KCubeConfig<TPoint>, IKCubeIntegerConfig<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract int GetPointsCount();
    }
}