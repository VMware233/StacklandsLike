using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    public interface ITypeValidationProvider
    {
        public IEnumerable<ValidationResult> GetValidationResults(GUIContent label)
        {
            yield break;
        }
    }
}