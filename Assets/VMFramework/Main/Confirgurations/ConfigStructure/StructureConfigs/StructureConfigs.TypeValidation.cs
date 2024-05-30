#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [TypeValidation]
    public partial class StructureConfigs<TConfig> : ITypeValidationProvider
    {
        protected virtual IEnumerable<ValidationResult> GetValidationResults(GUIContent label)
        {
            var labelName = label?.text;
            
            if (configs.Count == 0)
            {
                yield return new($"{labelName} is lacking any configuration", ValidateType.Info);
            }
            
            foreach (var config in configs)
            {
                if (config == null)
                {
                    yield return new($"{labelName} configs contain null", ValidateType.Error);
                }
            }
        }
        
        IEnumerable<ValidationResult> ITypeValidationProvider.GetValidationResults(GUIContent label)
        {
            return GetValidationResults(label);
        }
    }
}
#endif