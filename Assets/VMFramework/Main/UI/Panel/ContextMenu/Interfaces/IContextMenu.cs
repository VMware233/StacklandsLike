namespace VMFramework.UI
{
    public interface IContextMenu : IUIPanelController
    {
        public void Open(IContextMenuProvider contextMenuProvider, IUIPanelController source);

        public void Close(IContextMenuProvider contextMenuProvider);
    }
}