using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace VMFramework.Core
{
    public partial class VisualElementUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VisualElement SetAsFirstSibling(this VisualElement element)
        {
            if (element.parent == null)
            {
                return null;
            }

            element.PlaceInFront(element.parent[0]);
            return element;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VisualElement SetAsLastSibling(this VisualElement element)
        {
            if (element.parent == null)
            {
                return null;
            }

            element.PlaceInFront(element.parent[element.parent.childCount - 1]);
            return element;
        }
    }
}