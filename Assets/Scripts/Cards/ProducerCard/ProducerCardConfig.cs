using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.OdinExtensions;

namespace StackLandsLike.Cards
{
    public partial class ProducerCardConfig : CardConfig, IProducerCardConfig
    {
        public override Type gameItemType => typeof(ProducerCard);

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public int productionTimes;

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public int productionRecoveryTicks = 600;

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public int productionRecoveryAmount = 1;

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        [GamePrefabID(typeof(ICardRecipe))]
        public string productionRecipeID;

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public bool hasLastGenerationConfig;

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        [ShowIf(nameof(hasLastGenerationConfig))]
        [GamePrefabID(typeof(ICardRecipe))]
        public string lastProductionRecipeID;

        string IProducerCardConfig.lastProductionRecipeID =>
            hasLastGenerationConfig ? lastProductionRecipeID : productionRecipeID;
    }
}