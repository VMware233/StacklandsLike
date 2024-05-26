using System;

namespace VMFramework.Configuration
{
    public partial class WeightedSelectVectorChooserConfig<TVector> : WeightedSelectChooserConfig<TVector>,
        IVectorChooserConfig<TVector>
        where TVector : struct, IEquatable<TVector>
    {
        
    }
}