#if UNITY_EDITOR
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Quests
{
    public partial class QuestGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Quest";
    }
}
#endif