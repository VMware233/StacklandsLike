#if UNITY_EDITOR
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.Editor
{
    public partial class TextureImporterGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "图片导入" },
            { "en-US", "Texture Importer" }
        };

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.editorCategoryName;
    }
}
#endif