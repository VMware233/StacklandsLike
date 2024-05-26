#if UNITY_EDITOR
namespace VMFramework.Configuration
{
    public partial class StructureConfigs<TConfig>
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            configs ??= new();
        }
    }
}
#endif