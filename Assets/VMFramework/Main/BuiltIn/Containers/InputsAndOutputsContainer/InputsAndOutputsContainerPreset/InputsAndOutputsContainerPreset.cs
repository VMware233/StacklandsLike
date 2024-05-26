using System;
using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.OdinExtensions;

namespace VMFramework.Containers
{
    public partial class InputsAndOutputsContainerPreset : GridContainerPreset
    {
        public override Type gameItemType => typeof(InputsAndOutputsContainer);

        [LabelText("输入槽位范围"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [RangeSlider(0, nameof(maxSlotIndex))]
        public RangeIntegerConfig inputsRange = new(1, 12);

        [LabelText("输出槽位范围"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [RangeSlider(0, nameof(maxSlotIndex))]
        public RangeIntegerConfig outputsRange = new(13, 16);
    }
}