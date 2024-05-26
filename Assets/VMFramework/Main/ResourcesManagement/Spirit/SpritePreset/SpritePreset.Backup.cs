#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using VMFramework.Core.Editor;

namespace VMFramework.ResourcesManagement
{
    public partial class SpritePreset
    {
        [LabelWidth(60), TabGroup(TAB_GROUP_NAME, TOOLS_CATEGORY)]
        [SerializeField]
        [DisplayAsString]
        private string backupAssetPath;

        [LabelWidth(60), TabGroup(TAB_GROUP_NAME, TOOLS_CATEGORY)]
        [SerializeField]
        [DisplayAsString]
        private string backupAssetName;
        
        [Button, TabGroup(TAB_GROUP_NAME, TOOLS_CATEGORY)]
        public void GenerateBackup()
        {
            if (sprite != null)
            {
                backupAssetPath = AssetDatabase.GetAssetPath(sprite).GetDirectoryPath();

                backupAssetName = sprite.name;
            }
        }

        [Button, TabGroup(TAB_GROUP_NAME, TOOLS_CATEGORY)]
        public void RestoreFromBackup()
        {
            if (string.IsNullOrEmpty(backupAssetPath) == false &&
                string.IsNullOrEmpty(backupAssetName) == false)
            {
                sprite = backupAssetName.FindAssetOfName<Sprite>(backupAssetPath);
            }
        }
    }
}
#endif