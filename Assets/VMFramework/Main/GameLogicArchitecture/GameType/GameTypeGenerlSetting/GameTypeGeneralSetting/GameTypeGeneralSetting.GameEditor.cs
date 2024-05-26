#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameTypeGeneralSetting : IGameEditorMenuTreeNode
    { 
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "游戏种类" },
            { "en-US", "Game Type" }
        };

        Icon IGameEditorMenuTreeNode.icon => new(SdfIconType.Collection);

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.coreCategoryName;
    }
}
#endif