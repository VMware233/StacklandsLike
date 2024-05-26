using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    /// <summary>
    /// K维立方体接口
    /// </summary>
    /// <typeparam name="TPoint">K维立方体的点类型</typeparam>
    public interface IKCube<TPoint> : IKSet<TPoint> where TPoint : struct, IEquatable<TPoint>
    {
        /// <summary>
        /// K维立方体的最小点
        /// </summary>
        public TPoint min { get; init; }

        /// <summary>
        /// K维立方体的最大点
        /// </summary>
        public TPoint max { get; init; }

        /// <summary>
        /// K维立方体的大小
        /// </summary>
        public TPoint size { get; }

        /// <summary>
        /// K维立方体的中心点
        /// </summary>
        public TPoint pivot { get; }

        /// <summary>
        /// 返回一个点相对于K维立方体的位置
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TPoint GetRelativePos(TPoint pos);

        /// <summary>
        /// 确保这个点比K维立方体的最小点大
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TPoint ClampMin(TPoint pos);

        /// <summary>
        /// 确保这个点比K维立方体的最大点小
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TPoint ClampMax(TPoint pos);

        /// <summary>
        /// 获取K维立方体内的一个随机点
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TPoint GetRandomPoint();
    }
}
