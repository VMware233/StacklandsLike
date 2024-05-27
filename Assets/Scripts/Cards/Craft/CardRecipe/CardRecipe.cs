using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Containers;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Cards
{
    public partial class CardRecipe : DescribedGamePrefab, ICardRecipe
    {
        protected override string idSuffix => "recipe";

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public int totalTicks;
        
        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public List<CardConsumptionConfig> consumptionConfigs = new();

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public List<CardGenerationConfig> generationConfigs = new();

        public bool SatisfyConsumptionRequirements(IContainer container)
        {
            return container.ContainsEnoughItems(consumptionConfigs);
        }

        int ICardRecipe.totalTicks => totalTicks;
        
        IEnumerable<CardConsumptionConfig> ICardRecipe.consumptionConfigs => consumptionConfigs;
        
        IEnumerable<CardGenerationConfig> ICardRecipe.generationConfigs => generationConfigs;
    }
}