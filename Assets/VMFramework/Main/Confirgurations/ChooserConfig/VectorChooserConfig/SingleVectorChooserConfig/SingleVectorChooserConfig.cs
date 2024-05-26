using System;

namespace VMFramework.Configuration
{
    public partial class SingleVectorChooserConfig<TVector>
        : SingleValueChooserConfig<TVector>, IVectorChooserConfig<TVector>
        where TVector : struct, IEquatable<TVector>
    {
        public SingleVectorChooserConfig()
        {

        }

        public SingleVectorChooserConfig(TVector defaultValue)
        {
            value = defaultValue;
        }
    }
}