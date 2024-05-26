using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;

namespace VMFramework.GameEvents
{
    public sealed partial class FloatInputGameEventConfig : InputGameEventConfig
    {
        public override Type gameItemType => typeof(FloatInputGameEvent);

        [TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_CATEGORY)]
        [JsonProperty]
        public bool isFromAxis = false;

        [HideLabel, TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_CATEGORY)]
        [ShowIf(nameof(isFromAxis))]
        [JsonProperty]
        public InputAxisType inputAxisType;

#if UNITY_EDITOR
        [TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_CATEGORY)]
        [HideIf(nameof(isFromAxis))]
        [ListDrawerSettings(CustomAddFunction = nameof(AddActionGroupGUI))]
#endif
        [JsonProperty]
        public List<InputActionGroup> positiveActionGroups = new();

#if UNITY_EDITOR
        [TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_CATEGORY)]
        [HideIf(nameof(isFromAxis))]
        [ListDrawerSettings(CustomAddFunction = nameof(AddActionGroupGUI))]
#endif
        [JsonProperty]
        public List<InputActionGroup> negativeActionGroups = new();
    }
}