using System;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed class MainMenuUIPreset : UIToolkitPanelPreset
    {
        public const string ID = "main_menu_ui";
        
        public override Type controllerType => typeof(MainMenuUIController);

        [TabGroup(TAB_GROUP_NAME, UI_TOOLKIT_PANEL_CATEGORY)]
        [VisualElementName(typeof(Button))]
        public string startGameButtonName;
    }
}