using System;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public sealed class CardGroupSelection : ManagerBehaviour<CardGroupSelection>, IManagerBehaviour
    {
        public static CardGroup selectedCardGroup { get; private set; }

        public static event Action<CardGroup> OnSelectedCardGroup;
        public static event Action<CardGroup> OnDeselectedCardGroup;
        
        [SerializeField]
        private LineRenderer _lineRenderer;
        
        [SerializeField]
        private Material _lineRendererMaterial;

        [SerializeField]
        private float zOffset = 0;
        
        [SerializeField]
        private float lineScrollSpeed = 0.1f;
        
        private static float xOffset = 0;
        
        private static readonly int mainTex = Shader.PropertyToID("_MainTex");

        private void Update()
        {
            xOffset += lineScrollSpeed * Time.deltaTime;
            _lineRendererMaterial.SetTextureOffset(mainTex, new Vector2(xOffset, 0));
        }

        public void OnInitComplete(Action onDone)
        {
            DisableSelection();
            
            OnSelectedCardGroup += OnSelectedCardGroupEvent;
            OnDeselectedCardGroup += OnDeselectedCardGroupEvent;
            
            ColliderMouseEventManager.AddCallback(MouseEventType.PointerEnter, OnPointerEnter);
            ColliderMouseEventManager.AddCallback(MouseEventType.PointerLeave, OnPointerLeave);
            
            onDone();
        }

        private static void OnPointerEnter(ColliderMouseEvent e)
        {
            var owner = e.trigger.owner;

            if (owner.TryGetComponent(out CardView cardView))
            {
                selectedCardGroup = cardView.card.group;
                OnSelectedCardGroup?.Invoke(selectedCardGroup);
            }
        }

        private static void OnPointerLeave(ColliderMouseEvent e)
        {
            var owner = e.trigger.owner;

            if (owner.TryGetComponent(out CardView cardView))
            {
                var cardGroup = cardView.card.group;

                if (selectedCardGroup == cardGroup)
                {
                    selectedCardGroup = null;
                    OnDeselectedCardGroup?.Invoke(cardGroup);
                }
            }
        }

        private static void OnSelectedCardGroupEvent(CardGroup cardGroup)
        {
            RefreshSelection(cardGroup);
            
            cardGroup.cardContainer.ItemAddedEvent.AddCallback(OnItemAdded, GameEventPriority.TINY);
            cardGroup.cardContainer.ItemRemovedEvent.AddCallback(OnItemRemoved, GameEventPriority.TINY);
            cardGroup.OnPositionChanged += RefreshSelection;
        }

        private static void OnDeselectedCardGroupEvent(CardGroup cardGroup)
        {
            cardGroup.cardContainer.ItemAddedEvent.RemoveCallback(OnItemAdded);
            cardGroup.cardContainer.ItemRemovedEvent.RemoveCallback(OnItemRemoved);
            cardGroup.OnPositionChanged -= RefreshSelection;
            
            DisableSelection();
        }
        
        private static void OnItemAdded(ContainerItemAddedEvent e)
        {
            RefreshSelection((CardGroup)e.container.owner);
        }

        private static void OnItemRemoved(ContainerItemRemovedEvent e)
        {
            RefreshSelection((CardGroup)e.container.owner);
        }

        private static void RefreshSelection(CardGroup cardGroup)
        {
            EnableSelection();

            var cardGroupCollider = cardGroup.GetComponent<CardGroupCollider>();

            var positions = cardGroupCollider.polygonVertices.Select(GetPosition).ToArray();
            
            instance._lineRenderer.positionCount = positions.Length;
            instance._lineRenderer.SetPositions(positions);

            var startPosXY = cardGroup.transform.position.XY();
            var startPosZ = CardTableManager.zPosition + instance.zOffset;
            instance._lineRenderer.transform.position = startPosXY.InsertAsZ(startPosZ);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Vector3 GetPosition(Vector2 position)
        {
            return new Vector3(position.x, position.y, 0);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnableSelection()
        {
            instance._lineRenderer.SetActive(true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DisableSelection()
        {
            instance._lineRenderer.SetActive(false);
        }
    }
}