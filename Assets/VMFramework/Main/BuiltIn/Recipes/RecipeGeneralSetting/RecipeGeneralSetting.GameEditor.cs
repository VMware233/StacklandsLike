#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Recipe
{
    public partial class RecipeGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Recipe";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.Grid3x3GapFill;

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.builtInCategoryName;
    }
}
#endif