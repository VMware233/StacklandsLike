using System;
using System.Collections.Generic;
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
        
        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public IVectorChooserConfig<int> defaultDefense;
        
        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public IChooserConfig<List<CardGenerationConfig>> dropCardConfigs;

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            defaultMaxHealth.CheckSettings();
            defaultAttack.CheckSettings();
            defaultDefense.CheckSettings();
            
            dropCardConfigs.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            defaultMaxHealth.Init();
            defaultAttack.Init();
            defaultDefense.Init();
            
            dropCardConfigs.Init();
        }
    }
}