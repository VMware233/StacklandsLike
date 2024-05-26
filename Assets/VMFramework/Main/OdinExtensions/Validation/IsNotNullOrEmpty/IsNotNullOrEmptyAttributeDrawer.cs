#if UNITY_EDITOR
using System.Collections;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.OdinExtensions
{
    [DrawerPriority(DrawerPriorityLevel.WrapperPriority)]
    public sealed class IsNotNullOrEmptyAttributeDrawer : SingleValidationAttributeDrawer<IsNotNullOrEmptyAttribute>
    {
        protected override bool Validate(object value)
        {
            bool isNullOrEmpty = value is null;

            if (value is string stringValue)
            {
                if (Attribute.Trim)
                {
                    if (stringValue.IsEmptyAfterTrim())
                    {
                        isNullOrEmpty = true;
                    }
                }
                else
                {
                    if (stringValue.IsEmpty())
                    {
                        isNullOrEmpty = true;
                    }
                }
            }
            else if (value is ICollection collectionValue)
            {
                if (collectionValue.Count == 0)
                {
                    isNullOrEmpty = true;
                }
            }
            else if (value is IEmptyCheckable emptyCheckable)
            {
                if (emptyCheckable.IsEmpty())
                {
                    isNullOrEmpty = true;
                }
            }

            return isNullOrEmpty == false;
        }

        protected override string GetDefaultMessage(GUIContent label)
        {
            var propertyName = label == null ? Property.Name : label.text;

            return $"{propertyName}不能为空";
        }
    }
}
#endif