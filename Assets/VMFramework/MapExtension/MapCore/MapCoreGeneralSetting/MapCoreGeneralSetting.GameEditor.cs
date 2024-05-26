#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Map
{
    public partial class MapCoreGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "en-US", "MapCore" },
            { "zh-CN", "地图核心" }
        };

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.PinMap;

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.builtInCategoryName;
    }
}
#endif