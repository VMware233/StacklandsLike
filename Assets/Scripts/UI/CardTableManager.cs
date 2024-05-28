using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using StackLandsLike.GameCore;
using UnityEngine;
using VMFramework.Procedure;

namespace StackLandsLike.UI
{
    [ManagerCreationProvider(nameof(GameManagerType.UI))]
    public sealed class CardTableManager : ManagerBehaviour<CardTableManager>
    {
        [SerializeField]
        [Required]
        private Transform cardTablePlane;

        [SerializeField]
        private float zOffset = 0;
        
        [ShowInInspector]
        public static float zPosition { get; private set; }

        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();

            if (cardTablePlane != null)
            {
                zPosition = cardTablePlane.position.z + zOffset;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetPositionInTable(Vector2 position)
        {
            return new Vector3(position.x, position.y, zPosition);
        }
    }
}