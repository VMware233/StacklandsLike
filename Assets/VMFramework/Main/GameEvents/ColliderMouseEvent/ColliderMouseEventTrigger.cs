using UnityEngine;
using Sirenix.OdinInspector;

namespace VMFramework.GameEvents
{
    [DisallowMultipleComponent]
    public sealed class ColliderMouseEventTrigger : MonoBehaviour
    {
        [LabelText("是否允许拖拽")]
        public bool draggable = false;

        [LabelText("触发拖拽的键"), ShowIf(nameof(draggable))]
        public MouseButtonType dragButton = MouseButtonType.LeftButton;
        
        [field: LabelText("拥有者Transform")]
        [field: Required]
        [field: SerializeField]
        public Transform owner { get; private set; }
        
        public void SetOwner(Transform owner)
        {
            if (this.owner != null && owner != null)
            {
                Debug.LogWarning($"ColliderMouseEventTrigger already has an owner : {owner.name}!");
            }
            
            this.owner = owner;
        }
    }
}
