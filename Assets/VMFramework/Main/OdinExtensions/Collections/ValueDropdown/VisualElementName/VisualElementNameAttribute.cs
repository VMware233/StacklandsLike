using System;
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.Core;

namespace VMFramework.OdinExtensions
{
    public class VisualElementNameAttribute : GeneralValueDropdownAttribute
    {
        public Type[] VisualElementTypes { get; }

        public VisualElementNameAttribute(params Type[] visualElementTypes)
        {
            VisualElementTypes = visualElementTypes;
            
            Check();
        }

        public VisualElementNameAttribute()
        {
            VisualElementTypes = new[] { typeof(VisualElement) };
            
            Check();
        }

        private void Check()
        {
            foreach (var type in VisualElementTypes)
            {
                if (type.IsDerivedFrom<VisualElement>(true) == false)
                {
                    Debug.LogError($"Type {type.Name} is not a derived class of VisualElement.");
                }
            }
        }
    }
}