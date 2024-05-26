#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.GameEvents
{
    public partial class ColliderMouseEventGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "鼠标事件" },
            { "en-US", "MouseEvent" }
        };

        Icon IGameEditorMenuTreeNode.icon => new(SdfIconType.Mouse2);

        string IGameEditorMenuTreeNode.folderPath =>
            (GameCoreSetting.gameEventGeneralSetting as IGameEditorMenuTreeNode)?.nodePath;
    }
}
#endif