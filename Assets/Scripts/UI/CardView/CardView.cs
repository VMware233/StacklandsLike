using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using StackLandsLike.Cards;
using StackLandsLike.GameCore;
using UnityEngine;
using VMFramework.Core;

namespace StackLandsLike.UI
{
    public sealed class CardView : MonoBehaviour
    {
        [ShowInInspector]
        public ICard card { get; private set; }
        
        public void SetCard(ICard card)
        {
            this.card = card;
            
            transform.SetParent(card.group.transform);
        }

        public void SetPosition(Vector2 position, bool isInstant = true)
        {
            transform.DOKill();
            if (isInstant)
            {
                transform.position = position.InsertAsZ(CardTableManager.zPosition);
            }
            else
            {
                transform.DOMove(position.InsertAsZ(CardTableManager.zPosition),
                    GameSetting.cardViewGeneralSetting.cardViewMovingTime);
            }
            
        }
    }
}