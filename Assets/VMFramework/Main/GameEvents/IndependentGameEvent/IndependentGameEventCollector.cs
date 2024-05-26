using System;
using System.Collections.Generic;
using VMFramework.Core;

namespace VMFramework.GameEvents
{
    public static class IndependentGameEventCollector
    {
        public static IEnumerable<Type> Collect()
        {
            foreach (var derivedClass in typeof(IndependentGameEvent<>).GetDerivedClasses(true, true))
            {
                if (derivedClass.IsGenericType)
                {
                    continue;
                }

                if (derivedClass.IsAbstract)
                {
                    continue;
                }

                yield return derivedClass;
            }
        }
    }
}