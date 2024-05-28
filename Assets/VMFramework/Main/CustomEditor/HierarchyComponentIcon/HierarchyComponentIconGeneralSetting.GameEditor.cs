#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Editor
{
    public partial class HierarchyComponentIconGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Hierarchy";

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.editorCategoryName;

        Icon IGameEditorMenuTreeNode.icon => new(SdfIconType.BarChartSteps);
    }
}
#endif