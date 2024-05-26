#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector.Modules.Localization.Editor;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    public static class OdinLocalizationEditorWindowUtility
    {
        public static void ShowTableCreator()
        {
            OdinLocalizationEditorWindow.OpenFromMenu();
        }

        public static void ShowTable(LocalizationTableCollection collection)
        {
            var window = EditorWindow.GetWindow<OdinLocalizationEditorWindow>();
            window.ForceMenuTreeRebuild();
            foreach (var menuItem in window.MenuTree.EnumerateTree())
            {
                if (menuItem == null)
                {
                    continue;
                }

                bool isTableCollection = false;
                if (menuItem.Value is OdinStringTableCollectionEditor stringTableEditor)
                {
                    if (stringTableEditor.Collection == collection)
                    {
                        isTableCollection = true;
                    }
                }

                if (menuItem.Value is OdinAssetTableCollectionEditor assetTableEditor)
                {
                    if (assetTableEditor.Collection == collection)
                    {
                        isTableCollection = true;
                    }
                }

                if (isTableCollection == false)
                {
                    continue;
                }

                window.MenuTree.Selection.Clear();
                window.MenuTree.Selection.Add(menuItem);
                
                break;
            }
        }
    }
}
#endif