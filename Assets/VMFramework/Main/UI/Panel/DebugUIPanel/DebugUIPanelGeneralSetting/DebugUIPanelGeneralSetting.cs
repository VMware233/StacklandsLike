using System;
using VMFramework.GameLogicArchitecture;
using Newtonsoft.Json;
using Sirenix.OdinInspector;

namespace VMFramework.UI
{
    public sealed partial class DebugUIPanelGeneralSetting : GamePrefabGeneralSetting
    {
        #region Setting MetaData

        public override Type baseGamePrefabType => typeof(DebugEntry);

        #endregion

        [LabelText("更新间隔"), SuffixLabel("秒")]
        [JsonProperty]
        [PropertyRange(0.1f, 1f)]
        public float updateInterval = 0.2f;
    }
}
