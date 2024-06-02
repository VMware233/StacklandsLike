using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using StackLandsLike.Cards;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace StackLandsLike.GameCore
{
    [ManagerCreationProvider(nameof(GameManagerType.GameCore))]
    public sealed class Scoreboard : ManagerBehaviour<Scoreboard>, IManagerBehaviour
    {
        [ShowInInspector]
        private static readonly HashSet<string> treeChoppingRecipesID = new();
        
        [ShowInInspector]
        public static int treeChoppingCount { get; private set; }
        
        [ShowInInspector]
        public static int personCount { get; private set; }
        
        [ShowInInspector]
        public static int nutritionCount { get; private set; }
        
        [ShowInInspector]
        public static int nutritionRequiredCount { get; private set; }
        
        public static event Action OnScoreboardUpdated;
        
        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();
            
            CardCraftManager.OnRecipeCompleted += OnRecipeCompleted;
            CardGroupManager.OnCardGroupCreated += OnCardGroupCreated;
            CardGroupManager.OnCardGroupDestroyed += OnCardGroupDestroyed;
            GameTimeManager.OnDayChanged += OnDayChanged;
        }

        void IInitializer.OnInitComplete(Action onDone)
        {
            foreach (var treeID in GameSetting.scoreboardGeneralSetting.treesID)
            {
                if (GamePrefabManager.TryGetActiveGamePrefab(treeID,
                        out IProducerCardConfig treeConfig) == false)
                {
                    Debug.LogError($"Could not find tree with ID {treeID} or it is not active or not a producer card.");
                    continue;
                }
                
                treeChoppingRecipesID.Add(treeConfig.lastProductionRecipeID);
            }
            
            onDone();
        }
        
        private static void OnDayChanged(int newDay)
        {
            RefreshScoreboard();
        }
        
        private static void OnCardGroupCreated(CardGroup group)
        {
            RefreshScoreboard();
        }

        private static void OnCardGroupDestroyed(CardGroup group)
        {
            RefreshScoreboard();
        }

        private static void RefreshScoreboard()
        {
            personCount = 0;
            nutritionCount = 0;
            nutritionRequiredCount = 0;
            
            foreach (var cardGroup in CardGroupManager.GetActiveCardGroups())
            {
                foreach (var card in cardGroup.cards)
                {
                    if (card is IPersonCard personCard)
                    {
                        personCount += personCard.count;
                    }

                    if (card is IFoodCard foodCard)
                    {
                        nutritionCount += foodCard.nutrition;
                    }

                    if (card is INutritionRequiredCard nutritionRequiredCard)
                    {
                        nutritionRequiredCount += nutritionRequiredCard.nutritionRequired;
                    }
                }
            }
            
            OnScoreboardUpdated?.Invoke();
        }

        private static void OnRecipeCompleted(CardGroup group, ICardRecipe recipe)
        {
            if (treeChoppingRecipesID.Contains(recipe.id))
            {
                treeChoppingCount++;
            }
        }

        public static void ResetScoreboard()
        {
            treeChoppingCount = 0;
            personCount = 0;
            nutritionCount = 0;
            nutritionRequiredCount = 0;
            
            OnScoreboardUpdated?.Invoke();
        }
    }
}