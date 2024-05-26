#if UNITY_EDITOR
namespace VMFramework.GameEvents
{
    public partial class KeyCodeTranslation
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            translation ??= new();
        }
    }
}
#endif