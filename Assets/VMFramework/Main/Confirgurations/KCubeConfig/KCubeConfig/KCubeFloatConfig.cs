using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;

namespace VMFramework.Configuration
{
    public abstract class KCubeFloatConfig<TPoint> : KCubeConfig<TPoint>, IKCubeFloatConfig<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {
        protected virtual string extentsName => "半径";

        [LabelText("@" + nameof(extentsName)), VerticalGroup(INFO_VALUE_GROUP)]
        [ShowInInspector, DisplayAsString]
        public abstract TPoint extents
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }
    }
}