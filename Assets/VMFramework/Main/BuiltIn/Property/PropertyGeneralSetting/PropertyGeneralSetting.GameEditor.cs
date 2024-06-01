#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Property
{
    public partial class PropertyGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Property";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.JournalText;

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.BUILT_IN_CATEGORY;
    }
}
#endif