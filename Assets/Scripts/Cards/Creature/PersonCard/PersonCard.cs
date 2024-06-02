namespace StackLandsLike.Cards
{
    public class PersonCard : CreatureCard, INutritionRequiredCard, IPersonCard
    {
        protected PersonCardConfig personCardConfig => (PersonCardConfig)gamePrefab;

        int INutritionRequiredCard.nutritionRequired => personCardConfig.nutritionRequired.GetValue() * count;
    }
}