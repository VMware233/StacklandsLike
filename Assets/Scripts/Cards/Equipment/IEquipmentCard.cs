namespace StackLandsLike.Cards
{
    public interface IEquipmentCard : ICard
    {
        public int attackBonus { get; }
        
        public int defenseBonus { get; }

        public void OnEquipTo(IEquippableCard equippableCard);
        
        public void OnUnequipFrom(IEquippableCard equippableCard);
    }
}