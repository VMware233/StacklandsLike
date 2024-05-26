#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.Localization;
using UnityEditor.Localization.UI;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Localization;

namespace VMFramework.OdinExtensions
{
    internal sealed class TableNameAttributeDrawer : GeneralValueDropdownAttributeDrawer<TableNameAttribute>, 
        IDefinesGenericMenuItems
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            return LocalizationEditorManager.GetTableNameList();
        }

        protected override Texture GetSelectorIcon(object value)
        {
            if (value is not string tableName)
            {
                return null;
            }

            if (StringTableCollectionUtility.TryGetStringTableCollection(tableName, out var collection) == false)
            {
                return null;
            }

            return GUIHelper.GetAssetThumbnail(collection, collection.GetType(), true);
        }

        protected override void DrawCustomButtons()
        {
            base.DrawCustomButtons();
            
            var value = Property.ValueEntry.WeakSmartValue;

            if (value is not string tableName)
            {
                return;
            }
            
            StringTableCollection collection = null;

            if (tableName.IsNullOrEmpty() == false)
            {
                collection = LocalizationEditorSettings.GetStringTableCollection(tableName);
            }
            
            if (Button("Edit Table", SdfIconType.PencilSquare))
            {
                if (collection == null)
                {
                    LocalizationTablesWindow.ShowTableCreator();
                }
                else
                {
                    OdinLocalizationEditorWindowUtility.ShowTable(collection);
                }
            }
            
            if (collection != null)
            {
                if (Button("Select Table Resource", SdfIconType.Search))
                {
                    Selection.activeObject = collection;
                }
            }
            
            if (Button("Create New Table", SdfIconType.Plus))
            {
                LocalizationTablesWindow.ShowTableCreator();
            }
        }

        void IDefinesGenericMenuItems.PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
        {
            var value = Property.ValueEntry.WeakSmartValue;

            if (value is not string tableName)
            {
                return;
            }

            if (tableName.IsNullOrWhiteSpace())
            {
                ShowWindow(null);
                return;
            }
            
            var stringTableCollection = LocalizationEditorSettings.GetStringTableCollection(tableName);

            ShowWindow(stringTableCollection);
            
            return;

            void ShowWindow(LocalizationTableCollection collection)
            {
                genericMenu.AddSeparator("");
                
                if (collection != null)
                {
                    genericMenu.AddItem(new GUIContent("Open Table"), false, () =>
                    {
                        OdinLocalizationEditorWindowUtility.ShowTable(collection);
                    });
                }
                
                genericMenu.AddItem(new GUIContent("Create New Table"), false,
                    LocalizationTablesWindow.ShowTableCreator);
            }
        }
    }
}
#endif