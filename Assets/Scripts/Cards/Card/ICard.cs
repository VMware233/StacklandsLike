using System;
using VMFramework.Containers;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Cards
{
    public interface ICard : IVisualGameItem, IContainerItem
    {
        public CardGroup group { get; }
        
        public event Action<ICard, CardGroup> OnGroupChangedEvent; 
        
        public bool CanStackWith(ICard card);

        public void SetGroup(CardGroup group);
    }
}