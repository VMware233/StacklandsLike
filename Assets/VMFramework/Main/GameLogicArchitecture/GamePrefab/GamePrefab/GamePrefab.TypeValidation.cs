#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    [TypeValidation(DrawCurrentRect = true)]
    public partial class GamePrefab : ITypeValidationProvider
    {
        public IEnumerable<ValidationResult> GetValidationResults(GUIContent label)
        {
            if (isIDStartsWithPrefix == false)
            {
                yield return new($"ID should start with prefix : {idPrefix}", ValidateType.Warning);
            }

            if (isIDEndsWithSuffix == false)
            {
                yield return new($"ID should end with suffix : {idSuffix}", ValidateType.Warning);
            }
        }
    }
}
#endif