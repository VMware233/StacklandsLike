using Sirenix.OdinInspector;
using VMFramework.Configuration;

namespace VMFramework.UI
{
    public sealed class TooltipPriorityBindConfig : GameTypeBasedConfigBase
    {
        [PropertyTooltip("优先级")]
        public TooltipPriority priority;
    }
}