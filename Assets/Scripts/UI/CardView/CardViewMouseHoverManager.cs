using System.Collections.Generic;
using System.Runtime.CompilerServices;
using StackLandsLike.GameCore;
using UnityEngine;
using VMFramework.Procedure;

namespace StackLandsLike.UI
{
    [ManagerCreationProvider(nameof(GameManagerType.UI))]
    public sealed class CardViewMouseHoverManager : ManagerBehaviour<CardViewMouseHoverManager>
    {
        [field: SerializeField]
        public new Camera camera { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetCamera(Camera camera)
        {
            this.camera = camera;
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