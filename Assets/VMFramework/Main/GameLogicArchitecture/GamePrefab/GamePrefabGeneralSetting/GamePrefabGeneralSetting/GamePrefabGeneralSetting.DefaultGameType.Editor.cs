#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.Core.Editor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabGeneralSetting
    {
        public void AddDefaultGameTypeToGamePrefabWrapper(GamePrefabWrapper wrapper)
        {
            if (defaultGameType.IsNullOrWhiteSpace())
            {
                return;
            }
            
            bool isDirty = false;
            foreach (var gamePrefab in wrapper.GetGamePrefabs())
            {
                if (gamePrefab is IGameTypedGamePrefab gameTypedGamePrefab)
                {
                    if (gameTypedGamePrefab.initialGameTypesID.Contains(defaultGameType) == false)
                    {
                        gameTypedGamePrefab.initialGameTypesID.Add(defaultGameType);
                        
                        isDirty = true;
                    }
                }
            }

            if (isDirty)
            {
                wrapper.EnforceSave();
            }
        }

        [Button, TabGroup(TAB_GROUP_NAME, GAME_TYPE_CATEGORY)]
        [EnableIf(nameof(gamePrefabGameTypeEnabled))]
        private void AddDefaultGameTypeToInitialGamePrefabWrappers()
        {
            foreach (var wrapper in initialGamePrefabWrappers)
            {
                AddDefaultGameTypeToGamePrefabWrapper(wrapper);
            }
        }
    }
}
#endif