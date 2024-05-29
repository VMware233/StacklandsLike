#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.ExtendedTilemap
{
    public partial class ExtendedRuleTileGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Ext Rule Tile";

        public Icon icon => SdfIconType.Grid3x3;

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.BUILT_IN_CATEGORY;
    }
}
#endif