#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.GameEvents
{
    public partial class GameEventGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "游戏事件" },
            { "en-US", "Game Event" }
        };

        Icon IGameEditorMenuTreeNode.icon => new(SdfIconType.Dpad);

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.coreCategoryName;
    }
}
#endif