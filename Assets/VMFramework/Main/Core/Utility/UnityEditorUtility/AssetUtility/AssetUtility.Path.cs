#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace VMFramework.Core.Editor
{
    public static partial class AssetUtility
    {
        #region Get Asset Path

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetAssetPath(this Object obj)
        {
            return AssetDatabase.GetAssetPath(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GUIDToAssetPath(this string guid)
        {
            return AssetDatabase.GUIDToAssetPath(guid);
        }

        #endregion

        #region Rename

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Rename(this Object obj, string newName)
        {
            if (newName.IsNullOrEmptyAfterTrim())
            {
                Debug.LogWarning($"{obj.name}'s New Name cannot be Null or Empty.");
                return;
            }
            
            string selectedAssetPath = AssetDatabase.GetAssetPath(obj);

            if (selectedAssetPath.IsNullOrEmpty())
            {
                obj.name = newName;
            }
            else
            {
                AssetDatabase.RenameAsset(selectedAssetPath, newName);
            }

            Undo.RecordObject(obj, "Rename");
            
            obj.EnforceSave();
        }

        #endregion
    }
}
#endif