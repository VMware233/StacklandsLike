using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static partial class TreeUtility
    {
        #region Get Root

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRoot<T>(this T node) where T : class, IParentProvider<T>
        {
            return node.TraverseToRoot(true).Last();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRoot<T>(this T node, Func<T, T> parentGetter)
            where T : class
        {
            return node.TraverseToRoot(true, parentGetter).Last();
        }

        #endregion

        #region Traverse To Root

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> TraverseToRoot<T>(this T node,
            bool includingSelf)
            where T : class, IParentProvider<T>
        {
            return TraverseToRoot(node, includingSelf, node => node.GetParent());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> TraverseToRoot<T>(this T node,
            bool includingSelf, Func<T, T> parentGetter) where T : class
        {
            while (node != null)
            {
                if (includingSelf)
                {
                    yield return node;
                }

                node = parentGetter(node);
                includingSelf = true;
            }
        }

        #endregion

        #region Has Parent

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasParent<T>(this T node, T parent, bool includingSelf)
            where T : class, IParentProvider<T>
        {
            return HasParent(node, parent, includingSelf, node => node.GetParent());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasParent<T>(this T node, T parent, bool includingSelf,
            Func<T, T> parentGetter)
            where T : class
        {
            foreach (var parentNode in node.TraverseToRoot(includingSelf,
                         parentGetter))
            {
                if (parentNode.Equals(parent))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Has Parent With Comparator

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasParent<T, TComparable>(this T node,
            TComparable comparable, Func<T, TComparable, bool> comparator,
            bool includingSelf)
            where T : class, IParentProvider<T>
        {
            return HasParent(node, comparable, comparator, includingSelf,
                node => node.GetParent());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasParent<T, TComparable>(this T node,
            TComparable comparable, Func<T, TComparable, bool> comparator,
            bool includingSelf, Func<T, T> parentGetter)
            where T : class
        {
            comparator.AssertIsNotNull(nameof(comparator));
            comparable.AssertIsNotNull(nameof(comparable));

            foreach (var parent in node.TraverseToRoot(includingSelf, parentGetter))
            {
                if (comparator(parent, comparable))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Get All Parents List

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> GetAllParentsList<T>(this T node, bool includingSelf)
            where T : class, IParentProvider<T>
        {
            return node.TraverseToRoot(includingSelf).ToList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> GetAllParentsList<T>(this T node, bool includingSelf, Func<T, T> parentGetter)
            where T : class
        {
            return node.TraverseToRoot(includingSelf, parentGetter).ToList();
        }

        #endregion
    }
}
