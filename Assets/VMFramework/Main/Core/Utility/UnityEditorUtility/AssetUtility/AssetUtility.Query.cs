#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VMFramework.Core.Linq;
using Object = UnityEngine.Object;

namespace VMFramework.Core.Editor
{
    public static partial class AssetUtility
    {
        #region Find By Type And By Name

        public static TAsset FindAssetOfName<TAsset>(this string assetName,
            string[] searchInFolders = null) where TAsset : Object
        {
            return assetName.FindAssetOfName(typeof(TAsset), searchInFolders) as TAsset;
        }

        public static TAsset FindAssetOfName<TAsset>(this string assetName,
            string searchInFolder) where TAsset : Object
        {
            return assetName.FindAssetOfName(typeof(TAsset), searchInFolder) as TAsset;
        }

        public static Object FindAssetOfName(this string assetName, Type type,
            string searchInFolder)
        {
            return assetName.FindAssetsOfName(type, searchInFolder).FirstOrDefault();
        }

        public static Object FindAssetOfName(this string assetName, Type type,
            string[] searchInFolders = null)
        {
            return assetName.FindAssetsOfName(type, searchInFolders).FirstOrDefault();
        }

        public static IEnumerable<Object> FindAssetsOfName(this string assetName,
            Type type, string searchInFolder)
        {
            if (searchInFolder.IsNullOrEmpty())
            {
                return FindAssetsOfName(assetName, type);
            }

            return FindAssetsOfName(assetName, type, new[] {searchInFolder});
        }

        public static IEnumerable<Object> FindAssetsOfName(this string assetName,
            Type type, string[] searchInFolders = null)
        {
            var assetsOfType = type.FindAssetsOfType(searchInFolders);

            int resultCount = 0;

            assetName = assetName.Replace(" ", "");

            foreach (var asset in assetsOfType)
            {
                if (asset.name.Replace(" ", "") == assetName)
                {
                    yield return asset;

                    resultCount++;
                }
            }

            if (resultCount == 0)
            {
                Debug.LogWarning($"没找到带有名称为{assetName}，type为{type}的组件的Prefab");
            }
        }

        #endregion

        #region Find By Type

        public static IEnumerable<Object> FindAssetsOfType(this Type type,
            string searchInFolder)
        {
            if (searchInFolder.IsNullOrEmpty())
            {
                return FindAssetsOfType(type);
            }

            return FindAssetsOfType(type, new[] {searchInFolder});
        }

        /// <summary>
        /// 在指定文件夹中查找指定类型的资源，文件夹路径为Assets/xxx/xxx
        /// </summary>
        /// <param name="searchInFolder"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> FindAssetsOfType<T>(this string searchInFolder)
        {
            return FindAssetsOfType(typeof(T), searchInFolder).Cast<T>();
        }

        public static Object FindAssetOfType(this Type type)
        {
            return type.FindAssetsOfType().FirstOrDefault();
        }

        public static IEnumerable<Object> FindAssetsOfType(this Type type,
            string[] searchInFolders = null)
        {
            int resultCount = 0;

            if (type.IsDerivedFrom<Component>(false))
            {
                var guids = SearchContent("t:GameObject");

                if (guids.Length >= 1)
                {
                    foreach (var guid in guids)
                    {
                        string path = AssetDatabase.GUIDToAssetPath(guid);

                        var targetObject =
                            AssetDatabase.LoadAssetAtPath(path, typeof(GameObject))
                                as GameObject;

                        if (targetObject != null &&
                            targetObject.GetComponent(type) != null)
                        {
                            yield return targetObject.GetComponent(type);

                            resultCount++;
                        }
                    }
                }
            }
            else
            {
                var guids = SearchContent($"t:{type.Name}");

                foreach (var guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);

                    var asset = AssetDatabase.LoadAssetAtPath(path, type);

                    if (asset != null)
                    {
                        yield return asset;
                        resultCount++;
                    }
                }
            }

            if (resultCount == 0)
            {
                string searchInFoldersLog = "";

                if (searchInFolders.IsNullOrEmpty() == false)
                {
                    searchInFoldersLog = $"在{searchInFolders.ToString(",")}中";
                }

                if (type.IsDerivedFrom<Component>(false))
                {
                    Debug.LogWarning(
                        $"没{searchInFoldersLog}找到带有type为{type}的组件的Prefab");
                }
                else
                {
                    Debug.LogWarning($"没{searchInFoldersLog}找到type为{type}的Asset");
                }
            }

            string[] SearchContent(string content)
            {
                if (searchInFolders == null || searchInFolders.Length == 0)
                {
                    return AssetDatabase.FindAssets(content);
                }

                return AssetDatabase.FindAssets(content, searchInFolders);
            }
        }

        #endregion
    }
}
#endif