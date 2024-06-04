namespace StackLandsLike.Cards
{
    public class EquipmentCard : Card, IEquipmentCard
    {
        protected EquipmentCardConfig equipmentCardConfig => (EquipmentCardConfig)gamePrefab;

        public int attackBonus => equipmentCardConfig.attackBonus;
        
        public int defenseBonus => equipmentCardConfig.defenseBonus;

        void IEquipmentCard.OnEquipTo(IEquippableCard equippableCard)
        {
            OnEquipTo(equippableCard);
        }

        void IEquipmentCard.OnUnequipFrom(IEquippableCard equippableCard)
        {
            OnUnequipFrom(equippableCard);
        }

        protected virtual void OnEquipTo(IEquippableCard equippableCard)
        {
            if (equippableCard is ICreatureCard creatureCard)
            {
                creatureCard.attack += attackBonus;
                creatureCard.defense += defenseBonus;
            }
        }

        protected virtual void OnUnequipFrom(IEquippableCard equippableCard)
        {
            if (equippableCard is ICreatureCard creatureCard)
            {
                creatureCard.attack -= attackBonus;
                creatureCard.defense -= defenseBonus;
            }
        }
    }
}