#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.UI
{
    public partial class UIPanelGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "UI面板" },
            { "en-US", "UI Panel" }
        };

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.LayoutWtf;
    }
}
#endif