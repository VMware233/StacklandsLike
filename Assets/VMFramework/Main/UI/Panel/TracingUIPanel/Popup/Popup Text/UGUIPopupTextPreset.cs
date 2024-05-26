using System;
using Sirenix.OdinInspector;

namespace VMFramework.UI
{
    public class UGUIPopupTextPreset : UGUIPopupPreset, IPopupTextPreset
    {
        public override Type controllerType => typeof(UGUIPopupTextController);

        [TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        [ValueDropdown(nameof(GetPrefabChildrenNamesOfTextMeshProUGUI))]
        public string textName;
    }
}
