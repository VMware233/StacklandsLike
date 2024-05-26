using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VMFramework.Core
{
    public static class GameObjectOrComponentFindOrCreateUtility
    {
        #region Game Object

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public static GameObject FindOrCreateGameObject(Func<GameObject> onFind, Func<GameObject> onCreate,
            Transform parent = null, Action<GameObject> afterCreate = null)
        {
            var newObject = onFind();

            if (newObject == null)
            {
                newObject = onCreate();
                afterCreate?.Invoke(newObject);
            }

            if (parent != null)
            {
                newObject.transform.SetParent(parent);
            }

            return newObject;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public static GameObject FindOrCreateGameObject(this string name, Func<GameObject> onCreate,
            Transform parent = null)
        {
            return FindOrCreateGameObject(name.FindObject<GameObject>, onCreate, parent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public static GameObject FindOrCreateGameObject(this string name, Transform parent = null)
        {
            return name.FindOrCreateGameObject(() => new GameObject(name), parent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public static GameObject FindOrCreateGameObject(this string name, [NotNull] GameObject parentObject)
        {
            return name.FindOrCreateGameObject(parentObject.transform);
        }

        #endregion

        #region Component

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public static T FindOrCreateComponent<T>(Func<T> onFind, Func<T> onCreate, Transform parent = null,
            Action<T> afterCreate = null) where T : Component
        {
            var result = onFind();
            if (result == null)
            {
                result = onCreate();
                afterCreate?.Invoke(result);
            }

            if (parent != null)
            {
                result.transform.SetParent(parent);
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public static T FindOrCreateComponent<T>([NotNull] this GameObject attachedObject) where T : Component
        {
            return FindOrCreateComponent(attachedObject.GetComponent<T>, attachedObject.AddComponent<T>);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public static T FindOrCreateComponent<T>([NotNull] this Component attachedComponent)
            where T : Component
        {
            return attachedComponent.gameObject.FindOrCreateComponent<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public static T FindOrCreateUniqueComponent<T>([NotNull] this GameObject attachedObject)
            where T : Component
        {
#if UNITY_2023
            return FindOrCreateComponent(Object.FindFirstObjectByType<T>, attachedObject.AddComponent<T>);
#else
            return FindOrCreateComponent(Object.FindObjectOfType<T>, attachedObject.FindOrCreateComponent<T>);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public static T FindOrCreateUniqueComponent<T>([NotNull] this string name, Transform parent = null)
            where T : Component
        {
            return FindOrCreateComponent(name.FindObject<T>,
                name.FindOrCreateGameObject().FindOrCreateComponent<T>, parent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [NotNull]
        public static T FindOrCreateUniqueComponent<T>([NotNull] this string name,
            [NotNull] GameObject parentObject) where T : Component
        {
            return name.FindOrCreateUniqueComponent<T>(parentObject.transform);
        }

        #endregion
    }
}