using System.Runtime.CompilerServices;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public partial interface IGameItem
    {
        /// <summary>
        /// 当被复制时调用
        /// </summary>
        /// <param name="other"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void OnClone(IGameItem other);
        
        /// <summary>
        /// 获得复制
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TGameItem GetClone<TGameItem>(TGameItem instance) where TGameItem : IGameItem
        {
            instance.AssertIsNotNull(nameof(instance));
            
            var clone = Create(instance.id);

            clone.OnClone(instance);

            return (TGameItem)clone;
        }
    }
}