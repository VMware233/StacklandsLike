#if UNITY_EDITOR
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Cards
{
    public partial class CardGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "卡牌";
    }
}
#endif