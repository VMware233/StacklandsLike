using System;
using Sirenix.OdinInspector;
using VMFramework.Configuration;

namespace StackLandsLike.Cards
{
    public partial class PersonCardConfig : CreatureCardConfig
    {
        public override Type gameItemType => typeof(PersonCard);

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public IVectorChooserConfig<int> nutritionRequired;

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            nutritionRequired.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            nutritionRequired.Init();
        }
    }
}