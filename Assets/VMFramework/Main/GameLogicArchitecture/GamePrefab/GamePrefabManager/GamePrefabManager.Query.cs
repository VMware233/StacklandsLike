using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public static partial class GamePrefabManager
    {
        #region Get All IDs

        /// <summary>
        /// 获取所有<see cref="IGamePrefab"/>的ID
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetAllIDs()
        {
            return allGamePrefabsByID.Keys;
        }

        /// <summary>
        /// 获取所有特定类型<see cref="IGamePrefab"/>的ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetAllIDs<T>() where T : IGamePrefab
        {
            return allGamePrefabsByGameType.Keys.Where(id => allGamePrefabsByGameType[id].Any(prefab => prefab is T));
        }

        /// <summary>
        /// 获取所有激活的<see cref="IGamePrefab"/>的ID
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetAllActiveIDs()
        {
            foreach (var (id, prefab) in allGamePrefabsByID)
            {
                if (prefab.isActive)
                {
                    yield return id;
                }
            }
        }

        /// <summary>
        /// 获取所有特定类型且激活的<see cref="IGamePrefab"/>的ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetAllActiveIDs<T>() where T : IGamePrefab
        {
            foreach (var (id, prefab) in allGamePrefabsByID)
            {
                if (prefab.isActive && prefab is T)
                {
                    yield return id;
                }
            }
        }

        #endregion

        #region Get Game Prefab

        /// <summary>
        /// 通过ID尝试获得<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="targetPrefab"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetGamePrefab(string id, out IGamePrefab targetPrefab)
        {
            if (id == null)
            {
                targetPrefab = default;
                return false;
            }
            
            return allGamePrefabsByID.TryGetValue(id, out targetPrefab);
        }

        /// <summary>
        /// 通过ID尝试获得特定类型的<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="targetPrefab"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetGamePrefab<T>(string id, out T targetPrefab)
            where T : IGamePrefab
        {
            if (id == null)
            {
                targetPrefab = default;
                return false;
            }
            
            if (allGamePrefabsByID.TryGetValue(id, out var prefab))
            {
                if (prefab is T typedPrefab)
                {
                    targetPrefab = typedPrefab;
                    return true;
                }
            }

            targetPrefab = default;
            return false;
        }

        /// <summary>
        /// 通过ID获得<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGamePrefab GetGamePrefab(string id)
        {
            if (id == null)
            {
                return default;
            }
            
            return allGamePrefabsByID.GetValueOrDefault(id);
        }

        /// <summary>
        /// 通过ID获得特定类型的<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetGamePrefab<T>(string id) where T : IGamePrefab
        {
            if (id == null)
            {
                return default;
            }
            
            if (allGamePrefabsByID.TryGetValue(id, out var prefab))
            {
                if (prefab is T typedPrefab)
                {
                    return typedPrefab;
                }
            }
            
            return default;
        }

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
        /// 通过ID获得激活的<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGamePrefab GetActiveGamePrefab(string id)
        {
            if (TryGetGamePrefab(id, out var prefab) == false)
            {
                return default;
            }

            return prefab.isActive ? prefab : default;
        }

        /// <summary>
        /// 通过ID尝试获得激活的<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="targetPrefab"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetActiveGamePrefab(string id, out IGamePrefab targetPrefab)
        {
            if (TryGetGamePrefab(id, out targetPrefab) == false)
            {
                return false;
            }
            
            return targetPrefab.isActive;
        }

        /// <summary>
        /// 通过ID获得激活的特定类型的<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetActiveGamePrefab<T>(string id) where T : IGamePrefab
        {
            if (TryGetActiveGamePrefab(id, out var prefab) == false)
            {
                return default;
            }

            if (prefab is T typedPrefab)
            {
                return typedPrefab;
            }

            return default;
        }

        /// <summary>
        /// 通过ID尝试获得激活的特定类型的<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="targetPrefab"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetActiveGamePrefab<T>(string id, out T targetPrefab) where T : IGamePrefab
        {
            if (TryGetActiveGamePrefab(id, out var prefab) == false)
            {
                targetPrefab = default;
                return false;
            }
            
            if (prefab is T typedPrefab)
            {
                targetPrefab = typedPrefab;
                return true;
            }

            targetPrefab = default;
            return false;
        }
        
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

        #region Get All Game Prefab

        /// <summary>
        /// 获取所有<see cref="IGamePrefab"/>
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGamePrefab> GetAllGamePrefabs()
        {
            return allGamePrefabsByID.Values;
        }
        
        /// <summary>
        /// 获取所有特定类型的<see cref="IGamePrefab"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetAllGamePrefabs<T>() where T : IGamePrefab
        {
            foreach (var prefab in allGamePrefabsByID.Values)
            {
                if (prefab is T typedPrefab)
                {
                    yield return typedPrefab;
                }
            }
        }
        
        /// <summary>
        /// 获取所有特定类型的<see cref="IGamePrefab"/>
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGamePrefab> GetAllGamePrefabs(Type gamePrefabType)
        {
            foreach (var prefab in allGamePrefabsByID.Values)
            {
                if (prefab.GetType().IsDerivedFrom(gamePrefabType, false))
                {
                    yield return prefab;
                }
            }
        }

        /// <summary>
        /// 获取所有激活的<see cref="IGamePrefab"/>
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGamePrefab> GetAllActiveGamePrefabs()
        {
            foreach (var prefab in allGamePrefabsByID.Values)
            {
                if (prefab.isActive)
                {
                    yield return prefab;
                }
            }
        }

        /// <summary>
        /// 获取所有激活的特定类型的<see cref="IGamePrefab"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetAllActiveGamePrefabs<T>() where T : IGamePrefab
        {
            foreach (var prefab in allGamePrefabsByID.Values)
            {
                if (prefab.isActive && prefab is T typedPrefab)
                {
                    yield return typedPrefab;
                }
            }
        }

        #endregion

        #region Get Random Game Prefab

        /// <summary>
        /// 获取随机的<see cref="IGamePrefab"/>
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGamePrefab GetRandomGamePrefab()
        {
            return allGamePrefabsByID.Values.ChooseOrDefault();
        }
        
        /// <summary>
        /// 获取随机的特定类型的<see cref="IGamePrefab"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandomGamePrefab<T>() where T : IGamePrefab
        {
            return GetAllGamePrefabs<T>().ChooseOrDefault();
        }

        #endregion

        #region Contains Game Prefab
        
        /// <summary>
        /// 通过ID查询是否包含<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsGamePrefab(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return false;
            }
            
            return allGamePrefabsByID.ContainsKey(id);
        }

        /// <summary>
        /// 通过ID查询是否包含特定类型的<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsGamePrefab<T>(string id) where T : IGamePrefab
        {
            if (id.IsNullOrEmpty())
            {
                return false;
            }
            
            if (allGamePrefabsByID.TryGetValue(id, out var prefab))
            {
                return prefab is T;
            }

            return false;
        }
        
        /// <summary>
        /// 通过ID查询是否包含激活的<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsActiveGamePrefab(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return false;
            }
            
            if (allGamePrefabsByID.TryGetValue(id, out var prefab))
            {
                return prefab.isActive;
            }

            return false;
        }

        /// <summary>
        /// 通过ID查询是否包含激活的特定类型的<see cref="IGamePrefab"/>
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsActiveGamePrefab<T>(string id) where T : IGamePrefab
        {
            if (id.IsNullOrEmpty())
            {
                return false;
            }
            
            if (allGamePrefabsByID.TryGetValue(id, out var prefab))
            {
                return prefab.isActive && prefab is T;
            }

            return false;
        }

        #endregion

        #region Get Game Prefabs Count

        public static int GetGamePrefabsCount()
        {
            return allGamePrefabsByID.Count;
        }

        #endregion
    }
}
