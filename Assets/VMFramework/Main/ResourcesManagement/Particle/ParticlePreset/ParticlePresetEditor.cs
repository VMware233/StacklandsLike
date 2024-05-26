using VMFramework.Configuration;

#if UNITY_EDITOR
namespace VMFramework.ResourcesManagement
{
    public partial class ParticlePreset
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            duration ??= new SingleValueChooserConfig<float>();
        }
    }
}
#endif