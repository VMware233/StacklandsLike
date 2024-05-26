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
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "拓展瓦片" },
            { "en-US", "Extended Rule Tile" }
        };

        public Icon icon => SdfIconType.Grid3x3;

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.builtInCategoryName;
    }
}
#endif