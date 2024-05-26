using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    /// <summary>
    /// K维整数立方体接口
    /// </summary>
    /// <typeparam name="TPoint"></typeparam>
    public interface IKCubeInteger<TPoint> : IKCube<TPoint>
        where TPoint : struct, IEquatable<TPoint>
    {
        /// <summary>
        /// 返回此K维立方体内的点的数量
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetPointsCount();
    }
}