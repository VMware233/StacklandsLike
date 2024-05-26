#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core.Linq;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial class CircularSelectChooserConfig<T> : ITypeValidationProvider
    {
        protected virtual IEnumerable<ValidationResult> GetValidationResults(GUIContent label)
        {
            if (items.IsNullOrEmpty())
            {
                yield return new("循环列表不能为空", ValidateType.Error);
                yield break;
            }
                    
            if (startCircularIndex >= items.Count)
            {
                yield return new("起始索引超出循环列表长度", ValidateType.Error);
            }
        }
        
        IEnumerable<ValidationResult> ITypeValidationProvider.GetValidationResults(GUIContent label)
        {
            return GetValidationResults(label);
        }
    }
}
#endif