using System;

namespace StackLandsLike.Cards
{
    public class ProductionToolCardConfig : EquipmentCardConfig
    {
        public override Type gameItemType => typeof(ProductionToolCard);
    }
}