using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using StackLandsLike.Cards;
using StackLandsLike.GameCore;
using TMPro;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.Core;

namespace StackLandsLike.UI
{
    [DisallowMultipleComponent]
    public sealed class CardView : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TextMeshProUGUI title;
        
        [ShowInInspector]
        public ICard card { get; private set; }
        
        public void SetCard(ICard card)
        {
            if (this.card != null)
            {
                this.card.OnCountChangedEvent -= OnCountChanged;
            }
            
            this.card = card;

            if (this.card != null)
            {
                this.card.OnCountChangedEvent += OnCountChanged;

                if (card.group == null)
                {
                    Debug.LogError($"{card} has no group!");
                }
                else
                {
                    transform.SetParent(this.card.group.transform);
                    transform.localPosition = Vector3.zero;
                }
                
                title.text = $"{card.name}x{card.count}";

                name = $"{card.name} Card View";
            }
        }

        private void OnCountChanged(IContainerItem item, int oldCount, int newCount)
        {
            if (item is not ICard)
            {
                return;
            }

            if (newCount <= 0)
            {
                CardViewManager.ReturnCardView(card);
                return;
            }
            
            name = $"{card.name}x{newCount} Card View";
            
            title.text = $"{card.name}x{newCount}";
        }

        /// <summary>
        /// 设置卡牌视图的位置，只需要提供XY坐标，z坐标由<see cref="CardTableManager"/>决定。
        /// </summary>
        /// <param name="position"></param>
        /// <param name="isInstant"></param>
        public void SetLocalPosition(Vector2 position, bool isInstant = true)
        {
            transform.DOKill();
            Vector3 positionInTable = CardTableManager.GetPositionInTable(position);
            if (isInstant)
            {
                transform.localPosition = positionInTable;
            }
            else
            {
                transform.DOLocalMove(positionInTable, GameSetting.cardViewGeneralSetting.cardViewMovingTime);
            }
        }

        /// <summary>
        /// 设置卡牌视图的位置，只需要提供XY坐标，z坐标由<see cref="CardTableManager"/>决定。
        /// </summary>
        /// <param name="position"></param>
        /// <param name="isInstant"></param>
        public void SetPosition(Vector2 position, bool isInstant = true)
        {
            transform.DOKill();
            Vector3 positionInTable = CardTableManager.GetPositionInTable(position);
            if (isInstant)
            {
                transform.position = positionInTable;
            }
            else
            {
                transform.DOMove(positionInTable, GameSetting.cardViewGeneralSetting.cardViewMovingTime);
            }
        }
        
        public Vector2 GetPosition()
        {
            return transform.position.XY();
        }
    }
}