#if UNITY_EDITOR
using EnumsNET;
using VMFramework.Core;

namespace VMFramework.UI
{
    public partial class DebugEntry
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            if (position.GetFlagCount() == 0 || position.GetFlagCount() > 1)
            {
                position = LeftRightDirection.Right;
            }
        }
    }
}
#endif