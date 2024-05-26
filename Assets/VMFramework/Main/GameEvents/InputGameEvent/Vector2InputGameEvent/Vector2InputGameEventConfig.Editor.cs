#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GameEvents
{
    public partial class Vector2InputGameEventConfig
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            xPositiveActionGroups ??= new();
            xNegativeActionGroups ??= new();
            yPositiveActionGroups??= new();
            yNegativeActionGroups??= new();
        }
        
        [Button("快速添加WASD输入动作组", ButtonSizes.Medium), TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_CATEGORY)]
        private void AddWASDActionGroupGUI()
        {
            isXFromAxis = false;
            isYFromAxis = false;

            xPositiveActionGroups.Clear();
            xPositiveActionGroups.Add(new(KeyCode.D, KeyBoardTriggerType.IsPressing));
            xPositiveActionGroups.Add(new(KeyCode.RightArrow, KeyBoardTriggerType.IsPressing));

            xNegativeActionGroups.Clear();
            xNegativeActionGroups.Add(new(KeyCode.A, KeyBoardTriggerType.IsPressing));
            xNegativeActionGroups.Add(new(KeyCode.LeftArrow, KeyBoardTriggerType.IsPressing));

            yPositiveActionGroups.Clear();
            yPositiveActionGroups.Add(new(KeyCode.W, KeyBoardTriggerType.IsPressing));
            yPositiveActionGroups.Add(new(KeyCode.UpArrow, KeyBoardTriggerType.IsPressing));

            yNegativeActionGroups.Clear();
            yNegativeActionGroups.Add(new(KeyCode.S, KeyBoardTriggerType.IsPressing));
            yNegativeActionGroups.Add(new(KeyCode.DownArrow, KeyBoardTriggerType.IsPressing));
        }
    }
}
#endif