#if UNITY_EDITOR
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    public partial class GamePrefabWrapperGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Game Prefab Wrapper";

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.coreCategoryName;
    }
}
#endif