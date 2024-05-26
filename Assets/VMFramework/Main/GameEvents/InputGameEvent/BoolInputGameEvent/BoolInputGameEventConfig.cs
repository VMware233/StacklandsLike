using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core.Linq;

namespace VMFramework.GameEvents
{
    public sealed partial class BoolInputGameEventConfig : InputGameEventConfig
    {
        public override Type gameItemType => typeof(BoolInputGameEvent);

#if UNITY_EDITOR
        [TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_CATEGORY)]
        [ListDrawerSettings(CustomAddFunction = nameof(AddActionGroupGUI))]
#endif
        [JsonProperty]
        public List<InputActionGroup> actionGroups = new();

        public override void CheckSettings()
        {
            base.CheckSettings();

            if (actionGroups.IsNullOrEmpty())
            {
                Debug.LogWarning($"{this} has no input action groups.");
            }
        }
    }
}