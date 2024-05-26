#if UNITY_EDITOR
namespace VMFramework.UI
{
    public partial class TooltipGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            tooltipIDBindConfigs ??= new();
            
            tooltipPriorityPresets ??= new();
        }
    }
}
#endif