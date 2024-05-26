#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public static class GamePrefabGeneralSettingUtility
    {
        public static GamePrefabGeneralSetting GetGamePrefabGeneralSetting(Type gamePrefabType)
        {
            foreach (var generalSetting in GameCoreSetting.GetAllGeneralSettings())
            {
                if (generalSetting is not GamePrefabGeneralSetting gamePrefabSetting)
                {
                    continue;
                }

                if (gamePrefabType.IsDerivedFrom(gamePrefabSetting.baseGamePrefabType, true))
                {
                    return gamePrefabSetting;
                }
            }
            
            return null;
        }

        public static bool TryGetGamePrefabGeneralSetting(Type gamePrefabType,
            out GamePrefabGeneralSetting gamePrefabSetting)
        {
            gamePrefabSetting = GetGamePrefabGeneralSetting(gamePrefabType);
            return gamePrefabSetting != null;
        }

        public static GamePrefabGeneralSetting GetGamePrefabGeneralSetting(IGamePrefab gamePrefab)
        {
            if (gamePrefab == null)
            {
                return null;
            }
            
            return GetGamePrefabGeneralSetting(gamePrefab.GetType());
        }
        
        public static bool TryGetGamePrefabGeneralSetting(IGamePrefab gamePrefab,
            out GamePrefabGeneralSetting gamePrefabSetting)
        {
            gamePrefabSetting = GetGamePrefabGeneralSetting(gamePrefab);
            return gamePrefabSetting != null;
        }

        public static IEnumerable<ValueDropdownItem> GetGamePrefabGeneralSettingNameList()
        {
            foreach (var generalSetting in GameCoreSetting.GetAllGeneralSettings())
            {
                if (generalSetting is not GamePrefabGeneralSetting gamePrefabSetting)
                {
                    continue;
                }

                yield return new ValueDropdownItem(gamePrefabSetting.gamePrefabName, gamePrefabSetting);
            }
        }
    }
}
#endif