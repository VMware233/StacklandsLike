#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.ResourcesManagement
{
    public partial class SpriteGeneralSetting
    {
        [Button, TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
        private void BackupAll()
        {
            foreach (var prefab in GamePrefabManager.GetAllGamePrefabs<SpritePreset>())
            {
                prefab.GenerateBackup();
            }
        }

        [Button, TabGroup(TAB_GROUP_NAME, DEBUGGING_CATEGORY)]
        private void RestoreAllFromBackup()
        {
            foreach (var prefab in GamePrefabManager.GetAllGamePrefabs<SpritePreset>())
            {
                if (prefab.sprite == null)
                {
                    prefab.RestoreFromBackup();
                }
            }
        }
    }
}
#endif