#if UNITY_EDITOR
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial class RangeFloatConfig : IRangeSliderValueProvider
    {
        float IRangeSliderValueProvider.min
        {
            get => min;
            set => min = value;
        }

        float IRangeSliderValueProvider.max
        {
            get => max;
            set => max = value;
        }
    }
}
#endif