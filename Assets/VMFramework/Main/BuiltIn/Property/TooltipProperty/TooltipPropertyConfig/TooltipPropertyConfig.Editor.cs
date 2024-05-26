#if UNITY_EDITOR
namespace VMFramework.Property
{
    public partial class TooltipPropertyConfig
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();
        
            OnInstanceTypeChangedGUI();
        }
        
        private void OnInstanceTypeChangedGUI()
        {
            tooltipPropertyConfigs ??= new();
        
            foreach (var config in tooltipPropertyConfigs)
            {
                config.filterType = _instanceType;
            }
        }
    }
}
#endif