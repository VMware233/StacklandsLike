using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabManager
    {
        #region Get Game Prefab

        /// <summary>
        /// 通过ID尝试获得<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="targetPrefab"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetGamePrefabWithWarning(string id, out IGamePrefab targetPrefab)
        {
            if (id.CheckIsNull(nameof(id)) == false)
            {
                targetPrefab = default;
                return false;
            }

            if (allGamePrefabsByID.TryGetValue(id, out targetPrefab))
            {
                return true;
            }
            
            Debug.LogWarning($"GamePrefab with ID {id} not found.");
            return false;
        }

        /// <summary>
        /// 通过ID尝试获得特定类型的<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="targetPrefab"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetGamePrefabWithWarning<T>(string id, out T targetPrefab)
            where T : IGamePrefab
        {
            if (id.CheckIsNull(nameof(id)) == false)
            {
                targetPrefab = default;
                return false;
            }
            
            if (allGamePrefabsByID.TryGetValue(id, out var prefab) == false)
            {
                Debug.LogWarning($"GamePrefab with ID {id} not found.");
                targetPrefab = default;
                return false;
            }
            
            if (prefab is T typedPrefab)
            {
                targetPrefab = typedPrefab;
                return true;
            }

            Debug.LogWarning($"GamePrefab with ID {id} is not of type {typeof(T)}.");
            targetPrefab = default;
            return false;
        }

        #endregion
    }
}