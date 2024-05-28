using System;
using TMPro;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed class HUDPreset : UGUIPanelPreset
    {
        public const string ID = "hud_ui";
        
        public override Type controllerType => typeof(HUDController);
        
        [UGUIName(typeof(TextMeshProUGUI))]
        public string tickInDayLabel;
    }
}