#if UNITY_EDITOR
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.ResourcesManagement
{
    public partial class TrailGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Trail Preset";

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.RESOURCES_MANAGEMENT_CATEGORY;
    }
}
#endif