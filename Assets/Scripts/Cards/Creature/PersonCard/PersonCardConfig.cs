using System;

namespace StackLandsLike.Cards
{
    public class PersonCardConfig : CreatureCardConfig
    {
        public override Type gameItemType => typeof(PersonCard);
    }
}