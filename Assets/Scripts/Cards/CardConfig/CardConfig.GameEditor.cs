#if UNITY_EDITOR
using VMFramework.Core.Editor;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace StackLandsLike.Cards
{
    public partial class CardConfig : IGameEditorMenuTreeNode
    {
        Icon IGameEditorMenuTreeNode.icon
        {
            get
            {
                if (icon != null)
                {
                    return icon;
                }

                return model.GetAssetPreview();
            }
        }
    }
}
#endif