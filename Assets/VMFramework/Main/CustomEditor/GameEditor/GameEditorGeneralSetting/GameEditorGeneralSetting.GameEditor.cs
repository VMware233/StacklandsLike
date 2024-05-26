#if UNITY_EDITOR
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Editor.GameEditor
{
    public partial class GameEditorGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "游戏编辑器" },
            { "en-US", "Game Editor" }
        };

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.editorCategoryName;
    }
}
#endif