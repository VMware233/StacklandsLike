#if UNITY_EDITOR
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Editor.GameEditor
{
    public partial class GameEditorGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Game Editor";

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.EDITOR_CATEGORY;
    }
}
#endif