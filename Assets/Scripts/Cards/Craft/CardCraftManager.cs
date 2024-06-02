using System;
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

        public static event Action<CardGroup, ICardRecipe> OnRecipeCompleted; 

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
            ICardRecipe currentRecipe = null;
            
            foreach (var recipe in GamePrefabManager.GetAllGamePrefabs<ICardRecipe>())
            {
                if (recipe.autoCheck == false)
                {
                    continue;
                }
                
                if (recipe.SatisfyConsumptionRequirements(container) == false)
                {
                    continue;
                }
                
                if (currentRecipe == null)
                {
                    currentRecipe = recipe;
                }
                else if (recipe.priority > currentRecipe.priority)
                {
                    currentRecipe = recipe;
                }
            }

            if (currentRecipe != null)
            {
                StartCraft((CardGroup)container.owner, currentRecipe);
            }
        }

        private static void OnTick()
        {
            foreach (var (cardGroup, info) in craftingRecipes)
            {
                info.tick++;
                EventManager.TriggerProgressBarTick(cardGroup, info);//这里换成info传递当前的tick,之后可以使用GetCraftingTicks函数，这样可以加速

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
                            var consumableCard = (ICraftableCard)item;
                            consumableCard.CraftConsume(consumedCount, out var actualConsumedCount);
                            consumedCount -= actualConsumedCount;
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

                        if (card == null)
                        {
                            continue;
                        }
                        
                        CardGroupManager.CreateCardGroup(card, cardGroup.GetPosition());
                    }
                }
                    
                StopCraft(cardGroup);
                
                OnRecipeCompleted?.Invoke(cardGroup, info.recipe);
                
                if (info.recipe.SatisfyConsumptionRequirements(cardGroup.cardContainer))
                {
                    StartCraft(cardGroup, info.recipe);
                }

                foreach (var craftableCard in cardGroup.cardContainer.GetAllItems<ICraftableCard>())
                {
                    craftableCard.OnCraftStopped(info.recipe);
                }
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

            if (recipe.SatisfyConsumptionRequirements(cardGroup.cardContainer) == false)
            {
                Debug.LogError($"Failed to start crafting {recipe.name} for {cardGroup.name}." + 
                               $"It does not satisfy the consumption requirements");
                return;
            }
            
            var newCraftInfo = new CardCraftInfo(recipe);
            craftingRecipes.Add(cardGroup, newCraftInfo);
            EventManager.TriggerCardCompositionStarted(cardGroup, recipe.totalTicks);

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StopCraft(CardGroup cardGroup)
        {
            if (craftingRecipes.Remove(cardGroup))
            {
                EventManager.TriggerStopComposition(cardGroup);//摧毁进度条
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCrafting(CardGroup cardGroup)
        {
            return craftingRecipes.ContainsKey(cardGroup);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCraftingRecipe(CardGroup cardGroup, out ICardRecipe recipe)
        {
            if (craftingRecipes.TryGetValue(cardGroup, out var info))
            {
                recipe = info.recipe;
                return true;
            }

            recipe = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetCraftingTicks(CardGroup cardGroup)
        {
            if (craftingRecipes.TryGetValue(cardGroup, out var info))
            {
                return info.tick;
            }

            Debug.LogWarning($"Failed to get crafting ticks for {cardGroup.name}, it is not crafting");
            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SpeedUpCrafting(CardGroup cardGroup, int ticks)
        {
            if (craftingRecipes.TryGetValue(cardGroup, out var info) == false)
            {
                Debug.LogWarning($"Failed to speed up crafting for {cardGroup.name}, it is not crafting");
                return;
            }
            
            info.tick += ticks;
        }
    }
}