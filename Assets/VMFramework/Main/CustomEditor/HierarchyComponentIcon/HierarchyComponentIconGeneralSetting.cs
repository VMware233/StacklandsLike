#if UNITY_EDITOR
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    public sealed partial class HierarchyComponentIconGeneralSetting : GeneralSetting
    {
        [LabelText("最大图标数量")]
        [PropertyRange(1, 10)]
        [JsonProperty]
        public int maxIconNum = 5;

        [LabelText("图标大小")]
        [PropertyRange(1, 24)]
        [JsonProperty]
        public int iconSize = 16;
    }
}

#endif