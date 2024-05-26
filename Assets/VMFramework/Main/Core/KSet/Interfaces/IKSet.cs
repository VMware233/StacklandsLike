using System;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    /// <summary>
    /// K维集合接口
    /// </summary>
    /// <typeparam name="TPoint"></typeparam>
    public interface IKSet<in TPoint> where TPoint : struct, IEquatable<TPoint>
    {
        /// <summary>
        /// 判断一个点是否在K维集合内
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(TPoint pos);
    }
}