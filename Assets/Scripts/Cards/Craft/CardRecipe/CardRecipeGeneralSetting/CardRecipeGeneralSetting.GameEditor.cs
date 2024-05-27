#if UNITY_EDITOR
using StackLandsLike.GameCore;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Cards
{
    public partial class CardRecipeGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "卡牌配方";

        public string folderPath => (GameSetting.cardGeneralSetting as IGameEditorMenuTreeNode).nodePath;
    }
}
#endif