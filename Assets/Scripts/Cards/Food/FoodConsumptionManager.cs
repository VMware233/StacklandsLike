using System.Collections.Generic;
using StackLandsLike.GameCore;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.Core;
using VMFramework.Core.Linq;
using VMFramework.Procedure;

namespace StackLandsLike.Cards
{
    [ManagerCreationProvider(nameof(GameManagerType.Card))]
    public sealed class FoodConsumptionManager : ManagerBehaviour<FoodConsumptionManager>
    {
        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();
            
            GameTimeManager.OnDayChanged += OnDayChanged;
        }

        private void OnDayChanged(int day)
        {
            int nutritionRequired = 0;
            
            foreach (var cardGroup in CardGroupManager.GetActiveCardGroups())
            {
                foreach (var item in cardGroup.cardContainer.GetAllValidItems())
                {
                    if (item is INutritionRequiredCard nutritionRequiredCard)
                    {
                        nutritionRequired += nutritionRequiredCard.nutritionRequired;
                    }
                }
            }

            if (nutritionRequired <= 0)
            {
                return;
            }

            List<IFoodCard> foodCards = new List<IFoodCard>();
            foreach (var cardGroup in CardGroupManager.GetActiveCardGroups())
            {
                foreach (var item in cardGroup.cardContainer.GetAllValidItems())
                {
                    if (item is IFoodCard foodCard)
                    {
                        foodCards.Add(foodCard);
                    }
                }
            }
            
            foodCards.Sort(food => food.nutrition);

            foreach (var foodCard in foodCards)
            {
                if (foodCard.nutrition <= 0)
                {
                    continue;
                }
                
                foodCard.ConsumeNutrition(nutritionRequired, out var actualConsumedAmount);
                nutritionRequired -= actualConsumedAmount;

                if (nutritionRequired <= 0)
                {
                    return;
                }
            }

            Debug.LogWarning($"You are out of nutrition!");
            
            GameStateManager.EnterSettlement(false);
        }
    }
}