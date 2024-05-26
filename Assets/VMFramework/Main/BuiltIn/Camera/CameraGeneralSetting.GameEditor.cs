#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework
{
    public partial class CameraGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => new LocalizedTempString()
        {
            { "zh-CN", "相机" },
            { "en-US", "Camera" }
        };

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.Camera;

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.builtInCategoryName;
    }
}
#endif