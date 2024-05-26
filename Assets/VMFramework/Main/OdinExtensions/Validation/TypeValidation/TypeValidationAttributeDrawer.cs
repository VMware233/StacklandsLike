#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    public class TypeValidationAttributeDrawer :
        MultipleValidationAttributeDrawer<TypeValidationAttribute>
    {
        protected override IEnumerable<ValidationResult> GetValidationResults(object value, GUIContent label)
        {
            if (value is ITypeValidationProvider provider)
            {
                foreach (var result in provider.GetValidationResults(label))
                {
                    yield return result;
                }
            }
        }
    }
}
#endif