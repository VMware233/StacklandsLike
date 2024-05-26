using System.Collections.Generic;
using VMFramework.Localization;

namespace VMFramework.UI
{
    public class TempTooltipProvider : ITooltipProvider
    {
        private LocalizedStringReference title;
        private LocalizedStringReference description;

        public TempTooltipProvider(LocalizedStringReference title,
            LocalizedStringReference description = null)
        {
            this.title = title;
            this.description = description;
        }

        string ITooltipProvider.GetTooltipTitle() => title;

        IEnumerable<TooltipPropertyInfo> ITooltipProvider.GetTooltipProperties()
        {
            yield break;
        }

        string ITooltipProvider.GetTooltipDescription() => description;
    }
}