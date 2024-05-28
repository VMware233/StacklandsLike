namespace StackLandsLike.Cards
{
    public interface ICraftableCard : ICard
    {
        public void CraftConsume(int countAmount, out int actualConsumedCount);

        public void OnCraftStopped(ICardRecipe recipe)
        {
            
        }
    }
}