#if UNITY_EDITOR
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.GameCore
{
    public partial class GameTimeGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "游戏时间";
    }
}
#endif