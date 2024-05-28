namespace StackLandsLike.Cards
{
    public interface IFoodCard : ICard
    {
        public int nutrition { get; }

        public void ConsumeNutrition(int amount);
    }
}