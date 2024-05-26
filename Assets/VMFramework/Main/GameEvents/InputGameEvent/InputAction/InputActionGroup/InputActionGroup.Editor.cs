#if UNITY_EDITOR
using UnityEngine;

namespace VMFramework.GameEvents
{
    public partial class InputActionGroup
    {
        private InputAction AddInputActionToListGUI()
        {
            return new InputAction()
            {
                type = InputType.KeyBoardOrMouseOrJoyStick,
                keyCode = KeyCode.None,
                keyBoardTriggerType = KeyBoardTriggerType.PressedDown,
                holdThreshold = 0.3f,
            };
        }
    }
}
#endif