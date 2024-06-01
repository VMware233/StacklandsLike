#if UNITY_EDITOR
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.GameCore
{
    public partial class ScoreboardGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Scoreboard";
    }
}
#endif