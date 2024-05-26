#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.OdinExtensions
{
    public sealed class TooltipPriorityPresetIDAttributeDrawer
        : GeneralValueDropdownAttributeDrawer<TooltipPriorityPresetIDAttribute>
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            return GameCoreSetting.tooltipGeneralSetting.tooltipPriorityPresets.GetNameList();
        }
    }
}
#endif