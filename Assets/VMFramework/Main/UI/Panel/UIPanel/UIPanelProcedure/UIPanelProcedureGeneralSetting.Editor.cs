#if UNITY_EDITOR
namespace VMFramework.UI
{
    public partial class UIPanelProcedureGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            procedureConfigs ??= new();
        }
    }
}
#endif