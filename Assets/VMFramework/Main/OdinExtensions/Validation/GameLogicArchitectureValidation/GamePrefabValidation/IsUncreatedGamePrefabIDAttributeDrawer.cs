#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.OdinExtensions
{
    public class IsUncreatedGamePrefabIDAttributeDrawer : 
        MultipleValidationAttributeDrawer<IsUncreatedGamePrefabIDAttribute>
    {
        protected override IEnumerable<ValidationResult> GetValidationResults(object value, GUIContent label)
        {
            if (value is not string id)
            {
                yield break;
            }

            if (id.IsNullOrEmptyAfterTrim())
            {
                yield break;
            }

            if (GamePrefabManager.ContainsGamePrefab(id))
            {
                yield return new("ID已经被占用，请更换ID", ValidateType.Error);
            }

            foreach (var result in GameLogicArchitectureAttributeUtility.ValidateID(id))
            {
                yield return result;
            }
        }
    }
}
#endif