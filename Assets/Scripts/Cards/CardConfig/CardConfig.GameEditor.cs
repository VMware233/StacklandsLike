#if UNITY_EDITOR
using VMFramework.Core.Editor;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace StackLandsLike.Cards
{
    public partial class CardConfig : IGameEditorMenuTreeNode
    {
        Icon IGameEditorMenuTreeNode.icon => model.GetAssetPreview();
    }
}
#endif