using System;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public interface IKCubeFloatConfig<TPoint> : IKCubeConfig<TPoint>, IKCubeFloat<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {
        
    }
}