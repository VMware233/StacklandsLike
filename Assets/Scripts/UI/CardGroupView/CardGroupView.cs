using System;
using System.Linq;
using Sirenix.OdinInspector;
using StackLandsLike.Cards;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.Core;
using VMFramework.Core.Linq;
using VMFramework.GameEvents;

namespace StackLandsLike.UI
{
    [DisallowMultipleComponent]
    public sealed class CardGroupView : MonoBehaviour
    {
        [ShowInInspector]
        public CardGroup cardGroup;

        private void Start()
        {
            cardGroup = GetComponentInParent<CardGroup>();
            
            cardGroup.OnPositionChanged += OnPositionChanged;
            cardGroup.cardContainer.ItemAddedEvent.AddCallback(OnCardAdded, GameEventPriority.LOW);
            cardGroup.cardContainer.ItemRemovedEvent.AddCallback(OnCardRemoved, GameEventPriority.LOW);
            
            RearrangeCardViews(true);
        }

        private void OnPositionChanged(CardGroup cardGroup)
        {
            RearrangeCardViews(true);
        }
        
        private void OnCardAdded(ContainerItemAddedEvent e)
        {
            RearrangeCardViews(false);
        }
        
        private void OnCardRemoved(ContainerItemRemovedEvent e)
        {
            if (cardGroup.count == 1)
            {
                var existingCard = cardGroup.cardContainer.GetAllItems<ICard>().First();
                var cardView = CardViewManager.GetCardView(existingCard);
                var position = cardView.transform.position.XY();
                // Debug.LogError($"{cardGroup.name} : {cardGroup.transform.position.XY()}, {cardView.name} : {position}");
                cardGroup.SetPosition(position);
            }
            else
            {
                RearrangeCardViews(false);
            }
        }

        [Button]
        public void RearrangeCardViews(bool isInstant)
        {
            // Debug.LogError($"Rearrange {name} : {cardGroup.count}");
            
            if (cardGroup.count == 0) return;

            var cardGroupCollider = cardGroup.GetComponent<CardGroupCollider>();

            var cards = cardGroup.cards.ToArray();
            
            foreach (var (index, rect) in cardGroupCollider.colliderRectangles.Enumerate())
            {
                if (index >= cards.Length)
                {
                    Debug.LogWarning(
                        $"CardGroup {cardGroup.name} has " +
                        $"more collider pivots {cardGroupCollider.colliderRectangles.Count} " +
                        $"than cards {cards.Length}.");
                    break;
                }
                
                var card = cards[index];

                var cardView = CardViewManager.GetCardView(card);
                    
                cardView.SetLocalPosition(rect.pivot, isInstant);
            }
        }
    }
}