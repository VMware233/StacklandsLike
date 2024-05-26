#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    public class EnumerableValidationAttributeDrawer : 
        MultipleValidationAttributeDrawer<EnumerableValidationAttribute>
    {
        protected override IEnumerable<ValidationResult> GetValidationResults(object value, GUIContent label)
        {
            if (value is IEnumerable enumerable)
            {
                foreach (var obj in enumerable)
                {
                    if (obj is ITypeValidationProvider provider)
                    {
                        foreach (var result in provider.GetValidationResults(label))
                        {
                            yield return result;
                        }
                    }
                }
            }
        }
    }
}
#endif