#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.UI
{
    public partial class ContextMenuGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "上下文菜单" },
            { "en-US", "Context Menu" }
        };

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.BorderStyle;

        string IGameEditorMenuTreeNode.folderPath =>
            (GameCoreSetting.uiPanelGeneralSetting as IGameEditorMenuTreeNode)?.nodePath;
    }
}
#endif