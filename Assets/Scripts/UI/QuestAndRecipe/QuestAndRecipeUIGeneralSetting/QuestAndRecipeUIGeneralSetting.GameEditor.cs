#if UNITY_EDITOR
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.UI
{
    public partial class QuestAndRecipeUIGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "任务和主意UI";

        string IGameEditorMenuTreeNode.folderPath =>
            (GameCoreSetting.uiPanelGeneralSetting as IGameEditorMenuTreeNode).nodePath;
    }
}
#endif