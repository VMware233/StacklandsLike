#if UNITY_EDITOR
using VMFramework.Core.Generic;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial class SingleVectorChooserConfig<TVector> : IMinimumValueProvider, IMaximumValueProvider
    {
        void IMinimumValueProvider.ClampByMinimum(double minimum)
        {
            value = value.ClampMin(minimum);
        }

        void IMaximumValueProvider.ClampByMaximum(double maximum)
        {
            value = value.ClampMax(maximum);
        }
    }
}
#endif