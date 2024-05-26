using System;
using System.Collections.Generic;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public interface ISingleRangeChooserConfig<TVector> : IRangeChooserConfig<TVector>, ISingleChooserConfig<TVector>, 
        IKCubeConfig<TVector>
        where TVector : struct, IEquatable<TVector>
    {
        IChooser<TVector> IChooserConfig<TVector>.GenerateNewObjectChooser()
        {
            return new RangeChooser<TVector>(this);
        }

        IEnumerable<TVector> IChooserConfig<TVector>.GetAvailableValues()
        {
            yield return min;
            yield return max;
        }

        void IChooserConfig<TVector>.SetAvailableValues(Func<TVector, TVector> setter)
        {
            min = setter(min);
            max = setter(max);
        }
    }
}