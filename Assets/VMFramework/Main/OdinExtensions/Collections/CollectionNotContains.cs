using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using VMFramework.Core;
using UnityEngine;

#if UNITY_EDITOR

using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;

#endif

namespace VMFramework.OdinExtensions
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = true)]
    public class CollectionNotContains : SingleValidationAttribute
    {
        public object Content;

        public string ContentGetter;

        public CollectionNotContains(string contentGetter)
        {
            ContentGetter = contentGetter;
        }

        public CollectionNotContains(object content)
        {
            Content = content;
        }
    }

#if UNITY_EDITOR

    public class CollectionNotContainsAttributeDrawer : OdinAttributeDrawer<CollectionNotContains>
    {
        private ValueResolver contentResolver;

        public override bool CanDrawTypeFilter(Type type)
        {
            return typeof(ICollection).IsAssignableFrom(type);
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            var value = Property.ValueEntry.WeakSmartValue;

            if (value == null)
            {
                CallNextDrawer(label);
                return;
            }

            if (value.GetType().IsGenericType &&
                value.GetType().IsDerivedFrom(typeof(ICollection<>), false, 
                    true))
            {
                Type itemType = value.GetType().GetGenericArguments()[0];

                contentResolver ??=
                    ValueResolver.Get(itemType, Property, Attribute.ContentGetter);

                if (contentResolver.HasError)
                {
                    SirenixEditorGUI.ErrorMessageBox(contentResolver.ErrorMessage);
                }
                else
                {
                    MethodInfo containsMethod =
                        value.GetType().GetMethod("Contains");
                    if (containsMethod != null)
                    {
                        object itemToCheck = contentResolver.GetWeakValue();
                        var containsItem =
                            (bool)containsMethod.Invoke(value,
                                new[] { itemToCheck });

                        if (containsItem)
                        {
                            SirenixEditorGUI.ErrorMessageBox(
                                $"{label}不应包含{itemToCheck}");
                        }
                    }
                }
            }

            CallNextDrawer(label);
        }
    }

#endif
}
