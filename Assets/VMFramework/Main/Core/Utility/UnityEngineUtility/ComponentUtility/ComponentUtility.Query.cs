using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class ComponentUtility
    {
        #region Query

        #region Query First

        public static T QueryFirstComponentInChildren<T>(this Component root, bool includingSelf)
            where T : Component
        {
            foreach (var transform in root.transform.GetAllChildren(includingSelf))
            {
                var component = transform.GetComponent<T>();

                if (component != null)
                {
                    return component;
                }
            }

            return default;
        }

        public static T QueryFirstComponentInChildren<T>(this Component root, string name, bool includingSelf)
            where T : Component
        {
            foreach (var transform in root.transform.GetAllChildren(includingSelf))
            {
                if (transform.name != name)
                {
                    continue;
                }

                var component = transform.GetComponent<T>();

                if (component != null)
                {
                    return component;
                }
            }

            return default;
        }

        public static T QueryFirstComponentInParents<T>(this Component root, bool includingSelf)
            where T : Component
        {
            foreach (var transform in root.transform.GetAllParents(includingSelf))
            {
                var component = transform.GetComponent<T>();

                if (component != null)
                {
                    return component;
                }
            }

            return default;
        }

        public static T QueryFirstComponentInParents<T>(this Component root, string name, bool includingSelf)
            where T : Component
        {
            foreach (var transform in root.transform.GetAllParents(includingSelf))
            {
                if (transform.name != name)
                {
                    continue;
                }

                var component = transform.GetComponent<T>();

                if (component != null)
                {
                    return component;
                }
            }

            return default;
        }

        #endregion

        #region Query All

        /// <summary>
        /// 在所有子物体中查找特定类型的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c"></param>
        /// <param name="includingSelf">查找对象是否也包含自身</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> QueryComponentsInChildren<T>(this Component c,
            bool includingSelf)
            where T : Component
        {
            foreach (var transform in c.transform.GetAllChildren(includingSelf))
            {
                var components = transform.GetComponents<T>();

                foreach (var component in components)
                {
                    yield return component;
                }
            }
        }

        /// <summary>
        /// 在所有父物体中查找特定类型的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c"></param>
        /// <param name="includingSelf">查找对象是否也包含自身</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> QueryComponentsInParents<T>(this Component c,
            bool includingSelf) where T : Component
        {
            foreach (var transform in c.transform.GetAllParents(includingSelf))
            {
                var components = transform.GetComponents<T>();

                foreach (var component in components)
                {
                    yield return component;
                }
            }
        }

        #endregion

        #endregion

        #region Has

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponent(this Component c, Type componentType)
        {
            return c.GetComponent(componentType) != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponent(this GameObject obj, Type componentType)
        {
            return obj.GetComponent(componentType) != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponent<T>(this Component c) where T : Component
        {
            return c.GetComponent<T>() != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponent<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponent<T>() != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponentInChildren<T>(this Component c,
            bool includingSelf)
            where T : Component
        {
            return HasComponentInChildren(c, typeof(T), includingSelf);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponentInChildren(this Component c,
            Type componentType, bool includingSelf)
        {
            return c.transform.GetAllChildren(includingSelf)
                .Any(child => child.HasComponent(componentType));
        }

        #endregion
    }
}