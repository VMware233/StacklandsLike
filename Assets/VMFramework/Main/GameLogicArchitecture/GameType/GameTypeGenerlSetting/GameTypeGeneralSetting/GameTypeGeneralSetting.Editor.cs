#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Localization;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameTypeGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            subrootGameTypeInfos ??= new();
        }

        private void OnSubrootGameTypeInfosChanged()
        {
            InitGameTypeInfo();
        }

        [Button, TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
        private void AddDefaultGameType(
            [GamePrefabGeneralSetting]
            GamePrefabGeneralSetting gamePrefabGeneralSetting)
        {
            var name = gamePrefabGeneralSetting.gameItemName;

            if (name.IsNullOrEmpty())
            {
                name = gamePrefabGeneralSetting.gamePrefabName;
            }

            if (name.IsNullOrEmpty())
            {
                Debug.LogError($"Could not determine a name for the game prefab general setting:" +
                               gamePrefabGeneralSetting.name);
                return;
            }

            name = name.ToSnakeCase();

            var subrootGameTypeID = name + "_type";
            subrootGameTypeID = subrootGameTypeID.ToSnakeCase();

            GameTypeInfo subrootGameTypeInfo = null;

            foreach (var info in subrootGameTypeInfos)
            {
                if (info.id == subrootGameTypeID)
                {
                    subrootGameTypeInfo = info;

                    break;
                }
            }

            if (subrootGameTypeInfo == null)
            {
                subrootGameTypeInfo = new GameTypeInfo()
                {
                    id = subrootGameTypeID,
                };
                
                subrootGameTypeInfos.Add(subrootGameTypeInfo);
            }
            
            var defaultGameTypeID = "default_" + subrootGameTypeID;

            GameTypeInfo defaultGameTypeInfo = null;

            foreach (var info in subrootGameTypeInfo.subtypes)
            {
                if (info.id == defaultGameTypeID)
                {
                    defaultGameTypeInfo = info;

                    break;
                }
            }

            if (defaultGameTypeInfo == null)
            {
                defaultGameTypeInfo = new LocalizedGameTypeInfo()
                {
                    id = defaultGameTypeID,
                    name = new LocalizedStringReference()
                    {
                        defaultValue = defaultGameTypeID.ToPascalCase(" "),
                        key = defaultGameTypeID.ToPascalCase() + "Name",
                        tableName = defaultLocalizationTableName
                    }
                };
                
                subrootGameTypeInfo.subtypes.Add(defaultGameTypeInfo);
            }
            
            OnSubrootGameTypeInfosChanged();
        }
    }
}
#endif