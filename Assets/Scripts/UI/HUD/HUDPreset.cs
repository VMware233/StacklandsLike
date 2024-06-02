using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed class HUDPreset : UIToolkitPanelPreset
    {
        public const string ID = "hud_ui";
        
        public override Type controllerType => typeof(HUDController);

        [VisualElementName(typeof(Label))]
        public string foodInfoLabelName;

        [VisualElementName(typeof(Label))]
        public string personCountLabelName;

        [VisualElementName]
        public string clockwiseName;
        
        public RangeFloatConfig clockwiseAngleRange = new();
        
        [VisualElementName(typeof(Button))]
        public string pauseButtonName;

        [VisualElementName]
        public string continueIconName;
        
        [VisualElementName]
        public string pauseIconName;
        
        [VisualElementName(typeof(Button))]
        public string speedButtonName;
        
        [VisualElementName]
        public string speed2xIconName;
        
        [VisualElementName]
        public string speed1xIconName;
    }
}