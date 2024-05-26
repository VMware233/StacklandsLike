using System;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public interface IKCubeIntegerConfig<TPoint> : IKCubeConfig<TPoint>, IKCubeInteger<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {

    }
}