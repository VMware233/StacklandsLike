using System;
using System.Linq;
using StackLandsLike.Cards;
using StackLandsLike.GameCore;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.Core;
using VMFramework.GameEvents;
using VMFramework.Procedure;

namespace StackLandsLike.UI
{
    [ManagerCreationProvider(nameof(GameManagerType.UI))]
    public sealed class CardViewDragManager : ManagerBehaviour<CardViewDragManager>, IManagerBehaviour
    {
        public static CardView draggingCardView { get; private set; }

        void IInitializer.OnInitComplete(Action onDone)
        {
            ColliderMouseEventManager.AddCallback(MouseEventType.DragBegin, OnDragBegin);
            
            ColliderMouseEventManager.AddCallback(MouseEventType.DragStay, OnDragStay);
            
            ColliderMouseEventManager.AddCallback(MouseEventType.DragEnd, OnDragEnd);
            
            onDone();
        }

        private static void OnDragBegin(ColliderMouseEvent e)
        {
            var owner = e.trigger.owner;
            if (owner.TryGetComponent(out CardView cardView) == false)
            {
                return;
            }
                
            draggingCardView = cardView;
        }

        private static void OnDragStay(ColliderMouseEvent e)
        {
            if (CardViewMouseHoverManager.TryGetMousePositionInCardTable(out var position) == false)
            {
                return;
            }
                
            var owner = e.trigger.owner;
            if (owner.TryGetComponent(out CardView cardView) == false)
            {
                return;
            }
                
            cardView.SetPosition(position);
        }

        private static void OnDragEnd(ColliderMouseEvent e)
        {
            var owner = e.trigger.owner;
            if (owner.TryGetComponent(out CardView cardView) == false)
            {
                return;
            }
                
            draggingCardView = null;

            if (CardViewMouseHoverManager.TryGetMouseCardView(out var selectedCardViews) == false)
            {
                return;
            }

            if (cardView.card.group == null)
            {
                Debug.LogError($"{cardView.card} has no group");
                return;
            }

            if (selectedCardViews.Count == 1 && selectedCardViews[0] == cardView)
            {
                Vector2 targetPosition = cardView.transform.position.XY();
                int count = cardView.card.group.count;
                CardGroup oldGroup = cardView.card.group;
                if (count > 1)
                {
                    cardView.card.MoveOutOfGroup(targetPosition);

                    if (count == 2)
                    {
                        var originalCard = oldGroup.cardContainer.GetAllValidItems<ICard>().First();
                        var originalCardView = CardViewManager.GetCardView(originalCard);
                        
                        oldGroup.SetPosition(originalCardView.GetPosition());
                    }
                }
                else
                {
                    oldGroup.SetPosition(targetPosition);
                }
                    
                return;
            }

            foreach (var selectedCardView in selectedCardViews)
            {
                if (selectedCardView == cardView)
                {
                    continue;
                }

                if (cardView.card.group != selectedCardView.card.group)
                {
                    cardView.card.MoveToGroup(selectedCardView.card.group);
                }
                    
                // selectedCardView.card.group.RearrangeCardViews(false);

                break;
            }
        }
    }
}