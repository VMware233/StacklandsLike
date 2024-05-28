using Sirenix.OdinInspector;
using StackLandsLike.GameCore;
using VMFramework.Core;
using VMFramework.Timers;

namespace StackLandsLike.Cards
{
    public class ProducerCard : Card, ICraftConsumableCard
    {
        protected ProducerCardConfig producerCardConfig => (ProducerCardConfig)gamePrefab;
        
        public override int maxStackCount => 1;

        [ShowInInspector]
        public int productionTimes { get; private set; }

        private int productionRecoveryTimer = 0;

        protected override void OnCreate()
        {
            base.OnCreate();

            productionTimes = producerCardConfig.productionTimes;
            
            LogicTickManager.OnPostTick += OnPostTick;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            LogicTickManager.OnPostTick -= OnPostTick;
        }

        private void OnPostTick()
        {
            if (productionTimes <= 0) return;

            if (productionTimes >= producerCardConfig.productionTimes)
            {
                return;
            }
            
            productionRecoveryTimer++;
            
            if (productionRecoveryTimer >= producerCardConfig.productionRecoveryTicks)
            {
                productionTimes += producerCardConfig.productionRecoveryAmount;
                productionTimes = productionTimes.ClampMax(producerCardConfig.productionTimes);
                productionRecoveryTimer = 0;
            }
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

            var generationConfigs = producerCardConfig.generationConfigs;

            if (producerCardConfig.hasLastGenerationConfig && productionTimes == 0)
            {
                generationConfigs = producerCardConfig.lastGenerationConfigs;
            }

            foreach (var config in generationConfigs.GetValue())
            {
                var card = config.GenerateItem();

                if (card == null)
                {
                    continue;
                }

                CardGroupManager.CreateCardGroup(card, group.GetPosition());
            }
            
            if (productionTimes <= 0)
            {
                count.value--;
            }
        }
    }
}