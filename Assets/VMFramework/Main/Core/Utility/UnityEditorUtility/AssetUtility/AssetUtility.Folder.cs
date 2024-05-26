#if UNITY_EDITOR
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace VMFramework.Core.Editor
{
    public static partial class AssetUtility
    {
        #region Get All Assets Recursively

        public static IEnumerable<Object> GetAllAssetsRecursively(
            this IEnumerable<Object> objects)
        {
            foreach (var obj in objects)
            {
                if (obj.IsFolder())
                {
                    foreach (var objInFolder in obj.GetAllAssetsInFolder())
                    {
                        yield return objInFolder;
                    }
                }
                else
                {
                    yield return obj;
                }
            }
        }

        #endregion

        #region Is Folder

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFolder(this Object obj)
        {
            return obj.IsFolder(out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFolder(this Object obj, out string path)
        {
            path = AssetDatabase.GetAssetPath(obj);

            return AssetDatabase.IsValidFolder(path);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFolder(this string path)
        {
            return AssetDatabase.IsValidFolder(path);
        }

        #endregion
        
        #region Get All Assets In Folder

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Object> GetAllAssetsInFolder(this Object obj)
        {
            if (obj.IsFolder(out var path))
            {
                string[] assetPaths = AssetDatabase.FindAssets("", new[] { path });

                foreach (string assetPath in assetPaths)
                {
                    Object objInFolder =
                        AssetDatabase.LoadAssetAtPath<Object>(
                            AssetDatabase.GUIDToAssetPath(assetPath));
                    yield return objInFolder;
                }
            }
        }

        #endregion
    }
}
#endif