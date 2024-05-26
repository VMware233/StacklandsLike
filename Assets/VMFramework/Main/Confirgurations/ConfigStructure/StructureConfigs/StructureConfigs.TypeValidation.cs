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
                yield return new($"{labelName}缺少配置", ValidateType.Info);
            }
            
            foreach (var config in configs)
            {
                if (config == null)
                {
                    yield return new($"{labelName}配置中存在Null", ValidateType.Error);
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