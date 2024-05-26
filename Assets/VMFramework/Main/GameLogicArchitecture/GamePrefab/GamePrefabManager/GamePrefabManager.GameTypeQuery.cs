using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabManager
    {
        #region Get Game Prefab By Game Type

        /// <summary>
        /// 通过游戏类型ID获取<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="gameTypeID"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGamePrefab> GetGamePrefabsByGameType(string gameTypeID)
        {
            if (allGamePrefabsByGameType.TryGetValue(gameTypeID, out var gamePrefabs))
            {
                return gamePrefabs;
            }
            
            return Enumerable.Empty<IGamePrefab>();
        }
        
        /// <summary>
        /// 通过游戏类型ID获取特定类型的<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="gameTypeID"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetGamePrefabsByGameType<T>(string gameTypeID) where T : IGamePrefab
        {
            if (allGamePrefabsByGameType.TryGetValue(gameTypeID, out var gamePrefabs))
            {
                foreach (var gamePrefab in gamePrefabs)
                {
                    if (gamePrefab is T typedPrefab)
                    {
                        yield return typedPrefab;
                    }
                }
            }
        }

        #endregion

        #region Get Random Game Prefab By Game Type

        /// <summary>
        /// 通过游戏类型ID获取随机的<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="gameTypeID"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGamePrefab GetRandomGamePrefabByGameType(string gameTypeID)
        {
            return GetGamePrefabsByGameType(gameTypeID).ChooseOrDefault();
        }
        
        /// <summary>
        ///  通过游戏类型ID获取随机的特定类型的<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="gameTypeID"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandomGamePrefabByGameType<T>(string gameTypeID) where T : IGamePrefab
        {
            return GetGamePrefabsByGameType<T>(gameTypeID).ChooseOrDefault();
        }

        #endregion

        #region Get IDs By Game Type
        
        /// <summary>
        /// 获取特定游戏类型的ID列表
        /// </summary>
        /// <param name="gameTypeID"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetIDsByGameType(string gameTypeID)
        {
            if (allGamePrefabsByGameType.TryGetValue(gameTypeID, out var gamePrefabs))
            {
                return gamePrefabs.Select(p => p.id);
            }
            
            return Enumerable.Empty<string>();
        }
        
        /// <summary>
        /// 获取特定游戏类型的ID列表
        /// </summary>
        /// <param name="gameTypeID"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetIDsByGameType<T>(string gameTypeID) where T : IGamePrefab
        {
            if (allGamePrefabsByGameType.TryGetValue(gameTypeID, out var gamePrefabs))
            {
                return gamePrefabs.Where(p => p is T).Select(p => p.id);
            }
            
            return Enumerable.Empty<string>();
        }

        #endregion

        #region Get Random ID By Game Type

        /// <summary>
        /// 获取特定游戏类型的随机ID
        /// </summary>
        /// <param name="gameTypeID"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetRandomIDByGameType(string gameTypeID)
        {
            return GetIDsByGameType(gameTypeID).ChooseOrDefault();
        }
        
        /// <summary>
        /// 获取特定游戏类型的随机ID
        /// </summary>
        /// <param name="gameTypeID"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetRandomIDByGameType<T>(string gameTypeID) where T : IGamePrefab
        {
            return GetIDsByGameType<T>(gameTypeID).ChooseOrDefault();
        }

        #endregion

        #region Contains Game Type

        /// <summary>
        /// 所有的<see cref="IGamePrefab"/>里是否包含特定游戏类型
        /// </summary>
        /// <param name="gameTypeID"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsGameType(string gameTypeID)
        {
            return allGamePrefabsByGameType.ContainsKey(gameTypeID);
        }

        #endregion
    }
}