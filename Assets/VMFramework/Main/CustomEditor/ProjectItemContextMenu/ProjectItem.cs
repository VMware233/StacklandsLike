#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using VMFramework.Core.Editor;

namespace VMFramework.Editor.ProjectItemContextMenu
{
    public sealed class ProjectItem
    {
        public string guid { get; private set; }
        
        public string path { get; private set; }
        
        public bool isFolder { get; private set; }
        
        public Object asset { get; private set; }

        public void Set(string newGUID)
        {
            if (newGUID == null)
            {
                guid = null;
                path = null;
                isFolder = false;
                asset = null;
            }

            if (guid != newGUID)
            {
                path = newGUID.GUIDToAssetPath();
            
                isFolder = AssetDatabase.IsValidFolder(path);
            
                asset = AssetDatabase.LoadAssetAtPath<Object>(path);
            }
            
            guid = newGUID;
        }
    }
}
#endif