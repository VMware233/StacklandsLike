#if UNITY_EDITOR
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.GameCore
{
    public partial class GameStateGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Game State";
    }
}
#endif