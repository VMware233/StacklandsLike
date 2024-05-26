#if UNITY_EDITOR
using VMFramework.Core;
using VMFramework.Core.Editor;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameTypeGeneralSetting
    {
        public override bool localizationEnabled => true;
        
        public override void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            base.AutoConfigureLocalizedString(settings);

            subrootGameTypeInfos ??= new();
            
            foreach (var gameTypeInfo in subrootGameTypeInfos.PreorderTraverse(true))
            {
                if (gameTypeInfo is ILocalizedStringOwnerConfig localizedStringOwnerConfig)
                {
                    localizedStringOwnerConfig.AutoConfigureLocalizedString(settings);
                }
            }
            
            this.EnforceSave();
        }

        public override void SetKeyValueByDefault()
        {
            base.SetKeyValueByDefault();
            
            subrootGameTypeInfos ??= new();
            
            foreach (var gameTypeInfo in subrootGameTypeInfos.PreorderTraverse(true))
            {
                if (gameTypeInfo is ILocalizedStringOwnerConfig localizedStringOwnerConfig)
                {
                    localizedStringOwnerConfig.SetKeyValueByDefault();
                }
            }
        }
    }
}
#endif