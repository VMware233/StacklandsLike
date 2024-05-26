using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VMFramework.Core
{
    public partial class ComponentUtility
    {
        #region Remove First

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponent(this Component component,
            Type componentType)
        {
            var target = component.GetComponent(componentType);

            if (target != null)
            {
                Object.Destroy(target);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponent(this GameObject gameObject,
            Type componentType)
        {
            var target = gameObject.GetComponent(componentType);

            if (target != null)
            {
                Object.Destroy(target);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponent<T>(this GameObject gameObject)
            where T : Component
        {
            gameObject.RemoveFirstComponent(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponent<T>(this Component component)
            where T : Component
        {
            component.RemoveFirstComponent(typeof(T));
        }

        #endregion

        #region Remove First Immediate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponentImmediate(this Component component,
            Type componentType)
        {
            var target = component.GetComponent(componentType);

            if (target != null)
            {
                Object.DestroyImmediate(target);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponentImmediate(this GameObject gameObject,
            Type componentType)
        {
            var target = gameObject.GetComponent(componentType);

            if (target != null)
            {
                Object.DestroyImmediate(target);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponentImmediate<T>(this GameObject gameObject)
            where T : Component
        {
            gameObject.RemoveFirstComponentImmediate(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFirstComponentImmediate<T>(this Component component)
            where T : Component
        {
            component.RemoveFirstComponentImmediate(typeof(T));
        }

        #endregion

        #region Remove All

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAllComponents<T>(this GameObject gameObject)
            where T : Component
        {
            var targets = gameObject.GetComponents<T>();

            foreach (var target in targets)
            {
                Object.Destroy(target);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAllComponents<T>(this Component component)
            where T : Component
        {
            var targets = component.GetComponents<T>();

            foreach (var target in targets)
            {
                Object.Destroy(target);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAllComponents(this GameObject gameObject, Type componentType)
        {
            var targets = gameObject.GetComponents(componentType);

            foreach (var target in targets)
            {
                Object.Destroy(target);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAllComponents(this Component component, Type componentType)
        {
            var targets = component.GetComponents(componentType);

            foreach (var target in targets)
            {
                Object.Destroy(target);
            }
        }

        #endregion
        
        #region Remove All Immediate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAllComponentsImmediate<T>(this GameObject gameObject)
            where T : Component
        {
            var targets = gameObject.GetComponents<T>();

            foreach (var target in targets)
            {
                Object.DestroyImmediate(target);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAllComponentsImmediate<T>(this Component component)
            where T : Component
        {
            var targets = component.GetComponents<T>();

            foreach (var target in targets)
            {
                Object.DestroyImmediate(target);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAllComponentsImmediate(this GameObject gameObject, Type componentType)
        {
            var targets = gameObject.GetComponents(componentType);

            foreach (var target in targets)
            {
                Object.DestroyImmediate(target);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAllComponentsImmediate(this Component component, Type componentType)
        {
            var targets = component.GetComponents(componentType);

            foreach (var target in targets)
            {
                Object.DestroyImmediate(target);
            }
        }

        #endregion
    }
}