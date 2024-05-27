using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using StackLandsLike.GameCore;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.Core;
using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;
using VMFramework.Timers;

namespace StackLandsLike.Cards
{
    [ManagerCreationProvider(nameof(GameManagerType.Card))]
    public sealed class CardCraftManager : ManagerBehaviour<CardCraftManager>
    {
        [ShowInInspector]
        private static readonly Dictionary<CardGroup, CardCraftInfo> craftingRecipes = new();
        
        private static readonly List<(CardGroup cardGroup, CardCraftInfo info)> craftDone = new();

        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();
            
            LogicTickManager.OnTick += OnTick;
            CardGroupManager.OnCardGroupCreated += OnCardGroupCreated;
            CardGroupManager.OnCardGroupDestroyed += OnCardGroupDestroyed;
        }

        private void OnCardGroupCreated(CardGroup cardGroup)
        {
            cardGroup.cardContainer.ItemAddedEvent.AddCallback(OnCardAdded, GameEventPriority.TINY);
            cardGroup.cardContainer.ItemRemovedEvent.AddCallback(OnCardRemoved, GameEventPriority.SUPER);
            cardGroup.cardContainer.OnItemCountChangedEvent += OnCardCountChanged;
        }

        private void OnCardGroupDestroyed(CardGroup cardGroup)
        {
            cardGroup.cardContainer.ItemAddedEvent.RemoveCallback(OnCardAdded);
            cardGroup.cardContainer.ItemRemovedEvent.RemoveCallback(OnCardRemoved);
            cardGroup.cardContainer.OnItemCountChangedEvent -= OnCardCountChanged;
        }

        private void OnCardCountChanged(IContainer container, int index, IContainerItem item, int oldCount,
            int newCount)
        {
            var cardGroup = (CardGroup)container.owner;

            if (newCount <= oldCount)
            {
                CheckGroup(cardGroup);
                return;
            }
            
            CheckSatisfied(container);
        }

        private void OnCardAdded(ContainerItemAddedEvent e)
        {
            CheckSatisfied(e.container);
        }
        
        private void OnCardRemoved(ContainerItemRemovedEvent e)
        {
            var cardGroup = (CardGroup)e.container.owner;

            CheckGroup(cardGroup);
        }

        private static void CheckGroup(CardGroup cardGroup)
        {
            if (craftingRecipes.TryGetValue(cardGroup, out var info) == false)
            {
                return;
            }

            if (info.recipe.SatisfyConsumptionRequirements(cardGroup.cardContainer) == false)
            {
                StopCraft(cardGroup);
            }
        }

        private static void CheckSatisfied(IContainer container)
        {
            foreach (var recipe in GamePrefabManager.GetAllGamePrefabs<ICardRecipe>())
            {
                if (recipe.SatisfyConsumptionRequirements(container))
                {
                    StartCraft((CardGroup)container.owner, recipe);
                    break;
                }
            }
        }

        private static void OnTick()
        {
            foreach (var (cardGroup, info) in craftingRecipes)
            {
                info.tick++;

                if (info.tick >= info.recipe.totalTicks)
                {
                    craftDone.Add((cardGroup, info));
                }
            }

            if (craftDone.Count <= 0)
            {
                return;
            }

            foreach (var (cardGroup, info) in craftDone)
            {
                if (info.recipe.SatisfyConsumptionRequirements(cardGroup.cardContainer))
                {
                    foreach (var consumptionConfig in info.recipe.consumptionConfigs)
                    {
                        if (consumptionConfig.isConsumable == false)
                        {
                            continue;
                        }

                        int consumedCount = consumptionConfig.count;
                        foreach (var item in cardGroup.cardContainer.GetItems(consumptionConfig.itemID))
                        {
                            var reducedCount = item.count.Min(consumedCount);
                            consumedCount -= reducedCount;
                            item.count -= reducedCount;
                        }

                        if (consumedCount > 0)
                        {
                            Debug.LogError(
                                $"Failed to consume {consumptionConfig.itemID}:{consumptionConfig.count} " +
                                $"from {cardGroup.name}, left with {consumedCount}");
                        }
                    }
                        
                    foreach (var generationConfig in info.recipe.generationConfigs)
                    {
                        var card = generationConfig.GenerateItem();
                        CardGroupManager.CreateCardGroup(card, cardGroup.GetPosition());
                    }
                }
                    
                StopCraft(cardGroup);
            }
            
            craftDone.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StartCraft(CardGroup cardGroup, ICardRecipe recipe)
        {
            if (craftingRecipes.ContainsKey(cardGroup))
            {
                return;
            }
            
            var newCraftInfo = new CardCraftInfo(recipe);
            craftingRecipes.Add(cardGroup, newCraftInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StopCraft(CardGroup cardGroup)
        {
            craftingRecipes.Remove(cardGroup);
        }
        
        
    }
}