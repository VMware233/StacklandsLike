using System.Runtime.CompilerServices;

namespace VMFramework.GameLogicArchitecture
{
    public static class IGameItemUtility
    {
        /// <summary>
        /// 获得复制
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TGameItem GetClone<TGameItem>(this TGameItem instance) where TGameItem : IGameItem
        {
            return IGameItem.GetClone(instance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TGameItem Create<TGameItem>(this IGamePrefab gamePrefab)
            where TGameItem : IGameItem
        {
            return IGameItem.Create<TGameItem>(gamePrefab.id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGameItem Create(this IGamePrefab gamePrefab)
        {
            return IGameItem.Create(gamePrefab.id);
        }
    }
}