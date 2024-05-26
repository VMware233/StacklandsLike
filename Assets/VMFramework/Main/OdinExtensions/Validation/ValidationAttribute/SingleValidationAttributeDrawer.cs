#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Editor;

namespace VMFramework.OdinExtensions
{
    [DrawerPriority(DrawerPriorityLevel.WrapperPriority)]
    public abstract class SingleValidationAttributeDrawer<TAttribute> : OdinAttributeDrawer<TAttribute> 
        where TAttribute : SingleValidationAttribute
    {
        private ValueResolver<string> errorMessageGetter;

        protected override void Initialize()
        {
            if (Attribute.Message.IsNullOrEmpty() == false)
            {
                errorMessageGetter =
                    ValueResolver.GetForString(Property, Attribute.Message);
            }
        }

        protected abstract bool Validate(object value);

        protected abstract string GetDefaultMessage(GUIContent label);

        protected override void DrawPropertyLayout(GUIContent label)
        {
            string errorMessage;
            if (errorMessageGetter != null)
            {
                if (errorMessageGetter.HasError)
                {
                    SirenixEditorGUI.ErrorMessageBox(errorMessageGetter
                        .ErrorMessage);
                    CallNextDrawer(label);
                    return;
                }

                errorMessage = errorMessageGetter.GetValue();
            }
            else
            {
                errorMessage = GetDefaultMessage(label);
            }

            var value = Property.ValueEntry.WeakSmartValue;

            bool isValid = Validate(value);

            if (isValid == false)
            {
                SirenixEditorGUI.MessageBox(errorMessage, Attribute.ValidateType.ToMessageType());
            }

            CallNextDrawer(label);

            if (isValid == false)
            {
                Rect rect = GUILayoutUtility.GetLastRect();

                if (Attribute.DrawCurrentRect)
                {
                    rect = GUIHelper.GetCurrentLayoutRect();
                }

                if (Attribute.ValidateType == ValidateType.Error)
                {
                    rect.DrawErrorRect();
                }
                else if (Attribute.ValidateType == ValidateType.Warning)
                {
                    rect.DrawWarningRect();
                }
            }
        }
    }
}
#endif