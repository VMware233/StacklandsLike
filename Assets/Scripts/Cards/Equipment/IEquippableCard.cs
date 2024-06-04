using System;
using System.Collections.Generic;

namespace StackLandsLike.Cards
{
    public interface IEquippableCard : ICard
    {
        public event Action<IEquippableCard> OnEquipmentChanged;
        
        public void Equip(IEquipmentCard card);

        public IEnumerable<IEquipmentCard> GetEquipments();
    }
}