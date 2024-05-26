using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial class RangeIntegerConfig : IRangeSliderValueProvider
    {
        float IRangeSliderValueProvider.min
        {
            get => min;
            set => min = value.Round();
        }

        float IRangeSliderValueProvider.max
        {
            get => max;
            set => max = value.Round();
        }
    }
}