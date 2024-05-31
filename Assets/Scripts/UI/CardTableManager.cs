using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using StackLandsLike.GameCore;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Procedure;

namespace StackLandsLike.UI
{
    [ManagerCreationProvider(nameof(GameManagerType.UI))]
    public sealed class CardTableManager : ManagerBehaviour<CardTableManager>
    {
        [SerializeField]
        [Required]
        private Transform checkPoint;
        
        [SerializeField]
        [Required]
        private Transform cardTablePlane;

        [ShowInInspector]
        public static float zPosition { get; private set; } = 0;

        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();

            if (cardTablePlane != null)
            {
                if (cardTablePlane.QueryFirstComponentInChildren<Collider>(true) == null)
                {
                    return;
                }

                var ray = new Ray(checkPoint.position, new Vector3(0, 0, 1));

                if (Physics.Raycast(ray, out var hit, 1000,
                        GameSetting.cardViewGeneralSetting.cardTableLayerMask))
                {
                    zPosition = hit.point.z;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetPositionInTable(Vector2 position)
        {
            return new Vector3(position.x, position.y, zPosition);
        }
    }
}