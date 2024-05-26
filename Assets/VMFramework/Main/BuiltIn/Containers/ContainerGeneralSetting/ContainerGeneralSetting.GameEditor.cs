#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Containers
{
    public partial class ContainerGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "容器" },
            { "en-US", "Container" }
        };

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.Archive;

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.builtInCategoryName;
    }
}
#endif