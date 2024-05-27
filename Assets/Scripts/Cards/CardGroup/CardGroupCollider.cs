using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using StackLandsLike.GameCore;
using StackLandsLike.UI;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.Core;
using VMFramework.GameEvents;

namespace StackLandsLike.Cards
{
    [RequireComponent(typeof(CardGroup))]
    [DisallowMultipleComponent]
    public class CardGroupCollider : MonoBehaviour
    {
        public CardGroup cardGroup { get; private set; }
        
        [ShowInInspector]
        private List<Vector2> _colliderPivots = new();
        private List<BoxCollider2D> colliders = new();
        
        private void Start()
        {
            cardGroup = GetComponent<CardGroup>();

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
            cardGroup.cardContainer.ItemRemovedEvent.AddCallback(OnCardRemoved, GameEventPriority.LOW);
        }

        private void OnCardRemoved(ContainerItemRemovedEvent e)
        {
            GenerateColliders();
        }

        private void OnCardAdded(ContainerItemAddedEvent e)
        {
            GenerateColliders();
        }

        private void GenerateColliders()
        {
            GenerateColliderPivots();
        }

        private void GenerateColliderPivots()
        {
            // _colliderPivots.Clear();
            //
            // if (cardGroup.count == 0) return;
            //
            // var width = cardGroup.count.Sqrt().Ceiling();
            // var height = (cardGroup.count.F() / width).Ceiling();
            //
            // var cards = cardGroup.cards.ToArray();
            //
            // Vector2 cardSize = GameSetting.cardGeneralSetting.cardViewSize;
            // Vector2 startPoint = cardGroup.transform.position.XY();
            // int index = 0;
            // for (int x = 0; x < width; x++)
            // {
            //     for (int y = 0; y < height; y++, index++)
            //     {
            //         if (index >= cards.Length)
            //         {
            //             break;
            //         }
            //         
            //         var card = cards[index];
            //
            //         var cardView = CardViewManager.GetCardView(card);
            //
            //         if (cardGroup == null)
            //         {
            //             continue;
            //         }
            //
            //         var position = startPoint + cardSize.Multiply(new Vector2(x, y));
            //         
            //         cardView.SetPosition(position, isInstant);
            //     }
            // }
        }
    }
}