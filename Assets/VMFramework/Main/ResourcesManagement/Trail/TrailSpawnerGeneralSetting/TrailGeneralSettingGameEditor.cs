#if UNITY_EDITOR
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.ResourcesManagement
{
    public partial class TrailGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "拖尾生成器" },
            { "en-US", "Trail Spawner" }
        };

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.resourcesManagementCategoryName;
    }
}
#endif