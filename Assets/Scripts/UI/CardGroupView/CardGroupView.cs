using System;
using System.Linq;
using Sirenix.OdinInspector;
using StackLandsLike.Cards;
using StackLandsLike.GameCore;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.Core;

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
            cardGroup.cardContainer.OnItemRemovedEvent += OnCardRemoved;
        }

        private void OnPositionChanged(CardGroup cardGroup)
        {
            RearrangeCardViews(true);
        }
        
        private void OnCardRemoved(IContainer container, int index, IContainerItem item)
        {
            if (item is not ICard card)
            {
                return;
            }
            
            if (cardGroup.count == 1)
            {
                var existingCard = cardGroup.cardContainer.GetAllItems<ICard>().First();
                var cardView = CardViewManager.GetCardView(existingCard);
                cardGroup.SetPosition(cardView.transform.position.XY());
            }
            else
            {
                RearrangeCardViews(false);
            }
        }

        [Button]
        public void RearrangeCardViews(bool isInstant)
        {
            if (cardGroup.count == 0) return;
            
            var width = cardGroup.count.Sqrt().Ceiling();
            var height = (cardGroup.count.F() / width).Ceiling();
            
            var cards = cardGroup.cards.ToArray();

            Vector2 cardSize = GameSetting.cardGeneralSetting.cardViewSize;
            Vector2 startPoint = cardGroup.transform.position.XY();
            int index = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++, index++)
                {
                    if (index >= cards.Length)
                    {
                        break;
                    }
                    
                    var card = cards[index];

                    var cardView = CardViewManager.GetCardView(card);

                    if (cardGroup == null)
                    {
                        continue;
                    }

                    var position = startPoint + cardSize.Multiply(new Vector2(x, y));
                    
                    cardView.SetPosition(position, isInstant);
                }
            }
        }
    }
}