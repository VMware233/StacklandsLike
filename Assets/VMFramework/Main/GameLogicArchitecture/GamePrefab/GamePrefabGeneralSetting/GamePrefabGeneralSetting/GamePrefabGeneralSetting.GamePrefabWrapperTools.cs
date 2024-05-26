#if UNITY_EDITOR
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.GameLogicArchitecture.Editor;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabGeneralSetting
    {
        #region Collect All Game Prefab Wrappers

        [Button(ButtonSizes.Medium), TabGroup(TAB_GROUP_NAME, INITIAL_GAME_PREFABS_CATEGORY)]
        private void CollectAllGamePrefabWrappers()
        {
            foreach (var wrapper in GamePrefabWrapperQuery.GetGamePrefabWrappers(baseGamePrefabType))
            {
                AddToInitialGamePrefabWrappers(wrapper);
            }
            
            this.EnforceSave();
        }

        #endregion
        
        #region Game Prefab Create

        [Button(ButtonSizes.Medium, ButtonStyle.FoldoutButton), TabGroup(TAB_GROUP_NAME, INITIAL_GAME_PREFABS_CATEGORY)]
        private void CreateGamePrefab(
            [IsNotNullOrEmpty, IsUncreatedGamePrefabID]
            string gamePrefabID)
        {
            if (gamePrefabID.IsNullOrEmptyAfterTrim())
            {
                Debug.LogError($"{nameof(gamePrefabID)} cannot be null or empty.");
                return;
            }

            var gamePrefabTypes = baseGamePrefabType.GetDerivedClasses(true, false)
                .Where(type => type.IsAbstract == false && type.IsInterface == false);

            new TypeSelector(gamePrefabTypes,
                selectedType =>
                {
                    var wrapper =
                        GamePrefabWrapperCreator.CreateGamePrefabWrapper(gamePrefabID, selectedType);

                    AddDefaultGameTypeToGamePrefabWrapper(wrapper);
                    
                    GUIHelper.OpenInspectorWindow(wrapper);
                }).ShowInPopup();
        }

        #endregion
    }
}
#endif