#if UNITY_EDITOR
using VMFramework.Core.Editor;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabWrapper : ILocalizedStringOwnerConfig
    {
        public void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            bool isDirty = false;
            
            foreach (var gamePrefab in GetGamePrefabs())
            {
                if (gamePrefab is ILocalizedStringOwnerConfig localizedStringOwnerConfig)
                {
                    localizedStringOwnerConfig.AutoConfigureLocalizedString(settings);
                    isDirty = true;
                }
            }

            if (isDirty)
            {
                if (settings.save)
                {
                    this.EnforceSave();
                }
                else
                {
                    this.SetEditorDirty();
                }
            }
        }

        public void SetKeyValueByDefault()
        {
            foreach (var gamePrefab in GetGamePrefabs())
            {
                if (gamePrefab is ILocalizedStringOwnerConfig localizedStringOwnerConfig)
                {
                    localizedStringOwnerConfig.SetKeyValueByDefault();
                }
            }
        }
    }
}
#endif