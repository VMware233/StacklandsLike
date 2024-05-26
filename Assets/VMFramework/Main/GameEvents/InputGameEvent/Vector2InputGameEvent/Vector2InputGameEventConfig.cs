using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;

namespace VMFramework.GameEvents
{
    public sealed partial class Vector2InputGameEventConfig : InputGameEventConfig
    {
        public override Type gameItemType => typeof(Vector2InputGameEvent);
        
        private const string XY_TAB_GROUP = TAB_GROUP_NAME + "/" + INPUT_MAPPING_CATEGORY + "/XY";

        private const string X_CATEGORY = "X";
        
        private const string Y_CATEGORY = "Y";
        
        [TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_CATEGORY)]
        [JsonProperty]
        public bool isMagnitudeLessThan1 = true;

        [TabGroup(XY_TAB_GROUP, X_CATEGORY)]
        [JsonProperty]
        public bool isXFromAxis = false;

        [HideLabel, TabGroup(XY_TAB_GROUP, X_CATEGORY)]
        [ShowIf(nameof(isXFromAxis))]
        [JsonProperty]
        public InputAxisType xInputAxisType;

#if UNITY_EDITOR
        [TabGroup(XY_TAB_GROUP, X_CATEGORY)]
        [ListDrawerSettings(CustomAddFunction = nameof(AddActionGroupGUI))]
        [HideIf(nameof(isXFromAxis))]
#endif
        [JsonProperty]
        public List<InputActionGroup> xPositiveActionGroups = new();

#if UNITY_EDITOR
        [TabGroup(XY_TAB_GROUP, X_CATEGORY)]
        [ListDrawerSettings(CustomAddFunction = nameof(AddActionGroupGUI))]
        [HideIf(nameof(isXFromAxis))]
#endif
        [JsonProperty]
        public List<InputActionGroup> xNegativeActionGroups = new();

        [TabGroup(XY_TAB_GROUP, Y_CATEGORY)]
        [JsonProperty]
        public bool isYFromAxis = false;

        [HideLabel, TabGroup(XY_TAB_GROUP, Y_CATEGORY)]
        [ShowIf(nameof(isYFromAxis))]
        [JsonProperty]
        public InputAxisType yInputAxisType;

#if UNITY_EDITOR
        [TabGroup(XY_TAB_GROUP, Y_CATEGORY)]
        [ListDrawerSettings(CustomAddFunction = nameof(AddActionGroupGUI))]
        [HideIf(nameof(isYFromAxis))]
#endif
        [JsonProperty]
        public List<InputActionGroup> yPositiveActionGroups = new();

#if UNITY_EDITOR
        [TabGroup(XY_TAB_GROUP, Y_CATEGORY)]
        [ListDrawerSettings(CustomAddFunction = nameof(AddActionGroupGUI))]
        [HideIf(nameof(isYFromAxis))]
#endif
        [JsonProperty]
        public List<InputActionGroup> yNegativeActionGroups = new();
    }
}