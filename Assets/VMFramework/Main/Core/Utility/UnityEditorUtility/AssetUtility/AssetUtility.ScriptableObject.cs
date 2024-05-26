#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace VMFramework.Core.Editor
{
    public static partial class AssetUtility
    {
        public static ScriptableObject FindScriptableObject(this Type type)
        {
            if (type.IsDerivedFrom<ScriptableObject>(false) == false)
            {
                Debug.LogWarning($"{type}不是{nameof(ScriptableObject)}的子类");
                return default;
            }

            var result = type.FindAssetOfType() as ScriptableObject;

            return result;
        }

        public static ScriptableObject FindOrCreateScriptableObject(this Type type, string newPath, 
            string newName)
        {
            if (type.IsDerivedFrom<ScriptableObject>(false) == false)
            {
                Debug.LogWarning($"{type}不是{nameof(ScriptableObject)}的子类");
                return default;
            }

            newPath.CreateDirectory();
            
            var result = type.FindAssetOfType() as ScriptableObject;

            if (result == null)
            {
                var temp = ScriptableObject.CreateInstance(type);
                AssetDatabase.CreateAsset(temp, Path.Combine(newPath, $"{newName}.asset"));
                AssetDatabase.Refresh();
                //DestroyImmediate(temp, true);

                result = type.FindAssetOfType() as ScriptableObject;

                if (result == null)
                {
                    Debug.LogWarning($"种类为:{type}" +
                                     $"的{nameof(ScriptableObject)}在{newPath}/{newName}.asset下创建失败");
                }
            }

            return result;
        }

        public static T FindOrCreateScriptableObjectAtPath<T>(this string path) where T : ScriptableObject
        {
            if (path.EndsWith(".asset") == false)
            {
                path += ".asset";
            }
            
            var result = AssetDatabase.LoadAssetAtPath<T>(path);

            if (result == null)
            {
                result = path.CreateScriptableObject<T>();
            }

            return result;
        }

        public static T CreateScriptableObject<T>(this string path) where T : ScriptableObject
        {
            if (path.EndsWith(".asset") == false)
            {
                path += ".asset";
            }
            
            var directoryPath = path.GetDirectoryPath();
            var absoluteDirectoryPath = IOUtility.projectFolderPath.PathCombine(directoryPath);

            if (absoluteDirectoryPath.ExistsDirectory())
            {
                var existedAsset = AssetDatabase.LoadAssetAtPath<T>(path);

                if (existedAsset != null)
                {
                    Debug.LogWarning($"{path}下的资源已存在{typeof(T)}，无法创建{typeof(T)}");
                    return null;
                }
            }
            else
            {
                absoluteDirectoryPath.CreateDirectory();
            }
            
            var result = ScriptableObject.CreateInstance<T>();

            if (result == null)
            {
                Debug.LogWarning($"创建{typeof(T)}失败");
                
                return result;
            }
            
            AssetDatabase.CreateAsset(result, path);
            AssetDatabase.Refresh();
            
            if (result == null)
            {
                Debug.LogWarning($"种类为:{typeof(T)}的资源在{path}下创建失败");
            }
            
            return result;
        }

        public static ScriptableObject CreateScriptableObject(this Type type, string path)
        {
            if (path.EndsWith(".asset") == false)
            {
                path += ".asset";
            }
            
            var directoryPath = path.GetDirectoryPath();
            var absoluteDirectoryPath = IOUtility.projectFolderPath.PathCombine(directoryPath);

            if (absoluteDirectoryPath.ExistsDirectory())
            {
                var existedAsset = AssetDatabase.LoadAssetAtPath(path, type);

                if (existedAsset != null)
                {
                    Debug.LogWarning($"{path}下的资源已存在{type}，无法创建{type}");
                    return null;
                }
            }
            else
            {
                absoluteDirectoryPath.CreateDirectory();
            }
            
            var result = ScriptableObject.CreateInstance(type);
            AssetDatabase.CreateAsset(result, path);
            AssetDatabase.Refresh();
            
            return result;
        }
    }
}
#endif