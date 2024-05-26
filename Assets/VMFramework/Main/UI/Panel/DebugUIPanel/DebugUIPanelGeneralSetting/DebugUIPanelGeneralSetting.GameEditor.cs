#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.UI
{
    public partial class DebugUIPanelGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "Debug UI面板" },
            { "en-US", "Debug UI Panel" }
        };

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.Bug;

        string IGameEditorMenuTreeNode.folderPath =>
            (GameCoreSetting.uiPanelGeneralSetting as IGameEditorMenuTreeNode)?.nodePath;
    }
}
#endif