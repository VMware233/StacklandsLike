using System;
using Sirenix.OdinInspector;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public class UGUIPopupProgressPreset : UGUIPopupPreset
    {
        public override Type controllerType => typeof(UGUIPopupProgressController);

        [LabelText("进度条名称"), TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        [ValueDropdown(nameof(GetPrefabChildrenNames))]
        [IsNotNullOrEmpty]
        public string progressName;
    }
}
