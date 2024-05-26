#if UNITY_EDITOR
namespace VMFramework.UI
{
    public partial class ContextMenuGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            contextMenuIDBindConfigs ??= new();
        }
    }
}
#endif