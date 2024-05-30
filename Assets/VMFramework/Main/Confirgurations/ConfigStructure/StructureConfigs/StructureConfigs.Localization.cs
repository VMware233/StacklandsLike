#if UNITY_EDITOR
using VMFramework.Localization;

namespace VMFramework.Configuration
{
    public partial class StructureConfigs<TConfig>
    {
        public void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            foreach (var config in configs)
            {
                if (config is ILocalizedStringOwnerConfig localizedConfig)
                {
                    localizedConfig.AutoConfigureLocalizedString(settings);
                }
            }
        }

        public void SetKeyValueByDefault()
        {
            foreach (var config in configs)
            {
                if (config is ILocalizedStringOwnerConfig localizedConfig)
                {
                    localizedConfig.SetKeyValueByDefault();
                }
            }
        }
    }
}
#endif