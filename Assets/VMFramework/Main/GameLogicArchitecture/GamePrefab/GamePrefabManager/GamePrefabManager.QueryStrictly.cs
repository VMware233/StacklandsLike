using System;
using System.Runtime.CompilerServices;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabManager
    {
        #region Get Game Prefab

        /// <summary>
        /// 通过ID获得<see cref="IGamePrefab"/>，如果没有找到则会报错
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGamePrefab GetGamePrefabStrictly(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (id == "")
            {
                throw new ArgumentException("id不能为空字符串！");
            }

            if (allGamePrefabsByID.TryGetValue(id, out var prefab))
            {
                return prefab;
            }

            throw new ArgumentException($"不存在id为{id}的{nameof(IGamePrefab)}");
        }

        /// <summary>
        /// 通过ID获得特定类型的<see cref="IGamePrefab"/>，如果没有找到则会报错
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetGamePrefabStrictly<T>(string id) where T : IGamePrefab
        {
            var prefab = GetGamePrefabStrictly(id);

            if (prefab is T typedGamePrefab)
            {
                return typedGamePrefab;
            }

            throw new ArgumentException($"id为{id}的{nameof(IGamePrefab)}类型不是{typeof(T)}");
        }

        #endregion

        #region Get Active Game Prefab

        /// <summary>
        /// 通过ID获得激活的<see cref="IGamePrefab"/>，如果没有找到则会报错
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGamePrefab GetActiveGamePrefabStrictly(string id)
        {
            var prefab = GetGamePrefabStrictly(id);

            if (prefab.isActive == false)
            {
                throw new ArgumentException($"id为{id}的{nameof(IGamePrefab)}没有激活！");
            }
            
            return prefab;
        }

        /// <summary>
        /// 通过ID获得激活的特定类型的<see cref="IGamePrefab"/>，如果没有找到则会报错
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetActiveGamePrefabStrictly<T>(string id) where T : IGamePrefab
        {
            var prefab = GetActiveGamePrefabStrictly(id);

            if (prefab is T typedPrefab)
            {
                return typedPrefab;
            }

            throw new ArgumentException($"id为{id}的{nameof(IGamePrefab)}类型不是{typeof(T)}");
        }

        #endregion
    }
}