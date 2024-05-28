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
        string INameOwner.name => "Mouse Event";

        Icon IGameEditorMenuTreeNode.icon => new(SdfIconType.Mouse2);

        string IGameEditorMenuTreeNode.folderPath =>
            (GameCoreSetting.gameEventGeneralSetting as IGameEditorMenuTreeNode)?.nodePath;
    }
}
#endif