#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [TypeValidation]
    public partial class ListConfigs<TConfig> : ITypeValidationProvider
    {
        public IEnumerable<ValidationResult> GetValidationResults(GUIContent label)
        {
            if (configs.Count == 0)
            {
                var labelName = label?.text;
                yield return new ($"{labelName}缺少配置", ValidateType.Warning);
            }
        }
    }
}
#endif