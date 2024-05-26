using VMFramework.GameLogicArchitecture;
using VMFramework.Core;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.UI
{
    public abstract partial class DebugEntry : LocalizedGamePrefab, IDebugEntry
    {
        protected override string idSuffix => "debug_entry";

        [LabelText("位置"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [EnumToggleButtons]
        [SerializeField, JsonProperty]
        protected LeftRightDirection position = LeftRightDirection.Right;

        public abstract string GetText();

        public virtual bool ShouldDisplay() => true;
        
        LeftRightDirection IDebugEntry.position => position;

        public override void CheckSettings()
        {
            base.CheckSettings();

            position.AssertIsSingleFlag(nameof(position));
        }
    }
}