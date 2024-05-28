using Sirenix.OdinInspector;
using StackLandsLike.GameCore;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.Core;
using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;
using VMFramework.Timers;

namespace StackLandsLike.Cards
{
    public class ProducerCard : Card, ICraftableCard, IContainerItem
    {
        protected ProducerCardConfig producerCardConfig => (ProducerCardConfig)gamePrefab;
        
        public override int maxStackCount => 1;

        [ShowInInspector]
        public int productionTimes { get; private set; }

        private int productionRecoveryTimer = 0;

        protected override void OnCreate()
        {
            base.OnCreate();

            productionTimes = producerCardConfig.productionTimes;
            
            LogicTickManager.OnPostTick += OnPostTick;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            LogicTickManager.OnPostTick -= OnPostTick;
        }

        private void OnPostTick()
        {
            if (productionTimes <= 0) return;

            if (productionTimes >= producerCardConfig.productionTimes)
            {
                return;
            }
            
            productionRecoveryTimer++;
            
            if (productionRecoveryTimer >= producerCardConfig.productionRecoveryTicks)
            {
                productionTimes += producerCardConfig.productionRecoveryAmount;
                productionTimes = productionTimes.ClampMax(producerCardConfig.productionTimes);
                productionRecoveryTimer = 0;
            }
        }

        void IContainerItem.OnAddToContainer(IContainer container)
        {
            OnContainerItemAdded(container);
            container.ItemAddedEvent.AddCallback(OnContainerItemAdded, GameEventPriority.TINY);
        }

        void IContainerItem.OnRemoveFromContainer(IContainer container)
        {
            container.ItemAddedEvent.RemoveCallback(OnContainerItemAdded);
        }

        private void OnContainerItemAdded(ContainerItemAddedEvent e)
        {
            OnContainerItemAdded(e.container);
        }

        private void OnContainerItemAdded(IContainer container)
        {
            if (productionTimes <= 0) return;

            if (TryGetRecipe(out var recipe) == false)
            {
                return;
            }

            if (recipe.SatisfyConsumptionRequirements(container))
            {
                CardCraftManager.StartCraft(group, recipe);
            }
        }

        public void OnCraftStopped(ICardRecipe recipe)
        {
            if (productionTimes <= 0) return;

            if (TryGetRecipe(out var requiredRecipe) == false)
            {
                return;
            }

            if (recipe == requiredRecipe)
            {
                return;
            }

            if (requiredRecipe.SatisfyConsumptionRequirements(sourceContainer))
            {
                CardCraftManager.StopCraft(group);
                
                CardCraftManager.StartCraft(group, requiredRecipe);
            }
        }

        private bool TryGetRecipe(out ICardRecipe recipe)
        {
            var recipeID = producerCardConfig.productionRecipeID;

            if (producerCardConfig.hasLastGenerationConfig && productionTimes == 1)
            {
                recipeID = producerCardConfig.lastProductionRecipeID;
            }

            if (GamePrefabManager.TryGetGamePrefab<ICardRecipe>(recipeID, out recipe) == false)
            {
                Debug.LogError($"Could not find recipe with ID {recipeID} for producer card {name}"); 
                return false;
            }

            return true;
        }

        void ICraftableCard.CraftConsume(int countAmount, out int actualConsumedCount)
        {
            if (countAmount <= 0)
            {
                actualConsumedCount = 0;
                return;
            }

            productionTimes--;

            actualConsumedCount = 1;
            
            if (productionTimes <= 0)
            {
                count.value--;
            }
        }
    }
}