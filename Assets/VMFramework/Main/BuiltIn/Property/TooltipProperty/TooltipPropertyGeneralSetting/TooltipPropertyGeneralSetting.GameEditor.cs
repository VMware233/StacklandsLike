#if UNITY_EDITOR
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Property
{
    public partial class TooltipPropertyGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Property Tooltip";

        string IGameEditorMenuTreeNode.folderPath =>
            (GameCoreSetting.propertyGeneralSetting as IGameEditorMenuTreeNode)?.nodePath;
    }
}
#endif