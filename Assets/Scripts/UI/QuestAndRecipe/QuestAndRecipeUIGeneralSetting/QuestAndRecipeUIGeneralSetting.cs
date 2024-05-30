using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.UI
{
    public sealed partial class QuestAndRecipeUIGeneralSetting : GeneralSetting
    {
        public DictionaryConfigs<string, RecipeCategoryConfig> recipeCategoryConfigs = new();
        
        public override void CheckSettings()
        {
            base.CheckSettings();
            
            recipeCategoryConfigs.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            recipeCategoryConfigs.Init();
        }
    }
}