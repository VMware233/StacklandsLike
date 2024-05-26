#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public static class RangeChooserConfigTypeValidationUtility
    {
        public static IEnumerable<ValidationResult> ValidateRangeChooserConfig<T>(
            this IRangeChooserConfig<T> config, IEnumerable<ValidationResult> oldValidationResults)
            where T : struct, IEquatable<T>
        {
            foreach (var result in oldValidationResults)
            {
                yield return result;
            }

            if (config.min.Equals(config.max))
            {
                yield return new ValidationResult("最大值与最小值相等，请用单值选择器代替", ValidateType.Warning);
            }
        }
    }
}
#endif