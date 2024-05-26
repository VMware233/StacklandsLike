#if UNITY_EDITOR
namespace VMFramework.ResourcesManagement
{
    public partial class TrailGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            container ??= new();
            container.SetDefaultContainerID("TrailContainer");
        }
    }
}
#endif