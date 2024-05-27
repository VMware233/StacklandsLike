using System;
using StackLandsLike.Cards;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Containers
{
    public sealed class CardGroupContainer : ListContainer
    {
        public bool CanAddCard(ICard card)
        {
            if (card == null)
            {
                return false;
            }

            if (this.ContainsItem(card))
            {
                Debug.LogWarning($"{card} already exists in Group: {name}");
                return false;
            }
            
            if (size > 0)
            {
                foreach (var existedCard in this.GetAllItems<ICard>())
                {
                    if (existedCard.CanStackWith(card) == false)
                    {
                        return false;
                    }

                    if (card.CanStackWith(existedCard) == false)
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }
        
        public override bool TryAddItem(IContainerItem item)
        {
            if (item is not ICard card)
            {
                return false;
            }
            
            if (CanAddCard(card) == false)
            {
                return false;
            }

            return base.TryAddItem(item);
        }
    }
}