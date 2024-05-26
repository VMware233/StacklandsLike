#if UNITY_EDITOR
namespace VMFramework.UI
{
    public partial class UGUIPopupPreset
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            containerAnimation ??= new();
            startContainerAnimation ??= new();
            endContainerAnimation ??= new();
        }
    }
}
#endif