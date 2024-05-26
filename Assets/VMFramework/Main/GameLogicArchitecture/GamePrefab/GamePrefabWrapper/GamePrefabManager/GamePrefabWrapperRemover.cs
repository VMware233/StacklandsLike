#if UNITY_EDITOR
using System;
using System.Linq;
using VMFramework.Core.Editor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    public static class GamePrefabWrapperRemover
    {
        public static void RemoveGamePrefabWrapper(IGamePrefab gamePrefab)
        {
            if (gamePrefab == null)
            {
                return;
            }

            foreach (var wrapper in GamePrefabWrapperQuery.GetGamePrefabWrappers(gamePrefab)
                         .ToList())
            {
                if (GamePrefabGeneralSettingUtility.TryGetGamePrefabGeneralSetting(gamePrefab,
                        out var gamePrefabGeneralSetting))
                {
                    gamePrefabGeneralSetting.RemoveFromInitialGamePrefabWrappers(wrapper);
                }
                
                wrapper.DeleteAsset();
            }
        }

        public static void RemoveGamePrefabWrapperWhere<TGamePrefab>(Func<TGamePrefab, bool> predicate)
            where TGamePrefab : IGamePrefab
        {
            var gamePrefabGeneralSetting =
                GamePrefabGeneralSettingUtility.GetGamePrefabGeneralSetting(typeof(TGamePrefab));
            
            foreach (var room in GamePrefabManager.GetAllGamePrefabs<TGamePrefab>())
            {
                if (predicate(room))
                {
                    foreach (var wrapper in GamePrefabWrapperQuery.GetGamePrefabWrappers(room))
                    {
                        if (gamePrefabGeneralSetting != null)
                        {
                            gamePrefabGeneralSetting.RemoveFromInitialGamePrefabWrappers(wrapper);
                        }
                        
                        wrapper.DeleteAsset();
                    }
                }
            }
        }
    }
}
#endif