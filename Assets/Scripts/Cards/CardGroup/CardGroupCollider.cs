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
        private readonly List<Vector2> _colliderPivots = new();
        private readonly List<BoxCollider2D> colliders = new();
        
        public IReadOnlyList<Vector2> colliderPivots => _colliderPivots;
        
        private void Start()
        {
            cardGroup = GetComponent<CardGroup>();
            colliders.AddRange(GetComponents<BoxCollider2D>());

            if (cardGroup.cardContainer == null)
            {
                cardGroup.OnInitialized += Init;
            }
            else
            {
                Init();
            }
        }

        private void Init()
        {
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
            GenerateColliderPivots();
            GenerateBoxColliders();
        }

        private void GenerateColliderPivots()
        {
            _colliderPivots.Clear();
            
            if (cardGroup.count == 0) return;
            
            var width = cardGroup.count.Sqrt().Ceiling();
            var height = (cardGroup.count.F() / width).Ceiling();
            
            var cards = cardGroup.cards.ToArray();
            
            Vector2 cardSize = GameSetting.cardGeneralSetting.cardViewSize;
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
            
                    var position = cardSize.Multiply(new Vector2(x, y));
                    
                    _colliderPivots.Add(position);
                }
            }
        }

        private void GenerateBoxColliders()
        {
            if (_colliderPivots.Count > colliders.Count)
            {
                (_colliderPivots.Count - colliders.Count).Repeat(AddNewCollider);
            }

            for (int i = 0; i < _colliderPivots.Count; i++)
            {
                colliders[i].enabled = true;
                var pivot = _colliderPivots[i];
                var collider = colliders[i];
                collider.offset = pivot;
                collider.size = GameSetting.cardGeneralSetting.cardViewSize;
            }

            for (int i = _colliderPivots.Count; i < colliders.Count; i++)
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