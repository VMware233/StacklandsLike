using System;

namespace StackLandsLike.Cards
{
    public class MonsterCardConfig : CreatureCardConfig
    {
        public override Type gameItemType => typeof(MonsterCard);
    }
}