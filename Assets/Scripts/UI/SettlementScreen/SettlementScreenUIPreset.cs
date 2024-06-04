using System;
using UnityEngine.UIElements;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed class SettlementScreenUIPreset : UIToolkitPanelPreset
    {
        public const string ID = "settlement_screen_ui";

        public override Type controllerType => typeof(SettlementScreenUIController);

        [VisualElementName(typeof(Label))]
        public string titleLabelName;

        [VisualElementName]
        public string victoryBackgroundName;
        
        [VisualElementName]
        public string defeatBackgroundName;
        
        [VisualElementName(typeof(Label))]
        public string settlementLabelName;
        
        [VisualElementName(typeof(Button))]
        public string restartGameButtonName;
        
        [VisualElementName(typeof(Button))]
        public string exitGameButtonName;
    }
}