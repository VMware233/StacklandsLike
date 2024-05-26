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
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "模型预制体" },
            { "en-US", "Model Prefab" }
        };

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.BoxSeam;

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.resourcesManagementCategoryName;
    }
}
#endif