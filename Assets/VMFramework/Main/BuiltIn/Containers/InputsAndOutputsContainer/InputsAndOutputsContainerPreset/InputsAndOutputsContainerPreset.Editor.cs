#if UNITY_EDITOR
using VMFramework.Configuration;

namespace VMFramework.Containers
{
    public partial class InputsAndOutputsContainerPreset
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            inputsRange ??= new RangeIntegerConfig(1, 12);
            outputsRange ??= new RangeIntegerConfig(13, 16);
        }
    }
}
#endif