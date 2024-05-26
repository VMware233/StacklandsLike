#if UNITY_EDITOR
using VMFramework.Configuration;
using VMFramework.Localization;

namespace VMFramework.GameEvents
{
    public partial class GameEventGeneralSetting
    {
        public override void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            base.AutoConfigureLocalizedString(settings);

            foreach (var config in keyCodeTranslations.GetAllConfigs())
            {
                config.AutoConfigureLocalizedString(settings);
            }
        }

        public override void SetKeyValueByDefault()
        {
            base.SetKeyValueByDefault();
            
            foreach (var config in keyCodeTranslations.GetAllConfigs())
            {
                config.SetKeyValueByDefault();
            }
        }
    }
}
#endif