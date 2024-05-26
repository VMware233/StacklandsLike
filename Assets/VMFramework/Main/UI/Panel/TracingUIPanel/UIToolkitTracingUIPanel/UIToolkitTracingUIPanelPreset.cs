using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public class UIToolkitTracingUIPanelPreset : UIToolkitPanelPreset, ITracingUIPanelPreset
    {
        protected const string TRACING_UI_SETTING_CATEGORY = "鼠标追随UI的设置";

        [LabelText("默认中心点"), SuffixLabel("左下角为(0, 0)"), 
         TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY, SdfIconType.Mouse, TextColor = "purple")]
        [MinValue(0), MaxValue(1)]
        [JsonProperty]
        public Vector2 defaultPivot = new(0, 1);

        [LabelText("允许溢出屏幕"), TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [JsonProperty]
        public bool enableOverflow;

        [LabelText("自动纠正中心点"), TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [HideIf(nameof(enableOverflow))]
        [JsonProperty]
        public bool autoPivotCorrection = true;
        
        [LabelText("开启自动鼠标跟随"), TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [JsonProperty]
        public bool enableAutoMouseTracing = false;

        [LabelText("跟随"), TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [ToggleButtons("持续跟随", "仅跟随一次")]
        [JsonProperty]
        public bool persistentTracing = true;

        [LabelText("使用绝对位置的Right进行左右定位"), LabelWidth(200), 
         TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [JsonProperty]
        public bool useRightPosition;

        [LabelText("使用绝对位置的Top进行上下定位"), TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [LabelWidth(200)]
        [JsonProperty]
        public bool useTopPosition;

        [LabelText("容器VisualElement名称"), TabGroup(TAB_GROUP_NAME, TRACING_UI_SETTING_CATEGORY)]
        [VisualElementName]
        [IsNotNullOrEmpty]
        [JsonProperty]
        public string containerVisualElementName;
    }
}