using System;
using StackLandsLike.Cards;
using StackLandsLike.GameCore;
using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace StackLandsLike.UI
{
    [ManagerCreationProvider(nameof(GameManagerType.UI))]
    public sealed class CardViewSplitManager : ManagerBehaviour<CardViewSplitManager>, IManagerBehaviour
    {
        public delegate void CardSplitEventHandler(ICard card, int oldCount, int newCount);
        
        public static event CardSplitEventHandler OnSplitCard; 
        
        void IInitializer.OnInitComplete(Action onDone)
        {
            ColliderMouseEventManager.AddCallback(MouseEventType.RightMouseButtonClick, OnRightMouseButtonClick);
            
            onDone();
        }

        private static void OnRightMouseButtonClick(ColliderMouseEvent e)
        {
            if (CardViewDragManager.draggingCardView != null)
            {
                return;
            }
            
            var owner = e.trigger.owner;
            if (owner.TryGetComponent(out CardView cardView) == false)
            {
                return;
            }

            var cardCount = cardView.card.count;
            
            if (cardCount <= 1)
            {
                return;
            }
            
            var splitCount = cardCount / 2;
            var newCard = IGameItem.Create<ICard>(cardView.card.id);
            newCard.count = splitCount;
            
            cardView.card.count -= splitCount;

            CardGroupManager.CreateCardGroup(newCard, cardView.GetGroupPosition());
            
            OnSplitCard?.Invoke(cardView.card, cardCount, cardView.card.count);
        }
    }
}