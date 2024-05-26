using System;

namespace VMFramework.Core
{
    public interface IKSphere<TPoint, TRadius> : IKSet<TPoint>
        where TPoint : struct, IEquatable<TPoint>
        where TRadius : struct, IComparable<TRadius>
    {
        public TPoint center { get; init; }

        public TRadius radius { get; init; }

        public TPoint GetRelativePos(TPoint pos);

        public TPoint Clamp(TPoint pos);

        public TPoint GetRandomPointInside();

        public TPoint GetRandomPointOnSurface();
    }
}
