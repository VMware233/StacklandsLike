using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace VMFramework.Core
{
    public static class GameObjectUtility
    {
        #region Set Active

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetActive(this Component component, bool value)
        {
            component.gameObject.SetActive(value);
        }

        #endregion

        #region Prefab

        /// <summary>
        ///     用于判断场景里的GameObject是否是来自Prefab
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsPrefab(this GameObject obj)
        {
#if UNITY_EDITOR
            return PrefabUtility.GetPrefabInstanceStatus(obj) != PrefabInstanceStatus.NotAPrefab;
#else
            return false; // Always return false in a build
#endif
        }

        /// <summary>
        ///     用于判断是否是Prefab Asset
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsPrefabAsset(this GameObject obj)
        {
#if UNITY_EDITOR
            return PrefabUtility.GetPrefabAssetType(obj) != PrefabAssetType.NotAPrefab;
#else
            return false; // Always return false in a build
#endif
        }

        #endregion
    }
}