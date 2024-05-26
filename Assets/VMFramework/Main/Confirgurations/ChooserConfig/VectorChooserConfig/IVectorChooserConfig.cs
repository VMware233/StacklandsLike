using System;

namespace VMFramework.Configuration
{
    public interface IVectorChooserConfig<TVector> : IChooserConfig<TVector>
        where TVector : struct, IEquatable<TVector>
    {
        
    }
}