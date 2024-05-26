#if UNITY_EDITOR
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.ResourcesManagement
{
    public partial class SpritePreset : IGameEditorMenuTreeNode
    {
        public Icon icon => preloadFlipXPreview;
    }
}
#endif