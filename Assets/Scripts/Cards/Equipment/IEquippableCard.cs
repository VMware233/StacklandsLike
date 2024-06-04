namespace StackLandsLike.Cards
{
    public interface IEquippableCard : ICard
    {
        public void Equip(IEquipmentCard card);
    }
}