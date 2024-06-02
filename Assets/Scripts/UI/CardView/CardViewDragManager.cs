using System;
using System.Collections.Generic;
using StackLandsLike.Cards;
using StackLandsLike.GameCore;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameEvents;
using VMFramework.Procedure;

namespace StackLandsLike.UI
{
    [ManagerCreationProvider(nameof(GameManagerType.UI))]
    public sealed class CardViewDragManager : ManagerBehaviour<CardViewDragManager>
    {
        public static CardView draggingCardView { get; private set; }

        public static void EnableDrag()
        {
            ColliderMouseEventManager.AddCallback(MouseEventType.DragBegin, OnDragBegin);
            
            ColliderMouseEventManager.AddCallback(MouseEventType.DragStay, OnDragStay);
            
            ColliderMouseEventManager.AddCallback(MouseEventType.DragEnd, OnDragEnd);
        }

        public static void DisableDrag()
        {
            draggingCardView = null;
            
            ColliderMouseEventManager.RemoveCallback(MouseEventType.DragBegin, OnDragBegin);
            
            ColliderMouseEventManager.RemoveCallback(MouseEventType.DragStay, OnDragStay);
            
            ColliderMouseEventManager.RemoveCallback(MouseEventType.DragEnd, OnDragEnd);
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
                
            var oldDraggingCardView = draggingCardView;
            draggingCardView = null;

            if (CardViewMouseHoverManager.TryGetMouseCardView(out var selectedCardViews) == false)
            {
                selectedCardViews = new List<CardView>() { oldDraggingCardView };
            }

            if (cardView.card == null)
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

                switch (count)
                {
                    case 1:
                        oldGroup.SetPosition(targetPosition);
                        break;
                    case > 1:
                        cardView.card.MoveOutOfGroup(targetPosition);
                        break;
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
                else
                {
                    cardView.card.group.RearrangeCardViews(false);
                }

                break;
            }
        }
    }
}