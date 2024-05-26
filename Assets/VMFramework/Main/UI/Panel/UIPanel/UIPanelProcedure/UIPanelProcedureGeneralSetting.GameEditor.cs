#if UNITY_EDITOR
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.UI
{
    public partial class UIPanelProcedureGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "UI面板流程" },
            { "en-US", "UI Panel Procedure" }
        };

        string IGameEditorMenuTreeNode.folderPath =>
            (GameCoreSetting.uiPanelGeneralSetting as IGameEditorMenuTreeNode)?.nodePath;
    }
}
#endif