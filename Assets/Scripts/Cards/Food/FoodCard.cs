using VMFramework.Core.Generic;
using VMFramework.Property;

namespace StackLandsLike.Cards
{
    public class FoodCard : Card, IFoodCard
    {
        protected FoodCardConfig foodCardConfig => (FoodCardConfig)gamePrefab;

        public int nutrition => foodCardConfig.nutrition * count;
        
        public void ConsumeNutrition(int nutritionAmount, out int actualConsumedAmount)
        {
            var countRequired = (nutritionAmount / foodCardConfig.nutrition).Ceiling();
            countRequired = countRequired.ClampMax(count);
            
            count.value -= countRequired;
            actualConsumedAmount = countRequired * foodCardConfig.nutrition;
        }
    }
}