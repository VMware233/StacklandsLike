#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.Core.Editor;
using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.UI
{
    [GamePrefabTypeAutoRegister(DEFAULT_ID)]
    public partial class UIToolkitContextMenuUIPreset : IGamePrefabAutoRegisterProvider
    {
        protected const string DEFAULT_ID = "universal_ui_toolkit_context_menu_ui";
        
        protected const string VISUAL_TREE_ASSET_NAME = "Context Menu";
        
        void IGamePrefabAutoRegisterProvider.OnGamePrefabAutoRegister()
        {
            sortingOrder = 80;
            visualTree = VISUAL_TREE_ASSET_NAME.FindAssetOfName<VisualTreeAsset>();
            useDefaultPanelSettings = true;
            ignoreMouseEvents = false;
            defaultPivot = new(0, 1);
            enableOverflow = false;
            autoPivotCorrection = true;
            persistentTracing = false;
            useRightPosition = false;
            useTopPosition = true;
            containerVisualElementName = "ContextMenu";
            contextMenuEntryContainerName = "EntryContainer";
            autoExecuteIfOnlyOneEntry = true;
            clickMouseButtonType = MouseButtonType.LeftButton;
            entrySelectedIcon = ENTRY_SELECTED_ICON_ASSET_NAME.FindAssetOfName<Sprite>();
        }
    }
}
#endif