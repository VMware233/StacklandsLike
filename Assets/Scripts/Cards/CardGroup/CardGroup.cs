using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using StackLandsLike.Containers;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.Core;
using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Cards
{
    [DisallowMultipleComponent]
    public sealed class CardGroup : MonoBehaviour, IContainerOwner
    {
        [ShowInInspector]
        public CardGroupContainer cardContainer { get; private set; }
        
        public IEnumerable<ICard> cards => cardContainer.GetAllValidItems<ICard>();
        
        public int count => cardContainer.validItemsSize;
        
        public event Action<CardGroup> OnPositionChanged;
        public event Action OnInitialized;

        public void Init()
        {
            cardContainer = IGameItem.Create<CardGroupContainer>(CardGroupContainerPreset.ID);
            cardContainer.SetOwner(this);
            cardContainer.ItemAddedEvent.AddCallback(OnCardAdded, GameEventPriority.SUPER);
            cardContainer.ItemRemovedEvent.AddCallback(OnCardRemoved, GameEventPriority.SUPER);
            OnInitialized?.Invoke();
        }
        
        private void OnCardAdded(ContainerItemAddedEvent e)
        {
            var card = (ICard)e.item;

            name = e.container.GetAllValidItems().Select(item => item.name).Join(",");
            
            card.SetGroup(this);
        }

        private void OnCardRemoved(ContainerItemRemovedEvent e)
        {
            var card = (ICard)e.item;

            if (card.group == this)
            {
                card.SetGroup(null);
            }

            if (e.container.validItemsSize == 0)
            {
                name = "Empty Card Group";
                CardGroupManager.DestroyCardGroup(this);
            }
            else
            {
                name = e.container.GetAllValidItems().Select(item => item.name).Join(",");
            }
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