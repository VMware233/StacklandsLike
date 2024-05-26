using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;

namespace VMFramework.Containers
{
    public class GridContainerPreset : ContainerPreset
    {
        public override Type gameItemType => typeof(GridContainer);

        [LabelText("大小"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [MinValue(1)]
        [JsonProperty]
        public int size = 9;

        protected int maxSlotIndex => size - 1;
    }
}