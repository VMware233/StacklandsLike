using System;
using Sirenix.OdinInspector;
using VMFramework.Configuration;

namespace StackLandsLike.Cards
{
    public partial class CreatureCardConfig : CardConfig, ICreatureCardConfig
    {
        public override Type gameItemType => typeof(CreatureCard);

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public IVectorChooserConfig<int> defaultMaxHealth;
        
        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public IVectorChooserConfig<int> defaultAttack;

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            defaultMaxHealth.CheckSettings();
            defaultAttack.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            defaultMaxHealth.Init();
            defaultAttack.Init();
        }
    }
}