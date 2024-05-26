#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial class RectangleIntegerChooserConfig
    {
        protected override IEnumerable<ValidationResult> GetValidationResults(GUIContent label)
        {
            return this.ValidateRangeChooserConfig(base.GetValidationResults(label));
        }
    }
}
#endif