using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using StackLandsLike.GameCore;
using StackLandsLike.UI;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.Core;
using VMFramework.Core.Linq;
using VMFramework.GameEvents;

namespace StackLandsLike.Cards
{
    [RequireComponent(typeof(CardGroup))]
    [DisallowMultipleComponent]
    public class CardGroupCollider : MonoBehaviour
    {
        public CardGroup cardGroup { get; private set; }
        
        [ShowInInspector]
        private readonly List<RectangleFloat> _colliderRectangles = new();
        private readonly List<BoxCollider2D> colliders = new();
        
        public IReadOnlyList<RectangleFloat> colliderRectangles => _colliderRectangles;

        public void Init()
        {
            cardGroup = GetComponent<CardGroup>();
            colliders.AddRange(GetComponents<BoxCollider2D>());
            
            GenerateColliders();
            
            cardGroup.cardContainer.ItemAddedEvent.AddCallback(OnCardAdded, GameEventPriority.HIGH);
            cardGroup.cardContainer.ItemRemovedEvent.AddCallback(OnCardRemoved, GameEventPriority.HIGH);
        }

        private void OnCardAdded(ContainerItemAddedEvent e)
        {
            GenerateColliders();
        }
        
        private void OnCardRemoved(ContainerItemRemovedEvent e)
        {
            GenerateColliders();
        }

        private void GenerateColliders()
        {
            GenerateColliderInfos();
            GenerateBoxColliders();
        }

        private void GenerateColliderInfos()
        {
            _colliderRectangles.Clear();
            
            if (cardGroup.count == 0) return;
            
            var cards = cardGroup.cards.ToArray();

            float width = 0;

            if (cards.Length >= 1)
            {
                width -= cards[0].cardSize.x / 2;
            }

            foreach (var card in cards)
            {
                Vector2 size = card.cardSize;
                
                var startPoint = new Vector2(width, -size.y / 2);
                var endPoint = startPoint + size;
                
                _colliderRectangles.Add(new RectangleFloat(startPoint, endPoint));
                
                width += size.x;
            }
        }

        private void GenerateBoxColliders()
        {
            if (_colliderRectangles.Count > colliders.Count)
            {
                (_colliderRectangles.Count - colliders.Count).Repeat(AddNewCollider);
            }

            for (int i = 0; i < _colliderRectangles.Count; i++)
            {
                colliders[i].enabled = true;
                var rect = _colliderRectangles[i];
                var collider = colliders[i];
                collider.offset = rect.pivot;
                collider.size = rect.size;
            }

            for (int i = _colliderRectangles.Count; i < colliders.Count; i++)
            {
                colliders[i].enabled = false;
            }
        }

        private void AddNewCollider()
        {
            var collider = gameObject.AddComponent<BoxCollider2D>();
            collider.compositeOperation = Collider2D.CompositeOperation.Merge;
            colliders.Add(collider);
        }
    }
}