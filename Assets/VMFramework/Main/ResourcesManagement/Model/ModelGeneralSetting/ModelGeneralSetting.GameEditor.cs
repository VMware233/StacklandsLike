#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.ResourcesManagement
{
    public partial class ModelGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Model Prefab";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.BoxSeam;

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.RESOURCES_MANAGEMENT_CATEGORY;
    }
}
#endif