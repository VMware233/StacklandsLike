#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using VMFramework.Core;
using VMFramework.Core.Editor;

namespace VMFramework.GameLogicArchitecture
{
    public static class GamePrefabWrapperQuery
    {
        public static IEnumerable<GamePrefabWrapper> GetAllGamePrefabWrappers()
        {
            return ConfigurationPath.RESOURCES_PATH.FindAssetsOfType<GamePrefabWrapper>();
        }

        public static bool TryGetGamePrefabWrapper(string id, out GamePrefabWrapper wrapper)
        {
            wrapper = GetGamePrefabWrapper(id);
            return wrapper != null;
        }

        public static GamePrefabWrapper GetGamePrefabWrapper(string id)
        {
            if (GamePrefabManager.TryGetGamePrefab(id, out var gamePrefab) == false)
            {
                return null;
            }
            
            return GetGamePrefabWrapper(gamePrefab);
        }

        public static GamePrefabWrapper GetGamePrefabWrapper(IGamePrefab gamePrefab)
        {
            return GetGamePrefabWrappers(gamePrefab).FirstOrDefault();
        }

        public static IEnumerable<GamePrefabWrapper> GetGamePrefabWrappers(IGamePrefab gamePrefab)
        {
            if (gamePrefab == null)
            {
                yield break;
            }

            foreach (var gamePrefabWrapper in GetAllGamePrefabWrappers())
            {
                foreach (var existingGamePrefab in gamePrefabWrapper.GetGamePrefabs())
                {
                    if (existingGamePrefab == gamePrefab)
                    {
                        yield return gamePrefabWrapper;
                        break;
                    }
                }
            }
        }

        public static IEnumerable<GamePrefabWrapper> GetGamePrefabWrappers(Type gamePrefabType)
        {
            if (gamePrefabType == null)
            {
                yield break;
            }

            if (gamePrefabType.IsDerivedFrom<IGamePrefab>(true) == false)
            {
                yield break;
            }

            foreach (var gamePrefabWrapper in GetAllGamePrefabWrappers())
            {
                foreach (var existingGamePrefab in gamePrefabWrapper.GetGamePrefabs())
                {
                    if (existingGamePrefab.GetType().IsDerivedFrom(gamePrefabType, true))
                    {
                        yield return gamePrefabWrapper;
                        break;
                    }
                }
            }
        }
    }
}
#endif