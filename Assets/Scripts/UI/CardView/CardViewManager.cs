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
        [SerializeField]
        private Transform cardViewCacheContainer;

        [ShowInInspector]
        private static readonly Dictionary<string, StackComponentPool<CardView>> allCaches = new();
        
        [ShowInInspector]
        private static Dictionary<ICard, CardView> allCardViews = new();

        public static event Action<CardView> OnCardViewCreated;
        
        public static event Action<CardView> OnCardViewDestroyed;

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
        private static StackComponentPool<CardView> GetCache(string cardID)
        {
            if (allCaches.TryGetValue(cardID, out var cache))
            {
                return cache;
            }
            
            cache = new(() =>
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
            
            allCaches.Add(cardID, cache);
            return cache;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNull]
        public static CardView GetCardView(ICard card)
        {
            if (allCardViews.TryGetValue(card, out var cardView))
            {
                return cardView;
            }
            
            var cache = GetCache(card.id);
            cardView = cache.Get();
            cardView.SetCard(card);
            allCardViews.Add(card, cardView);
            
            card.OnGroupChangedEvent += OnCardGroupChanged;
            
            OnCardViewCreated?.Invoke(cardView);
            
            return cardView;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReturnCardView(ICard card)
        {
            if (allCardViews.Remove(card, out var cardView))
            {
                cardView.SetCard(null);
                var cache = GetCache(card.id);
                cache.Return(cardView);
                card.OnGroupChangedEvent -= OnCardGroupChanged;
                
                OnCardViewDestroyed?.Invoke(cardView);
            }
        }

        private static void OnCardGroupChanged(ICard card, CardGroup cardGroup)
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