#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;
using VMFramework.Core.Editor;

namespace VMFramework.OdinExtensions
{
    public abstract class MultipleValidationAttributeDrawer<TAttribute> : OdinAttributeDrawer<TAttribute> 
        where TAttribute : MultipleValidationAttribute
    {
        protected abstract IEnumerable<ValidationResult> GetValidationResults(object value, GUIContent label);

        protected override void DrawPropertyLayout(GUIContent label)
        {
            var value = Property.ValueEntry.WeakSmartValue;

            ValidateType validateType = ValidateType.Info;
            foreach (var validationResult in GetValidationResults(value, label))
            {
                SirenixEditorGUI.MessageBox(validationResult.message,
                    validationResult.validateType.ToMessageType());

                if (validationResult.validateType == ValidateType.Error)
                {
                    validateType = ValidateType.Error;
                }
                else if (validationResult.validateType == ValidateType.Warning)
                {
                    if (validateType == ValidateType.Info)
                    {
                        validateType = ValidateType.Warning;
                    }
                }
            }

            CallNextDrawer(label);

            if (validateType != ValidateType.Info)
            {
                Rect rect = GUILayoutUtility.GetLastRect();

                if (Attribute.DrawCurrentRect)
                {
                    rect = GUIHelper.GetCurrentLayoutRect();
                }

                if (validateType == ValidateType.Error)
                {
                    rect.DrawErrorRect();
                }
                else if (validateType == ValidateType.Warning)
                {
                    rect.DrawWarningRect();
                }
            }
        }
    }
}
#endif