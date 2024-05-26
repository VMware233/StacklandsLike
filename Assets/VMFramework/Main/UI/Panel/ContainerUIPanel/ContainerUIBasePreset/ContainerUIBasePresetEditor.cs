#if UNITY_EDITOR
namespace VMFramework.UI
{
    public partial class ContainerUIBasePreset
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            customSlotSourceContainerNames ??= new();

            autoFillContainerConfigs ??= new();

            foreach (var autoFillContainerConfig in autoFillContainerConfigs)
            {
                autoFillContainerConfig.preset = this;
            }
        }

        private AutoFillContainerConfig AddAutoFillContainerConfigGUI()
        {
            return new AutoFillContainerConfig()
            {
                preset = this,
            };
        }
    }
}
#endif