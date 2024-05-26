#if UNITY_EDITOR
using VMFramework.Core.Generic;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial class CircularSelectVectorChooserConfig<TVector>: IMinimumValueProvider, IMaximumValueProvider
    {
        void IMinimumValueProvider.ClampByMinimum(double minimum)
        {
            foreach (var item in items)
            {
                item.value = item.value.ClampMin(minimum);
            }
        }

        void IMaximumValueProvider.ClampByMaximum(double maximum)
        {
            foreach (var item in items)
            {
                item.value = item.value.ClampMax(maximum);
            }
        }
    }
}
#endif