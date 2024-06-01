namespace StackLandsLike.Cards
{
    public interface IProducerCardConfig : ICardConfig
    {
        public string lastProductionRecipeID { get; }
    }
}