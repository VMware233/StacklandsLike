using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public class AutoFillContainerConfig : BaseConfig
    {
        [HideInInspector]
        public UIToolkitPanelPreset preset;

        [LabelText("插槽自动补充的容器名称"), LabelWidth(180)]
        [ValueDropdown("@preset.GetVisualTreeNames()")]
        [JsonProperty]
        [IsNotNullOrEmpty]
        public string autoFillSlotContainerName;

        [LabelText("自动补充容器中插槽的预处理模式"), LabelWidth(180)]
        [JsonProperty]
        public AutoFillContainerSlotsPreprocessMode autoFillContainerSlotsPreprocessMode;

        [LabelText("自动补充插槽开始的序号"), LabelWidth(180)]
        [JsonProperty]
        public int autoFillSlotStartIndex = 0;

        [LabelText("插槽自动隐藏"), LabelWidth(180)]
        [JsonProperty]
        public bool slotsDisplayNoneIfNull = false;
    }
}