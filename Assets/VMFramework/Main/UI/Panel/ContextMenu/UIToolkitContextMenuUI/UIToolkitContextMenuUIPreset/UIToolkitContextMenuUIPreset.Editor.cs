#if UNITY_EDITOR
namespace VMFramework.UI
{
    public partial class UIToolkitContextMenuUIPreset
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            gameEventIDsToClose ??= new();
        }
    }
}
#endif