using System;

namespace VMFramework.Configuration
{
    public partial class CircularSelectVectorChooserConfig<TVector> : CircularSelectChooserConfig<TVector>,
        IVectorChooserConfig<TVector>
        where TVector : struct, IEquatable<TVector>
    {
        
    }
}