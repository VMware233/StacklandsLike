using System;

namespace StackLandsLike.Cards
{
    public class EquipmentCardConfig : CardConfig
    {
        public override Type gameItemType => typeof(EquipmentCard);
        
        public int attackBonus;
        
        public int defenseBonus;
    }
}