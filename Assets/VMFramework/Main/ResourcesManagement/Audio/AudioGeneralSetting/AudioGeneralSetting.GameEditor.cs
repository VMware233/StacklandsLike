#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.ResourcesManagement
{
    public partial class AudioGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Audio";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.MusicNoteBeamed;

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.resourcesManagementCategoryName;
    }
}
#endif