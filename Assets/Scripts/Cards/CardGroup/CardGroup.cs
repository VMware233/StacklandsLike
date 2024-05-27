using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using StackLandsLike.Containers;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Cards
{
    public sealed class CardGroup : MonoBehaviour, IContainerOwner
    {
        [ShowInInspector]
        public CardGroupContainer cardContainer { get; private set; }
        
        public IEnumerable<ICard> cards => cardContainer.GetAllValidItems<ICard>();
        
        public int count => cardContainer.size;
        
        public event Action<CardGroup> OnPositionChanged;

        public void Init()
        {
            cardContainer = IGameItem.Create<CardGroupContainer>(CardGroupContainerPreset.ID);
            cardContainer.SetOwner(this);
            cardContainer.OnItemAddedEvent += OnCardAdded;
            cardContainer.OnItemRemovedEvent += OnCardRemoved;
        }

        private void OnCardRemoved(IContainer container, int index, IContainerItem item)
        {
            if (item is not ICard card)
            {
                return;
            }

            if (card.group == this)
            {
                card.SetGroup(null);
            }

            if (cardContainer.validItemsSize == 0)
            {
                name = "Empty Card Group";
            }
        }

        private void OnCardAdded(IContainer container, int index, IContainerItem item)
        {
            if (item is not ICard card)
            {
                return;
            }

            if (container.validItemsSize == 1)
            {
                name = card.name;
            }
            
            card.SetGroup(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPosition(Vector2 position)
        {
            transform.position = position;
            OnPositionChanged?.Invoke(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2 GetPosition()
        {
            return transform.position.XY();
        }

        public IEnumerable<IContainer> GetContainers()
        {
            yield return cardContainer;
        }
    }
}