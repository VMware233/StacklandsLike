#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    public class IsGameTypeIDAttributeDrawer : MultipleValidationAttributeDrawer<IsGameTypeIDAttribute>
    {
        protected override IEnumerable<ValidationResult> GetValidationResults(object value, GUIContent label)
        {
            if (value is not string id)
            {
                yield break;
            }

            foreach (var result in GameLogicArchitectureAttributeUtility.ValidateID(id))
            {
                yield return result;
            }

            if (id.TrimEnd('_', ' ').EndsWith("_type") == false)
            {
                yield return new()
                {
                    message = "GameType的ID必须以_type结尾",
                    validateType = ValidateType.Warning
                };
            }
        }
    }
}
#endif