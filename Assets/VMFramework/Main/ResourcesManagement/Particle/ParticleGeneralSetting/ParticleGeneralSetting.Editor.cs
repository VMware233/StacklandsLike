#if UNITY_EDITOR
namespace VMFramework.ResourcesManagement
{
    public partial class ParticleGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            container ??= new();
            container.SetDefaultContainerID("@Particle Container");
        }
    }
}
#endif