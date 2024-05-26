#if UNITY_EDITOR
using System;
using System.Linq;
using VMFramework.Procedure.Editor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    internal sealed class GamePrefabWrapperEditorInitializer : IEditorInitializer
    {
        public void OnInit(Action onDone)
        {
            GamePrefabWrapperCreator.OnGamePrefabWrapperCreated += gamePrefabWrapper =>
            {
                var gamePrefabs = gamePrefabWrapper.GetGamePrefabs().ToList();

                if (gamePrefabs.Count == 0 || gamePrefabs.All(gamePrefab => gamePrefab == null))
                {
                    return;
                }

                if (GamePrefabGeneralSettingUtility.TryGetGamePrefabGeneralSetting(gamePrefabs[0],
                        out var gamePrefabGeneralSetting))
                {
                    gamePrefabGeneralSetting.AddToInitialGamePrefabWrappers(gamePrefabWrapper);
                }
            };
            
            GamePrefabWrapperInitializeUtility.Refresh();
            GamePrefabWrapperInitializeUtility.CreateAutoRegisterGamePrefabs();
            
            onDone();
        }
    }
}
#endif