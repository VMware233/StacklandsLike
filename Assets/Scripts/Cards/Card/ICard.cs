using System;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Cards
{
    public interface ICard : IVisualGameItem, IContainerItem
    {
        public CardGroup group { get; }
        
        public GameObject model { get; }
        
        public Vector2 cardSize { get; }
        
        public event Action<ICard, CardGroup> OnGroupChangedEvent; 
        
        public bool CanStackWith(ICard card);

        public void SetGroup(CardGroup group);
    }
}