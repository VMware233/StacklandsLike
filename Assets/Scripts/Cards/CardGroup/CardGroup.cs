using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace StackLandsLike.Cards
{
    public sealed class CardGroup : MonoBehaviour
    {
        private readonly List<ICard> _cards = new();
        
        public IReadOnlyList<ICard> cards => _cards;
        
        public int count => _cards.Count;

        public event Action<CardGroup, ICard> OnCardAdded; 
        public event Action<CardGroup, ICard> OnCardRemoved;
        public event Action<CardGroup> OnPositionChanged;

        public bool CanAddCard(ICard card)
        {
            if (card == null)
            {
                return false;
            }
            
            if (_cards.Contains(card))
            {
                Debug.LogWarning($"{card} already exists in Group: {name}");
                return false;
            }
            
            if (_cards.Count > 0)
            {
                foreach (var existedCard in _cards)
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

        public void AddCard(ICard card)
        {
            _cards.Add(card);
            
            card.SetGroup(this);

            if (_cards.Count == 1)
            {
                name = card.name;
            }
            
            OnCardAdded?.Invoke(this, card);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryAddCard(ICard card)
        {
            if (CanAddCard(card) == false)
            {
                return false;
            }
            
            if (card.group != null)
            {
                Debug.LogWarning($"{card} already belongs to Group: {card.group.name}");
                return false;
            }
            
            AddCard(card);
            
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveCard(ICard card)
        {
            if (_cards.Remove(card) == false)
            {
                Debug.LogWarning($"{card} does not exist in Group: {name}");
                return;
            }
            
            card.SetGroup(null);
            
            OnCardRemoved?.Invoke(this, card);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPosition(Vector2 position)
        {
            transform.position = position;
            OnPositionChanged?.Invoke(this);
        }
    }
}