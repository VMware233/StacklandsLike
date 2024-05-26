#if UNITY_EDITOR
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameTypeGeneralSetting
    {
        private partial class LocalizedGameTypeInfo : ILocalizedStringOwnerConfig
        {
            public void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
            {
                name ??= new();

                name.AutoConfigNameByID(id, settings.defaultTableName);
            }

            public void SetKeyValueByDefault()
            {
                name.SetKeyValueByDefault();
            }
        }
    }
}
#endif