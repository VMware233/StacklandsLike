using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;

namespace VMFramework.GameEvents
{
    public partial class InputActionGroup : BaseConfig
    {
#if UNITY_EDITOR
        [ListDrawerSettings(ShowFoldout = false, DefaultExpandedState = true,
            CustomAddFunction = nameof(AddInputActionToListGUI))]
#endif
        [JsonProperty]
        public List<InputAction> actions = new();

        public InputActionGroup()
        {
            actions.Add(new());
        }

        public InputActionGroup(KeyCode keyCode,
            KeyBoardTriggerType keyBoardTriggerType)
        {
            InputAction action;
            action.type = InputType.KeyBoardOrMouseOrJoyStick;
            action.keyCode = keyCode;
            action.keyBoardTriggerType = keyBoardTriggerType;
            action.holdThreshold = 0;

            actions.Add(action);
        }
    }
}