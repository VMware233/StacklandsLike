using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.Containers;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Cards
{
    public partial class CardRecipe : DescribedGamePrefab, ICardRecipe
    {
        protected override string idSuffix => "recipe";

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public bool autoCheck;

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public int priority;

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public int totalTicks;
        
        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public List<CardConsumptionConfig> consumptionConfigs = new();

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public IChooserConfig<List<CardGenerationConfig>> generationConfigs;

        public override void CheckSettings()
        {
            base.CheckSettings();

            consumptionConfigs.CheckSettings();
            
            foreach (var consumptionConfig in consumptionConfigs)
            {
                var gameItemType = IGameItem.GetGameItemType(consumptionConfig.itemID);

                if (gameItemType.IsDerivedFrom<ICraftableCard>(true) == false)
                {
                    Debug.LogWarning(
                        $"{this} has a consumption config with an invalid item type: {gameItemType}." +
                        $"It should be derived from {nameof(ICraftableCard)}.");
                }
            }
            
            generationConfigs.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            consumptionConfigs.Init();
            generationConfigs.Init();
        }

        public bool SatisfyConsumptionRequirements(IContainer container)
        {
            return container.ContainsEnoughItems(consumptionConfigs);
        }

        bool ICardRecipe.autoCheck => autoCheck;

        int ICardRecipe.priority => priority;

        int ICardRecipe.totalTicks => totalTicks;
        
        IEnumerable<CardConsumptionConfig> ICardRecipe.consumptionConfigs => consumptionConfigs;
        
        IEnumerable<CardGenerationConfig> ICardRecipe.generationConfigs => generationConfigs.GetValue();
    }
}