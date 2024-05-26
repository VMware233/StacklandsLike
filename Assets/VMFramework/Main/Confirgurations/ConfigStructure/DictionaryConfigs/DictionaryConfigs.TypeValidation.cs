#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial class DictionaryConfigs<TID, TConfig>
    {
        protected override IEnumerable<ValidationResult> GetValidationResults(GUIContent label)
        {
            foreach (var result in base.GetValidationResults(label))
            {
                yield return result;
            }

            if (configs.Any(config => config.id == null || config.id is ""))
            {
                yield return new("All Configs must have a non-empty ID.", ValidateType.Error);
            }
        }
    }
}
#endif