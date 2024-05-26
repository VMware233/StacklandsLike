#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    public class IsGamePrefabIDAttributeDrawer : MultipleValidationAttributeDrawer<IsGamePrefabIDAttribute>
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
        }
    }
}
#endif