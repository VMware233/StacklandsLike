#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.UI
{
    public partial class TooltipGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Tooltip";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.CardHeading;

        string IGameEditorMenuTreeNode.folderPath =>
            (GameCoreSetting.uiPanelGeneralSetting as IGameEditorMenuTreeNode)?.nodePath;
    }
}
#endif