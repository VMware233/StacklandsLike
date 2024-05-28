using VMFramework.Property;

namespace StackLandsLike.Cards
{
    public class FoodCard : Card, IFoodCard
    {
        protected FoodCardConfig foodCardConfig => (FoodCardConfig)gamePrefab;
        
        public BaseIntProperty nutrition;

        protected override void OnCreate()
        {
            base.OnCreate();

            nutrition = new(foodCardConfig.nutrition.GetValue());
        }

        int IFoodCard.nutrition => nutrition;
        
        public void ConsumeNutrition(int amount)
        {
            nutrition.value -= amount;
        }
    }
}