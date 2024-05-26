using System;
using System.Diagnostics;
using VMFramework.Core;
using UnityEngine;

#if UNITY_EDITOR

using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;

#endif

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    [Conditional("UNITY_EDITOR")]
    public class ValidateIsNotAttribute : SingleValidationAttribute
    {
        public object Content;

        public string ContentGetter;

        public string ContentName;

        public ValidateIsNotAttribute(string contentGetter, string contentName = null) : base()
        {
            ContentGetter = contentGetter;
            ContentName = contentName;
        }

        public ValidateIsNotAttribute(object content, string contentName = null) : base()
        {
            Content = content;
            ContentName = contentName;
        }
    }

#if UNITY_EDITOR

    [DrawerPriority(DrawerPriorityLevel.WrapperPriority)]
    public class ValidateIsNotAttributeDrawer : SingleValidationAttributeDrawer<ValidateIsNotAttribute>
    {
        private ValueResolver contentResolver;

        private object content;

        protected override bool Validate(object value)
        {
            if (value == null)
            {
                return content != null;
            }
            
            return value.Equals(content) == false;
        }

        protected override string GetDefaultMessage(GUIContent label)
        {
            var propertyName = label == null ? Property.Name : label.text;
            var contentName = Attribute.ContentName.IsNullOrEmpty()
                ? content?.ToString()
                : Attribute.ContentName;
            return $"{propertyName}不要是{contentName}";
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (Attribute.ContentGetter.IsNullOrEmpty() == false)
            {
                contentResolver ??=
                    ValueResolver.Get(Property.ValueEntry.TypeOfValue, Property,
                        Attribute.ContentGetter);

                if (contentResolver.HasError)
                {
                    SirenixEditorGUI.ErrorMessageBox(contentResolver.ErrorMessage);
                    CallNextDrawer(label);
                    return;
                }

                content = contentResolver.GetWeakValue();
            }
            else
            {
                content = Attribute.Content;
            }

            base.DrawPropertyLayout(label);
        }
    }

#endif
}
