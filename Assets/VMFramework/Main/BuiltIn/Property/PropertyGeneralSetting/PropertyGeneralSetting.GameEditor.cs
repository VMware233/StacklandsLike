#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Property
{
    public partial class PropertyGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "属性" },
            { "en-US", "Property" }
        };

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.JournalText;

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.builtInCategoryName;
    }
}
#endif