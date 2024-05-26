using System;
using UnityEngine.UIElements;

namespace VMFramework.OdinExtensions
{
    public class VisualElementNameAttribute : GeneralValueDropdownAttribute
    {
        public Type[] VisualElementTypes { get; }

        public VisualElementNameAttribute(params Type[] visualElementTypes)
        {
            VisualElementTypes = visualElementTypes;
        }

        public VisualElementNameAttribute()
        {
            VisualElementTypes = new[] { typeof(VisualElement) };
        }
    }
}