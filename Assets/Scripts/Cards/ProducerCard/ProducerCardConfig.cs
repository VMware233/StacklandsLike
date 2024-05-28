using System;
using Sirenix.OdinInspector;

namespace StackLandsLike.Cards
{
    public class ProducerCardConfig : CardConfig
    {
        public override Type gameItemType => typeof(ProducerCard);

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public int productionTimes;
    }
}