#if UNITY_EDITOR
using System.Collections;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using VMFramework.Core.Linq;

namespace VMFramework.OdinExtensions
{
    [DrawerPriority(DrawerPriorityLevel.WrapperPriority)]
    public class DisallowDuplicateElementsAttributeDrawer : 
        SingleValidationAttributeDrawer<DisallowDuplicateElementsAttribute>
    {
        protected override string GetDefaultMessage(GUIContent label)
        {
            return $"{label}不能包含重复元素";
        }

        protected override bool Validate(object value)
        {
            if (value is not ICollection collection)
            {
                return true;
            }

            if (collection.Cast<object>().ContainsSame())
            {
                return false;
            }

            return true;
        }
    }
}
#endif