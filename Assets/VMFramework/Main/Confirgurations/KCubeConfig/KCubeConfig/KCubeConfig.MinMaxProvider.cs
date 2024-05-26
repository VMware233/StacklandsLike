#if UNITY_EDITOR
using VMFramework.Core.Generic;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial class KCubeConfig<TPoint> : IMinimumValueProvider, IMaximumValueProvider
    {
        void IMinimumValueProvider.ClampByMinimum(double minimum)
        {
            min = min.ClampMin(minimum);
            max = max.ClampMin(minimum);
        }

        void IMaximumValueProvider.ClampByMaximum(double maximum)
        {
            min = min.ClampMax(maximum);
            max = max.ClampMax(maximum);
        }
    }
}
#endif