using System;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public interface IKCubeConfig<TPoint> : IKCube<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {
        public new TPoint min { get; set; }

        public new TPoint max { get; set; }

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
    }
}