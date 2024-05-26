using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GameEvents
{
    public struct InputAction
    {
        [HideLabel]
        [EnumToggleButtons]
        public InputType type;

        [ShowIf(nameof(type), InputType.KeyBoardOrMouseOrJoyStick)]
        public KeyCode keyCode;

        [ShowIf(nameof(type), InputType.KeyBoardOrMouseOrJoyStick)]
        [EnumToggleButtons]
        public KeyBoardTriggerType keyBoardTriggerType;

        [ShowIf(nameof(DisplayHoldThresholdGUI))]
        [MinValue(0)]
        public float holdThreshold;

        private bool DisplayHoldThresholdGUI()
        {
            return type == InputType.KeyBoardOrMouseOrJoyStick &&
                   keyBoardTriggerType is KeyBoardTriggerType.IsHolding
                       or KeyBoardTriggerType.IsHoldingAfterThreshold
                       or KeyBoardTriggerType.HoldAndRelease;
        }

        public InputAction Copy()
        {
            return new InputAction()
            {
                type = type,
                keyCode = keyCode,
                keyBoardTriggerType = keyBoardTriggerType,
                holdThreshold = holdThreshold
            };
        }
    }
}