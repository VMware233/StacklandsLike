#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.OdinExtensions
{
    [DrawerPriority(DrawerPriorityLevel.WrapperPriority)]
    public class RequiredComponentAttributeDrawer : 
        SingleValidationAttributeDrawer<RequiredComponentAttribute>
    {
        private ValueResolver<Type> componentTypeGetter;
        private ValueResolver<string> errorMessageGetter;

        protected override void Initialize()
        {
            if (Attribute.Message.IsNullOrEmpty() == false)
            {
                errorMessageGetter =
                    ValueResolver.GetForString(Property, Attribute.Message);
            }

            if (Attribute.ComponentTypeGetter.IsNullOrEmpty() == false)
            {
                componentTypeGetter =
                    ValueResolver.Get<Type>(Property, Attribute.ComponentTypeGetter);
            }
        }

        protected override string GetDefaultMessage(GUIContent label)
        {
            var componentType =
                Attribute.ComponentType ?? componentTypeGetter?.GetValue();

            if (componentType == null)
            {
                return string.Empty;
            }

            var name = componentType.Name;

            return errorMessageGetter == null
                ? $"{name} is Required"
                : errorMessageGetter.ErrorMessage;
        }

        protected override bool Validate(object value)
        {
            var componentType =
                Attribute.ComponentType ?? componentTypeGetter?.GetValue();

            if (componentType == null)
            {
                return true;
            }

            var gameObject = value as GameObject;

            if (gameObject == null)
            {
                return false;
            }

            if (gameObject.HasComponent(componentType) == false)
            {
                return false;
            }

            return true;
        }
    }
}
#endif