using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using StackLandsLike.Cards;
using StackLandsLike.GameCore;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameEvents;
using VMFramework.Procedure;

namespace StackLandsLike.UI
{
    [ManagerCreationProvider(nameof(GameManagerType.UI))]
    public sealed class CardViewDragManager : ManagerBehaviour<CardViewDragManager>, IManagerBehaviour
    {
        [field: SerializeField]
        public new Camera camera { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetCamera(Camera camera)
        {
            this.camera = camera;
        }
        
        private CardView draggingCardView;

        void IInitializer.OnInitComplete(Action onDone)
        {
            ColliderMouseEventManager.AddCallback(MouseEventType.DragBegin, e =>
            {
                var owner = e.trigger.owner;
                if (owner.TryGetComponent(out CardView cardView) == false)
                {
                    return;
                }
                
                draggingCardView = cardView;
            });
            
            ColliderMouseEventManager.AddCallback(MouseEventType.DragStay, e =>
            {
                if (TryGetMousePositionInCardTable(out var position) == false)
                {
                    return;
                }
                
                var owner = e.trigger.owner;
                if (owner.TryGetComponent(out CardView cardView) == false)
                {
                    return;
                }
                
                cardView.SetPosition(position);
            });
            
            ColliderMouseEventManager.AddCallback(MouseEventType.DragEnd, e =>
            {
                var owner = e.trigger.owner;
                if (owner.TryGetComponent(out CardView cardView) == false)
                {
                    return;
                }
                
                draggingCardView = null;

                if (TryGetMouseCardView(out var selectedCardViews) == false)
                {
                    return;
                }

                if (selectedCardViews.Count == 1 && selectedCardViews[0] == cardView)
                {
                    Vector2 position = cardView.transform.position.XY();
                    if (cardView.card.group.count > 1)
                    {
                        cardView.card.MoveOutOfGroup(position);
                    }
                    else
                    {
                        cardView.card.group.SetPosition(position);
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
                    
                    selectedCardView.card.group.RearrangeCardViews(false);
                }
            });
            
            onDone();
        }

        /// <summary>
        /// 获取鼠标指向的卡卓的位置
        /// </summary>
        /// <returns></returns>
        public static bool TryGetMousePositionInCardTable(out Vector2 position)
        {
            var ray = instance.camera.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);

            LayerMask layerMask = GameSetting.cardViewGeneralSetting.cardTableLayer.ToLayerMask();
            
            if (Physics.Raycast(ray, out var hitInfo, 1000f, layerMask))
            {
                position = hitInfo.point;
                return true;
            }

            position = Vector3.zero;
            return false;
        }

        /// <summary>
        /// 获取鼠标指向的卡片
        /// </summary>
        /// <returns></returns>
        public static bool TryGetMouseCardView(out List<CardView> cardViews)
        {
            var ray = instance.camera.ScreenPointToRay(Input.mousePosition);

            LayerMask layerMask = GameSetting.cardViewGeneralSetting.cardViewLayer.ToLayerMask();

            var hitInfos = Physics.RaycastAll(ray, 1000f, layerMask);

            cardViews = new();

            foreach (var hitInfo in hitInfos)
            {
                if (hitInfo.collider == null)
                {
                    continue;
                }

                if (hitInfo.collider.TryGetComponent(out CardView cardView))
                {
                    cardViews.Add(cardView);
                }
            }
            
            return cardViews.Count > 0;
        }
    }
}