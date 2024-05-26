#if UNITY_EDITOR
namespace VMFramework.Configuration
{
    public partial class GeneralChooserConfig<T>
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            singleValueChooserConfig ??= new();
            weightedSelectChooserConfig ??= new();
            circularSelectChooserConfig ??= new();
        }
    }
}
#endif