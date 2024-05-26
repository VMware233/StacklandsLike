using VMFramework.Core;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace VMFramework.UI
{
    public class UGUIPopupTextController : UGUIPopupController, IPopupTextController
    {
        protected UGUIPopupTextPreset uguiPopupTextPreset { get; private set; }

        [ShowInInspector]
        protected TextMeshProUGUI textUGUI;

        public string text
        {
            get => textUGUI.text;
            set => textUGUI.text = value;
        }

        public Color textColor
        {
            get => textUGUI.color;
            set => textUGUI.color = value;
        }

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            uguiPopupTextPreset = preset as UGUIPopupTextPreset;

            uguiPopupTextPreset.AssertIsNotNull(nameof(uguiPopupTextPreset));

            textUGUI = visualObject.transform.QueryFirstComponentInChildren<TextMeshProUGUI>(
                uguiPopupTextPreset.textName, true);

            textUGUI.AssertIsNotNull(nameof(textUGUI));
        }
    }
}
