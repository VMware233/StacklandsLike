#if UNITY_EDITOR
namespace VMFramework.GameEvents
{
    public partial class BoolInputGameEventConfig
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            actionGroups ??= new();
        }
    }
}
#endif