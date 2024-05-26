using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.Core.Linq;

namespace VMFramework.Core
{
    public static class VisualElementUtility
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

        #region Get Children

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetChildren<T>(this VisualElement root) where T : VisualElement
        {
            foreach (var child in root.Children())
            {
                if (child is T typedChild)
                {
                    yield return typedChild;
                }
            }
        }

        #endregion

        #region Get All

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<VisualElement> GetAll(this VisualElement root)
        {
            return root.PreorderTraverse(true, element => element.Children());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<VisualElement> GetAll(this IEnumerable<VisualElement> roots)
        {
            foreach (var root in roots)
            {
                foreach (var element in root.GetAll())
                {
                    yield return element;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetAll<T>(this VisualElement root) where T : VisualElement
        {
            return root.PreorderTraverse<VisualElement, T>(true, element => element.Children());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetAll<T>(this IEnumerable<VisualElement> roots) where T : VisualElement
        {
            foreach (var root in roots)
            {
                foreach (var element in root.GetAll<T>())
                {
                    yield return element;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<VisualElement> GetAllByType(this VisualElement root, Type type)
        {
            return root.PreorderTraverse(true, element => element.Children())
                .Where(element => element.GetType().IsDerivedFrom(type, true));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<VisualElement> GetAllByTypes(this VisualElement root, params Type[] types)
        {
            return root.PreorderTraverse(true, element => element.Children())
                .Where(element => element.GetType().IsDerivedFromAny(types, true));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetAll<T>(this VisualTreeAsset treeAsset) where T : VisualElement
        {
            if (treeAsset == null)
            {
                return Enumerable.Empty<T>();
            }

            return treeAsset.CloneTree().contentContainer.GetAll<T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<VisualElement> GetAllByType(this VisualTreeAsset treeAsset, Type type)
        {
            if (treeAsset == null)
            {
                return Enumerable.Empty<VisualElement>();
            }

            return treeAsset.CloneTree().contentContainer.GetAllByType(type);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<VisualElement> GetAllByTypes(this VisualTreeAsset treeAsset, params Type[] types)
        {
            if (treeAsset == null)
            {
                return Enumerable.Empty<VisualElement>();
            }

            return treeAsset.CloneTree().contentContainer.GetAllByTypes(types);
        }

        #endregion

        #region Get All Names

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetAllNames(this VisualElement root)
        {
            return root.GetAll<VisualElement>().Select(visualElement => visualElement.name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetAllNames(this IEnumerable<VisualElement> roots)
        {
            foreach (var root in roots)
            {
                foreach (var name in root.GetAllNames())
                {
                    yield return name;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetAllNames(this VisualTreeAsset treeAsset)
        {
            if (treeAsset == null)
            {
                return Enumerable.Empty<string>();
            }

            return treeAsset.GetAll<VisualElement>()
                .Where(visualElement => visualElement.name.IsNullOrEmptyAfterTrim() == false)
                .Select(visualElement => visualElement.name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetAllNames<T>(this VisualTreeAsset treeAsset)
            where T : VisualElement
        {
            if (treeAsset == null)
            {
                return Enumerable.Empty<string>();
            }

            return treeAsset.GetAll<T>()
                .Where(visualElement => visualElement.name.IsNullOrEmptyAfterTrim() == false)
                .Select(visualElement => visualElement.name);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetAllNamesByType(this VisualTreeAsset treeAsset, Type type)
        {
            if (treeAsset == null)
            {
                return Enumerable.Empty<string>();
            }

            return treeAsset.GetAllByType(type)
                .Where(visualElement => visualElement.name.IsNullOrEmptyAfterTrim() == false)
                .Select(visualElement => visualElement.name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetAllNamesByTypes(this VisualTreeAsset treeAsset,
            params Type[] types)
        {
            if (treeAsset == null)
            {
                return Enumerable.Empty<string>();
            }

            return treeAsset.GetAllByTypes(types)
                .Where(visualElement => visualElement.name.IsNullOrEmptyAfterTrim() == false)
                .Select(visualElement => visualElement.name);
        }

        #endregion

        #region Query

        #region Single Name

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VisualElement QueryStrictly(this VisualElement root, string name, string nameSourceName)
        {
            var visualElement = root.Q(name);

            visualElement.AssertIsNotNull($"{nameSourceName}对应的{nameof(VisualElement)}");

            return visualElement;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T QueryStrictly<T>(this VisualElement root, string name, string nameSourceName)
            where T : VisualElement
        {
            var visualElement = root.Q<T>(name);

            visualElement.AssertIsNotNull($"{nameSourceName}对应的{typeof(T)}");

            return visualElement;
        }

        #endregion

        #region Enumerable Names

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<VisualElement> Query(this VisualElement root, IEnumerable<string> names)
        {
            var namesSet = new HashSet<string>(names);

            foreach (var visualElement in root.GetAll())
            {
                if (visualElement.name.IsNullOrEmpty())
                {
                    continue;
                }

                if (namesSet.Contains(visualElement.name))
                {
                    yield return visualElement;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<VisualElement> QueryStrictly(this VisualElement root,
            IEnumerable<string> names, string namesSourceName)
        {
            foreach (var (index, name) in names.Enumerate())
            {
                var visualElement = root.Q(name);

                visualElement.AssertIsNotNull($"{namesSourceName}[{index}]对应的{nameof(VisualElement)}");

                yield return visualElement;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Query<T>(this VisualElement root, IEnumerable<string> names)
            where T : VisualElement
        {
            var namesSet = new HashSet<string>(names);

            foreach (var visualElement in root.GetAll())
            {
                if (visualElement.name.IsNullOrEmpty())
                {
                    continue;
                }

                if (visualElement is T typedVisualElement && namesSet.Contains(visualElement.name))
                {
                    yield return typedVisualElement;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> QueryStrictly<T>(this VisualElement root, IEnumerable<string> names,
            string namesSourceName) where T : VisualElement
        {
            foreach (var (index, name) in names.Enumerate())
            {
                var visualElement = root.Q<T>(name);

                visualElement.AssertIsNotNull($"{namesSourceName}[{index}]对应的{typeof(T)}");

                yield return visualElement;
            }
        }

        #endregion

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