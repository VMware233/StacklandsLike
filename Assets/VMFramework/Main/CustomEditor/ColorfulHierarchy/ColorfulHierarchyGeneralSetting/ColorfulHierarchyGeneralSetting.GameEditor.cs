#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Editor
{
    public partial class ColorfulHierarchyGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "彩色层级" },
            { "en-US", "Colorful Hierarchy" }
        };

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.editorCategoryName;

        Icon IGameEditorMenuTreeNode.icon => new(SdfIconType.FileEarmarkRichtextFill);
    }
}
#endif