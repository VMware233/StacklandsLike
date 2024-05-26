#if UNITY_EDITOR
namespace VMFramework.Property
{
    public partial class TooltipPropertyGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            tooltipPropertyConfigs ??= new();
        }
    }
}
#endif