using Sirenix.OdinInspector;

namespace StackLandsLike.Cards
{
    public class ProducerCard : Card, ICraftConsumableCard
    {
        protected ProducerCardConfig producerCardConfig => (ProducerCardConfig)gamePrefab;
        
        public override int maxStackCount => 1;

        [ShowInInspector]
        public int productionTimes { get; private set; }

        protected override void OnCreate()
        {
            base.OnCreate();

            productionTimes = producerCardConfig.productionTimes;
        }

        void ICraftConsumableCard.CraftConsume(int countAmount, out int actualConsumedCount)
        {
            if (countAmount <= 0)
            {
                actualConsumedCount = 0;
                return;
            }

            productionTimes--;

            actualConsumedCount = 1;

            if (productionTimes <= 0)
            {
                count.value--;
            }
        }
    }
}