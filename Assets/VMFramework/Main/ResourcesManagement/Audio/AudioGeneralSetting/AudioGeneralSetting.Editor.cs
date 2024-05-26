#if UNITY_EDITOR
namespace VMFramework.ResourcesManagement
{
    public partial class AudioGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            container ??= new();
            container.SetDefaultContainerID("#Audio Container");
        }
    }
}
#endif