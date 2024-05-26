#if UNITY_EDITOR
using UnityEngine.UIElements;
using VMFramework.Core.Editor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.UI
{
    [GamePrefabTypeAutoRegister(DEFAULT_ID)]
    public partial class UIToolkitTracingTooltipPreset : IGamePrefabAutoRegisterProvider
    {
        public const string DEFAULT_ID = "universal_ui_toolkit_tooltip_ui";
        
        protected const string VISUAL_TREE_ASSET_NAME = "Tracing Tooltip";
        
        void IGamePrefabAutoRegisterProvider.OnGamePrefabAutoRegister()
        {
            sortingOrder = 90;
            visualTree = VISUAL_TREE_ASSET_NAME.FindAssetOfName<VisualTreeAsset>();
            useDefaultPanelSettings = true;
            ignoreMouseEvents = true;
            defaultPivot = new(0, 1);
            enableOverflow = false;
            autoPivotCorrection = true;
            persistentTracing = true;
            useRightPosition = false;
            useTopPosition = false;
            containerVisualElementName = "Tooltip";
            titleLabelName = "Title";
            descriptionLabelName = "Description";
            propertyContainerName = "PropertyContainer";
        }
    }
}
#endif