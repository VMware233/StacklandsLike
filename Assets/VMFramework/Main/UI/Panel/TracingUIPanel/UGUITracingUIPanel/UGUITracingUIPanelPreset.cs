using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public class UGUITracingUIPanelPreset : UGUIPanelPreset, ITracingUIPanelPreset
    {
        protected const string TRACING_UI_SETTING_CATEGORY = "鼠标追随UI的设置";

        public override Type controllerType => typeof(UGUITracingUIPanelController);

        [LabelText("默认中心点"), TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [SuffixLabel("左下角为(0, 0)")]
        [MinValue(0), MaxValue(1)]
        [JsonProperty]
        public Vector2 defaultPivot = new(0, 1);

        [LabelText("允许溢出屏幕"), TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [JsonProperty]
        public bool enableOverflow = false;

        [LabelText("自动纠正中心点"), TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [HideIf(nameof(enableOverflow))]
        [JsonProperty]
        public bool autoPivotCorrection = true;

        [LabelText("跟随模式"), TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [JsonProperty]
        [ToggleButtons("持续跟随", "仅跟随一次")]
        public bool persistentTracing = true;

        [LabelText("开启自动鼠标跟随"), TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [JsonProperty]
        public bool enableAutoMouseTracing = false;
    }
}
