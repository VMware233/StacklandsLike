#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Core;

namespace VMFramework.OdinExtensions
{
    public static class GameLogicArchitectureAttributeUtility
    {
        public static IEnumerable<ValidationResult> ValidateID(string gamePrefabID)
        {
            if (gamePrefabID.IsNullOrEmpty())
            {
                yield break;
            }
            
            if (gamePrefabID.EndsWith("_"))
            {
                yield return new("ID不推荐以_结尾", ValidateType.Warning);
            }
            
            if (gamePrefabID.Contains(" "))
            {
                yield return new("ID不推荐包含空格", ValidateType.Warning);
            }
            
            if (gamePrefabID.Contains("-"))
            {
                yield return new("ID不推荐包含-符号", ValidateType.Warning);
            }

            if (gamePrefabID.HasUppercaseLetter())
            {
                yield return new("ID不推荐包含大写字母", ValidateType.Warning);
            }
        }
    }
}
#endif