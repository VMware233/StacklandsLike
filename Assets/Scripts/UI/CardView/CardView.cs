using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using StackLandsLike.Cards;
using StackLandsLike.GameCore;
using TMPro;
using UnityEngine;
using VMFramework.Containers;
using VMFramework.Core;
using VMFramework.GameEvents;

namespace StackLandsLike.UI
{
    [DisallowMultipleComponent]
    public sealed class CardView : MonoBehaviour
    {
        [ShowInInspector]
        private MeshFilter meshFilter;

        [ShowInInspector]
        private ColliderMouseEventTrigger trigger;
        
        [SerializeField]
        private GameObject defaultView;
        
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
                
                SetModel(card.model);
                
                SetCollider();
            }
        }

        private void SetModel(GameObject modelPrefab)
        {
            if (modelPrefab == null)
            {
                return;
            }
            
            if (meshFilter != null)
            {
                return;
            }
            
            defaultView.SetActive(false);
            
            var model = Instantiate(modelPrefab, transform);

            model.transform.eulerAngles = new Vector3(-90, 0, 0);

            meshFilter = model.transform.QueryFirstComponentInChildren<MeshFilter>(true);

            var bounds = meshFilter.sharedMesh.bounds;

            model.transform.localPosition -= new Vector3(0, 0, bounds.center.y + 2 * bounds.extents.y);
            
            SetCollider();
        }

        private void SetCollider()
        {
            if (this.trigger != null)
            {
                return;
            }
            
            ColliderMouseEventTrigger trigger = null;
            
            if (meshFilter != null && meshFilter.TryGetComponent(out MeshCollider meshCollider))
            {
                trigger = meshCollider.AddComponent<ColliderMouseEventTrigger>();
            }
            else
            {
                var boxCollider = gameObject.AddComponent<BoxCollider>();
                boxCollider.size = card.cardSize.InsertAsZ(0.3f);
                trigger = gameObject.AddComponent<ColliderMouseEventTrigger>();
            }
            
            trigger.SetOwner(transform);
            trigger.draggable = true;
            trigger.dragButton = MouseButtonType.LeftButton;

            foreach (var t in transform.QueryComponentsInChildren<Transform>(true))
            {
                t.gameObject.layer = GameSetting.cardViewGeneralSetting.cardViewLayer;
            }
            
            this.trigger = trigger;
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