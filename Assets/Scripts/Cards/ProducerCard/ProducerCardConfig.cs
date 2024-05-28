using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Configuration;

namespace StackLandsLike.Cards
{
    public partial class ProducerCardConfig : CardConfig
    {
        public override Type gameItemType => typeof(ProducerCard);

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public int productionTimes;

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public int productionRecoveryTicks = 600;

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public int productionRecoveryAmount = 1;
        
        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public IChooserConfig<List<CardGenerationConfig>> generationConfigs;

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public bool hasLastGenerationConfig;

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        [ShowIf(nameof(hasLastGenerationConfig))]
        public IChooserConfig<List<CardGenerationConfig>> lastGenerationConfigs;

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            generationConfigs.CheckSettings();
            lastGenerationConfigs.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            generationConfigs.Init();
            lastGenerationConfigs.Init();
        }
    }
}