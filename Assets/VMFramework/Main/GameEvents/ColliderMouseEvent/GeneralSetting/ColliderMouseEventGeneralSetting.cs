using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.GameEvents
{
    public sealed partial class ColliderMouseEventGeneralSetting : GeneralSetting
    {
        [LabelText("优先检测哪个维度的物体")]
        public ObjectDimensions dimensionsDetectPriority = ObjectDimensions.TWO_D;
        
        [LabelText("2D检测的射线长度")]
        [Range(0, 100)]
        public float detectDistance2D = 100;
    }
}
