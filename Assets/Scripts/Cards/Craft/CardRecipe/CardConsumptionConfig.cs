using VMFramework.Configuration;

namespace StackLandsLike.Cards
{
    public class CardConsumptionConfig : DefaultContainerItemConsumptionConfig<ICard, ICardConfig>
    {
        public bool isConsumable = true;
    }
}