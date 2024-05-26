using System;
using System.Collections.Generic;
using VMFramework.Core;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Containers;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public partial class ContainerUIBasePreset : UIToolkitPanelPreset
    {
        protected const string CONTAINER_SETTING_CATEGORY = "容器UI设置";

        public override Type controllerType => typeof(ContainerUIBaseController);

        [LabelText("绑定的容器"), TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY, SdfIconType.Box,
             TextColor = "magenta")]
        [GamePrefabID(typeof(ContainerPreset))]
        [IsNotNullOrEmpty]
        [JsonProperty]
        public string bindContainerID;

        [LabelText("使用自定义插槽容器"), TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY)]
        [JsonProperty]
        public bool useCustomSlotSourceContainer = false;

        [LabelText("自定义插槽容器名称"), TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY)]
        [VisualElementName]
        [ShowIf(nameof(useCustomSlotSourceContainer))]
        [JsonProperty]
        public List<string> customSlotSourceContainerNames = new();

        [LabelText("自动分配插槽的序号"), TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY)]
        [JsonProperty]
        public bool autoAllocateSlotIndex = false;

        [LabelText("忽略预分配插槽序号"), TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY)]
        [JsonProperty]
        [ShowIf(nameof(autoAllocateSlotIndex))]
        public bool ignorePreallocateSlotIndex = false;

        [LabelText("是否自动补充插槽"), TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY)]
        [JsonProperty]
        public bool autoFillSlot = false;

#if UNITY_EDITOR
        [LabelText("自动补充插槽的容器设置"), TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY)]
        [ListDrawerSettings(CustomAddFunction = nameof(AddAutoFillContainerConfigGUI))]
        [ShowIf(nameof(autoFillSlot))]
#endif
        public List<AutoFillContainerConfig> autoFillContainerConfigs = new();

        [LabelText("容器UI的优先级"), TabGroup(TAB_GROUP_NAME, CONTAINER_SETTING_CATEGORY)]
        [JsonProperty]
        public int containerUIPriority = 0;

        public override void CheckSettings()
        {
            base.CheckSettings();

            bindContainerID.AssertIsNotNull(nameof(bindContainerID));
        }

        protected override void OnInit()
        {
            base.OnInit();

            ContainerUIManager.BindContainerUITo(id, bindContainerID);
        }
    }
}
