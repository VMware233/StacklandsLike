#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core.Linq;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [TypeValidation]
    public partial class WeightedSelectChooserConfig<T> : ITypeValidationProvider
    {
        protected virtual IEnumerable<ValidationResult> GetValidationResults(GUIContent label)
        {
            if (items.IsNullOrEmpty())
            {
                yield return new("概率权值列表不能为空", ValidateType.Error);
                yield break;
            }

            if (IsItemsContainsSameValue())
            {
                yield return new("概率权值列表中存在相同的值", ValidateType.Warning);
            }

            if (IsRatiosCoprime() == false)
            {
                yield return new("概率权值列表中的占比不是互质，可以化简", ValidateType.Warning);
            }

            if (IsRatiosAllZero())
            {
                yield return new("占比不能全为0", ValidateType.Error);
            }

            if (items.Count == 1)
            {
                yield return new("概率权值列表中只有一个选项，请用单值选择器代替", ValidateType.Warning);
            }
        }

        IEnumerable<ValidationResult> ITypeValidationProvider.GetValidationResults(GUIContent label)
        {
            return GetValidationResults(label);
        }
    }
}
#endif