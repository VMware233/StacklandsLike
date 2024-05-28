namespace StackLandsLike.Cards
{
    public interface ICraftConsumableCard
    {
        public void CraftConsume(int countAmount, out int actualConsumedCount);
    }
}