using System;
using Sirenix.OdinInspector;
using TMPro;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public class UGUIPopupTextPreset : UGUIPopupPreset, IPopupTextPreset
    {
        public override Type controllerType => typeof(UGUIPopupTextController);

        [TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        [UGUIName(typeof(TextMeshProUGUI))]
        public string textName;
    }
}
