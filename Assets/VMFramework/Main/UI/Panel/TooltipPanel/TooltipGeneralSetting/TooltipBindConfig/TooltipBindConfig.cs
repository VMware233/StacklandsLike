using VMFramework.Configuration;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public sealed class TooltipBindConfig : GameTypeBasedConfigBase
    {
        [UIPresetID(typeof(ITooltipPreset))]
        [IsNotNullOrEmpty]
        public string tooltipID;
    }
}