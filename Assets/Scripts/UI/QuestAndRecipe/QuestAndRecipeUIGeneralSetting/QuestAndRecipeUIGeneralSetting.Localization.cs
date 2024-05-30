#if UNITY_EDITOR
using VMFramework.Localization;

namespace StackLandsLike.UI
{
    public partial class QuestAndRecipeUIGeneralSetting
    {
        public override bool localizationEnabled => true;
        
        public override void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            base.AutoConfigureLocalizedString(settings);
            
            recipeCategoryConfigs.AutoConfigureLocalizedString(settings);
        }

        public override void SetKeyValueByDefault()
        {
            base.SetKeyValueByDefault();
            
            recipeCategoryConfigs.SetKeyValueByDefault();
        }
    }
}
#endif