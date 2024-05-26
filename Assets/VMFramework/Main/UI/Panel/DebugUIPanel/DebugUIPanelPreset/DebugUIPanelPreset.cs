using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine.Localization;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public sealed partial class DebugUIPanelPreset : UIToolkitPanelPreset
    {
        public const string DEBUGGING_UI_SETTING_CATEGORY = "Debug UI";

        public const string DEBUGGING_SCREEN_VISUAL_TREE_ASSET_NAME = "DebugScreen";

        public override Type controllerType => typeof(DebugUIPanelController);

        [LabelText("Left Container Name"), TabGroup(TAB_GROUP_NAME, DEBUGGING_UI_SETTING_CATEGORY)]
        [VisualElementName]
        [IsNotNullOrEmpty]
        [JsonProperty]
        public string leftContainerVisualElementName = LEFT_GROUP_NAME;

        [LabelText("Right Container Name"), TabGroup(TAB_GROUP_NAME, DEBUGGING_UI_SETTING_CATEGORY)]
        [VisualElementName]
        [IsNotNullOrEmpty]
        [JsonProperty]
        public string rightContainerVisualElementName = RIGHT_GROUP_NAME;

        public override void CheckSettings()
        {
            base.CheckSettings();

            leftContainerVisualElementName.AssertIsNotNullOrEmpty(nameof(leftContainerVisualElementName));

            rightContainerVisualElementName.AssertIsNotNullOrEmpty(nameof(rightContainerVisualElementName));
        }
    }
}