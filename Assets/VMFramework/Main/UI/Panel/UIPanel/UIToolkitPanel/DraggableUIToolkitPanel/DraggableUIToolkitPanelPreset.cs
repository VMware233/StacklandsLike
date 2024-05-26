using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public class DraggableUIToolkitPanelPreset : UIToolkitPanelPreset
    {
        [LabelText("启用拖拽"), TabGroup(TAB_GROUP_NAME, UI_TOOLKIT_PANEL_CATEGORY)]
        [JsonProperty]
        public bool enableDragging = false;

        [LabelText("可拖拽区域名称"), TabGroup(TAB_GROUP_NAME, UI_TOOLKIT_PANEL_CATEGORY)]
        [VisualElementName]
        [EnableIf(nameof(enableDragging))]
        [JsonProperty]
        public string draggableAreaName;

        [LabelText("拖拽容器名称"), TabGroup(TAB_GROUP_NAME, UI_TOOLKIT_PANEL_CATEGORY)]
        [VisualElementName]
        [EnableIf(nameof(enableDragging))]
        [JsonProperty]
        public string draggingContainerName;

        [LabelText("是否可拖拽溢出屏幕"), TabGroup(TAB_GROUP_NAME, UI_TOOLKIT_PANEL_CATEGORY)]
        [EnableIf(nameof(enableDragging))]
        [JsonProperty]
        public bool draggableOverflowScreen = false;

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            if (enableDragging)
            {
                draggableAreaName.AssertIsNotNull(nameof(draggableAreaName));
                draggingContainerName.AssertIsNotNull(nameof(draggingContainerName));
            }
        }
    }
}