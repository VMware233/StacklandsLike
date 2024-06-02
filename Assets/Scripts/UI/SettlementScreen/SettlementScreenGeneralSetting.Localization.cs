#if UNITY_EDITOR
using VMFramework.Localization;

namespace StackLandsLike.UI
{
    public partial class SettlementScreenGeneralSetting
    {
        public override bool localizationEnabled => true;

        public override void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            base.AutoConfigureLocalizedString(settings);

            victoryTitle.AutoConfigByVariableName(nameof(victoryTitle), settings.defaultTableName);
            defeatTitle.AutoConfigByVariableName(nameof(defeatTitle), settings.defaultTableName);
        }
    }
}
#endif