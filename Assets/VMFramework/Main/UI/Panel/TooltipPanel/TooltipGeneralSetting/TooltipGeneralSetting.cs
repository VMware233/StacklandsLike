using VMFramework.Configuration;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public sealed partial class TooltipGeneralSetting : GeneralSetting
    {
        private const string TOOLTIP_CATEGORY = "Tooltip";

        private const string TOOLTIP_ID_BIND_CATEGORY =
            TAB_GROUP_NAME + "/" + TOOLTIP_CATEGORY + "/Tooltip ID Bind";

        private const string TOOLTIP_PRIORITY_CATEGORY =
            TAB_GROUP_NAME + "/" + TOOLTIP_CATEGORY + "/Tooltip Priority Bind";

        [TabGroup(TAB_GROUP_NAME, TOOLTIP_CATEGORY), TitleGroup(TOOLTIP_ID_BIND_CATEGORY)]
        [GamePrefabID(typeof(ITooltipPreset))]
        [IsNotNullOrEmpty]
        [JsonProperty]
        public string defaultTooltipID;

        [TitleGroup(TOOLTIP_ID_BIND_CATEGORY)]
        [JsonProperty]
        public GameTypeBasedConfigs<TooltipBindConfig> tooltipIDBindConfigs = new();

        [TitleGroup(TOOLTIP_PRIORITY_CATEGORY)]
        [JsonProperty, SerializeField]
        public DictionaryConfigs<string, TooltipPriorityPreset> tooltipPriorityPresets = new();

        [TitleGroup(TOOLTIP_PRIORITY_CATEGORY)]
        public GameTypeBasedConfigs<TooltipPriorityBindConfig> tooltipPriorityBindConfigs = new();

        [TitleGroup(TOOLTIP_PRIORITY_CATEGORY)]
        public TooltipPriority defaultPriority;

        public override void CheckSettings()
        {
            base.CheckSettings();

            if (defaultTooltipID.IsNullOrEmpty())
            {
                Debug.LogWarning($"{nameof(defaultTooltipID)} is not set.");
            }

            tooltipIDBindConfigs.CheckSettings();

            tooltipPriorityPresets.CheckSettings();

            tooltipPriorityBindConfigs.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();

            tooltipIDBindConfigs.Init();

            tooltipPriorityPresets.Init();

            tooltipPriorityBindConfigs.Init();
        }
    }
}
