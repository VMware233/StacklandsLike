using System.Runtime.CompilerServices;
using VMFramework.GameEvents;

namespace VMFramework.UI
{
    public partial class UIPanelControllerUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Open(this IUIPanelController controller, BoolInputGameEvent boolInputGameEvent)
        {
            controller.Open(null);
        }
    }
}