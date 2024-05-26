#if UNITY_EDITOR

using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    public sealed partial class ColorfulHierarchyGeneralSetting : GeneralSetting
    {
        private const string HIERARCHY_CATEGORY = "层级设置";
        
        [LabelText("颜色预设"), TabGroup(TAB_GROUP_NAME, HIERARCHY_CATEGORY)]
        [JsonProperty]
        public List<HierarchyColorPreset> colorPresets = new();
    }
}

#endif