#if UNITY_EDITOR
using StackLandsLike.GameCore;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.UI
{
    public partial class CardViewGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "卡牌视图";
        
        public string folderPath => (GameSetting.cardGeneralSetting as IGameEditorMenuTreeNode).nodePath;
    }
}
#endif