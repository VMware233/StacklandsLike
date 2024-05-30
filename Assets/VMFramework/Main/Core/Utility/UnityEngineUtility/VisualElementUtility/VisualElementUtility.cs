using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace VMFramework.Core
{
    public static partial class VisualElementUtility
    {
        #region Clear All

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearAll<T>(this VisualElement root) where T : VisualElement
        {
            foreach (var child in root.GetAll<T>().ToList())
            {
                child.RemoveFromHierarchy();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearAll<T>(this VisualElement root, Func<T, bool> predicate)
            where T : VisualElement
        {
            foreach (var child in root.GetAll<T>().ToList())
            {
                if (predicate(child))
                {
                    child.RemoveFromHierarchy();
                }
            }
        }

        #endregion

        #region Button

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void QueryAndInitButton(this VisualElement root, string name, Action onClick)
        {
            var button = root.Q<Button>(name);

            if (button != null)
            {
                button.clicked += onClick;
            }
        }

        #endregion

        #region Set Position

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this VisualElement visualElement, Vector2 position,
            bool setRightTopAuto = true)
        {
            visualElement.style.left = position.x;
            visualElement.style.bottom = position.y;

            if (setRightTopAuto)
            {
                visualElement.style.right = StyleKeyword.Auto;
                visualElement.style.top = StyleKeyword.Auto;
                visualElement.style.width = visualElement.resolvedStyle.width;
                visualElement.style.height = visualElement.resolvedStyle.height;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this VisualElement visualElement, Vector2 position, bool useRight,
            bool useTop)
        {
            if (useRight)
            {
                visualElement.style.right = position.x;
            }
            else
            {
                visualElement.style.left = position.x;
            }

            if (useTop)
            {
                visualElement.style.top = position.y;
            }
            else
            {
                visualElement.style.bottom = position.y;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this VisualElement visualElement, Vector2 position, bool useRight,
            bool useTop, Vector2 bounds)
        {
            if (useRight)
            {
                visualElement.style.right = bounds.x - position.x - visualElement.resolvedStyle.width;
            }
            else
            {
                visualElement.style.left = position.x;
            }

            if (useTop)
            {
                visualElement.style.top = bounds.y - position.y - visualElement.resolvedStyle.height;
            }
            else
            {
                visualElement.style.bottom = position.y;
            }
        }

        #endregion
    }
}