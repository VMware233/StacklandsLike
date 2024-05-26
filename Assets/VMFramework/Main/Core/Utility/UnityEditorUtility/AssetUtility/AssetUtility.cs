#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEditor;

namespace VMFramework.Core.Editor
{
    public static partial class AssetUtility
    {
        #region Save

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnforceSave(this Object obj)
        {
            if (obj == null)
            {
                Debug.LogWarning("保存对象为空，无法保存");
                return;
            }
            
            obj.SetEditorDirty();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEditorDirty(this Object obj)
        {
            if (obj == null)
            {
                Debug.LogWarning("保存对象为空，无法设置Dirty");
                return;
            }
            
            UnityEditor.EditorUtility.SetDirty(obj);
        }

        #endregion

        #region Copy Asset

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CopyAssetTo<T>(this T obj, string newPath) where T : Object
        {
            if (obj == null)
            {
                Debug.LogWarning("复制对象为空，无法复制");
                return null;
            }

            var sourcePath = obj.GetAssetPath();

            if (sourcePath.IsNullOrEmpty())
            {
                Debug.LogWarning("复制对象路径为空，无法复制");
                return null;
            }

            if (newPath.IsNullOrEmpty())
            {
                Debug.LogWarning("复制目标路径为空，无法复制");
                return null;
            }

            var absoluteDirectoryPath = IOUtility.projectFolderPath.PathCombine(newPath).GetDirectoryPath();

            if (absoluteDirectoryPath.ExistsDirectory() == false)
            {
                Debug.LogWarning($"复制目标路径{absoluteDirectoryPath}不存在，无法复制");
                return null;
            }

            if (AssetDatabase.CopyAsset(sourcePath, newPath) == false)
            {
                Debug.LogWarning($"复制{obj.GetType()}从{sourcePath}到{newPath}失败");
                return null;
            }
            
            var newObj = AssetDatabase.LoadAssetAtPath<T>(newPath);
            
            return newObj;
        }

        #endregion

        #region Exists Asset

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ExistsAsset(this string path)
        {
            return AssetDatabase.AssetPathToGUID(path).IsNullOrWhiteSpace() == false;
        }

        #endregion

        #region Create Asset

        public static bool TryCreateAsset(this Object obj, string path)
        {
            if (IOUtility.projectFolderPath.TryMakeRelative(path, out path) == false)
            {
                Debug.LogWarning($"保存路径{path}不在Assets文件夹下，创建{obj.GetType()}失败");

                Object.DestroyImmediate(obj);
                return false;
            }
            
            obj.CreateAsset(path);
            return true;
        }
        
        public static void CreateAsset(this Object obj, string path)
        {
            AssetDatabase.CreateAsset(obj, path);
            obj.EnforceSave();
            AssetDatabase.Refresh();
        }

        #endregion

        #region Delete Asset
        
        public static void DeleteAsset(this Object obj)
        {
            if (obj.IsAsset() == false)
            {
                Debug.LogWarning($"{obj}不是Asset，无法删除");
                return;
            }
            
            Undo.DestroyObjectImmediate(obj);
            AssetDatabase.Refresh();
        }

        #endregion

        #region Is Asset
        
        public static bool IsAsset(this Object obj)
        {
            return AssetDatabase.Contains(obj);
        }

        #endregion
    }
}

#endif