#if UNITY_EDITOR
namespace VMFramework.UI
{
    public partial class InfoDebugEntry
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            content ??= new();
        }
    }
}
#endif