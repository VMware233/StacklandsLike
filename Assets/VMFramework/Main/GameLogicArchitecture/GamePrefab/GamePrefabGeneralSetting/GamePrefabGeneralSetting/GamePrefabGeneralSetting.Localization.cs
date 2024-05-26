#if UNITY_EDITOR
#if PARREL_SYNC
using ParrelSync;
#endif
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabGeneralSetting
    {
        public override bool localizationEnabled =>
            baseGamePrefabType.IsDerivedFrom<ILocalizedGamePrefab>(false);

        public override void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            base.AutoConfigureLocalizedString(settings);

#if PARREL_SYNC
            if (ClonesManager.IsClone())
            {
                return;
            }
#endif

            foreach (var gamePrefabWrapper in initialGamePrefabWrappers)
            {
                if (gamePrefabWrapper == null)
                {
                    continue;
                }
                
                gamePrefabWrapper.AutoConfigureLocalizedString(new()
                {
                    defaultTableName = settings.defaultTableName,
                    save = false
                });
            }

            if (settings.save)
            {
                this.EnforceSave();
            }
            else
            {
                this.SetEditorDirty();
            }
        }

        public override void SetKeyValueByDefault()
        {
            base.SetKeyValueByDefault();
            
            foreach (var gamePrefabWrapper in initialGamePrefabWrappers)
            {
                gamePrefabWrapper.SetKeyValueByDefault();
            }
        }
    }
}
#endif