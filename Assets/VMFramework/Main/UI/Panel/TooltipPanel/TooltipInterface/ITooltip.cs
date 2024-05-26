namespace VMFramework.UI
{
    public interface ITooltip : IUIPanelController
    {
        public void Open(ITooltipProvider tooltipProvider, IUIPanelController source, TooltipOpenInfo info);

        public void Close(ITooltipProvider tooltipProvider);
    }
}