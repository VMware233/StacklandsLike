using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    [JsonObject(MemberSerialization.OptIn)]
    [PreviewComposite]
    public struct TooltipPriority
    {
        [PropertyTooltip("优先级类型")]
        [JsonProperty, SerializeField]
        private TooltipPriorityType priorityType;

        [PropertyTooltip("优先级预设")]
        [TooltipPriorityPresetID]
        [ShowIf(nameof(priorityType), TooltipPriorityType.Preset)]
        [JsonProperty, SerializeField]
        private string presetID;

        [PropertyTooltip("优先级")]
        [ShowIf(nameof(priorityType), TooltipPriorityType.Custom)]
        [JsonProperty, SerializeField]
        private int priority;

        public TooltipPriority(int priority)
        {
            this.priorityType = TooltipPriorityType.Custom;
            this.priority = priority;
            presetID = null;
        }

        public TooltipPriority(string presetID)
        {
            this.priorityType = TooltipPriorityType.Preset;
            this.presetID = presetID;
            priority = 0;
        }

        public override string ToString()
        {
            return priorityType switch
            {
                TooltipPriorityType.Preset => presetID,
                TooltipPriorityType.Custom => priority.ToString(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public int GetPriority()
        {
            switch (priorityType)
            {
                case TooltipPriorityType.Preset:
                    if (GameCoreSetting.tooltipGeneralSetting.tooltipPriorityPresets.TryGetConfigRuntime(
                            presetID, out var config))
                    {
                        return config.priority;
                    }
                    
                    Debug.LogWarning($"No Tooltip Priority Preset found with ID: {presetID}");
                    return 0;
                case TooltipPriorityType.Custom:
                    return priority;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static implicit operator int(TooltipPriority config)
        {
            return config.GetPriority();
        }
    }
}