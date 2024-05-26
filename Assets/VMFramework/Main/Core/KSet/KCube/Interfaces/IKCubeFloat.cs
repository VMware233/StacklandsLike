using System;

namespace VMFramework.Core
{
    /// <summary>
    /// K维浮点立方体接口
    /// </summary>
    /// <typeparam name="TPoint"></typeparam>
    public interface IKCubeFloat<TPoint> : IKCube<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {
        /// <summary>
        /// K维立方体中心到边界的距离，或者说是半径
        /// </summary>
        public TPoint extents { get; }
    }
}