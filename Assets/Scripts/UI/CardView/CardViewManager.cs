using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using StackLandsLike.Cards;
using StackLandsLike.GameCore;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Pool;
using VMFramework.Procedure;

namespace StackLandsLike.UI
{
    [ManagerCreationProvider(nameof(GameManagerType.UI))]
    public sealed class CardViewManager : ManagerBehaviour<CardViewManager>, IManagerBehaviour
    {
        private static readonly StackComponentPool<CardView> cache = new(() =>
        {
            var cardViewObject = Instantiate(GameSetting.cardViewGeneralSetting.cardViewPrefab);
            var cardView = cardViewObject.GetComponent<CardView>();
            cardView.transform.position = cardView.transform.position.ReplaceZ(CardTableManager.zPosition);
            return cardView;
        }, onReturnCallback: cardView =>
        {
            cardView.gameObject.SetActive(false);
            cardView.transform.SetParent(instance.cardViewCacheContainer);
        });
        
        [SerializeField]
        private Transform cardViewCacheContainer;

        [ShowInInspector]
        private static Dictionary<ICard, CardView> allCardViews = new();

        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();

            CardGroupManager.OnCardGroupCreated += cardGroup =>
            {
                foreach (var card in cardGroup.cards)
                {
                    GetCardView(card);
                }
            };

            CardGroupManager.OnCardGroupDestroyed += cardGroup =>
            {
                foreach (var card in cardGroup.cards)
                {
                    ReturnCardView(card);
                }
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNull]
        public static CardView GetCardView(ICard card)
        {
            if (allCardViews.TryGetValue(card, out var cardView))
            {
                return cardView;
            }
            
            cardView = cache.Get();
            cardView.SetCard(card);
            allCardViews.Add(card, cardView);
            
            card.OnGroupChangedEvent += OnCardGroupChanged;
            return cardView;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReturnCardView(ICard card)
        {
            if (allCardViews.Remove(card, out var cardView))
            {
                cardView.SetCard(null);
                cache.Return(cardView);
                card.OnGroupChangedEvent -= OnCardGroupChanged;
            }
        }

        public static void OnCardGroupChanged(ICard card, CardGroup cardGroup)
        {
            if (cardGroup == null)
            {
                return;
            }

            if (allCardViews.TryGetValue(card, out var cardView) == false)
            {
                Debug.LogWarning("Card not found in card view dictionary.");
                return;
            }
            
            cardView.transform.SetParent(cardGroup.transform);
        }
    }
}