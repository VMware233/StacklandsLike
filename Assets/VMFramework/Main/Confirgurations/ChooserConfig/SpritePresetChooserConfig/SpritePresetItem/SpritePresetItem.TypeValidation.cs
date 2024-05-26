#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [TypeValidation]
    public partial class SpritePresetItem : ITypeValidationProvider
    {
        public IEnumerable<ValidationResult> GetValidationResults(GUIContent label)
        {
            if (spritePresetID.IsNullOrEmpty())
            {
                yield break;
            }

            if (GamePrefabManager.ContainsGamePrefab(spritePresetID) == false)
            {
                yield return new($"Sprite Preset ID : {spritePresetID} does not exist!", ValidateType.Error);
            }
        }
    }
}
#endif