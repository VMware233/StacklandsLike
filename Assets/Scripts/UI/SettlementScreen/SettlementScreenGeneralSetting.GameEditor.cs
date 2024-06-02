#if UNITY_EDITOR
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.UI
{
    public partial class SettlementScreenGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "结算界面设置";

        string IGameEditorMenuTreeNode.folderPath =>
            (GameCoreSetting.uiPanelGeneralSetting as IGameEditorMenuTreeNode)?.nodePath;
    }
}
#endif