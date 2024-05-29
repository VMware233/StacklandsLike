#if UNITY_EDITOR
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Tasks
{
    public partial class TaskGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Task";
    }
}
#endif