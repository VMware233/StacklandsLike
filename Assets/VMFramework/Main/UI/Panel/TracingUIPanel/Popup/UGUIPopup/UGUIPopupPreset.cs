using System;
using Sirenix.OdinInspector;
using VMFramework.Configuration.Animation;

namespace VMFramework.UI
{
    public partial class UGUIPopupPreset : UGUITracingUIPanelPreset, IPopupPreset
    {
        protected const string POPUP_SETTING_CATEGORY = "Popup";

        public override Type controllerType => typeof(UGUIPopupController);

        [TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        [ValueDropdown(nameof(GetPrefabChildrenNames))]
        public string popupContainerName;

        [TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        public bool enableContainerAnimation = false;

        [TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        [EnableIf(nameof(enableContainerAnimation))]
        [Indent]
        public bool splitContainerAnimation = false;

        [TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        [EnableIf(nameof(enableContainerAnimation))]
        [HideIf(nameof(splitContainerAnimation))]
        [Indent]
        public GameObjectAnimation containerAnimation = new();

        [TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        [EnableIf(nameof(enableContainerAnimation))]
        [HideIf(nameof(splitContainerAnimation))]
        [Indent]
        public bool autoCloseAfterContainerAnimation = true;

        [TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        [EnableIf(nameof(enableContainerAnimation))]
        [ShowIf(nameof(splitContainerAnimation))]
        [Indent]
        public GameObjectAnimation startContainerAnimation = new();

        [TabGroup(TAB_GROUP_NAME, POPUP_SETTING_CATEGORY)]
        [EnableIf(nameof(enableContainerAnimation))]
        [ShowIf(nameof(splitContainerAnimation))]
        [Indent]
        public GameObjectAnimation endContainerAnimation = new();

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            containerAnimation.CheckSettings();
            startContainerAnimation.CheckSettings();
            endContainerAnimation.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();

            containerAnimation.Init();
            startContainerAnimation.Init();
            endContainerAnimation.Init();
        }
    }
}
