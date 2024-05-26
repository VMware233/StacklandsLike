#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace VMFramework.Localization
{
    public class LocalizationEditorManager
    {
        #region Editor Table

        private static StringTable editorTable;

        public static bool TryGetEditorTable(out StringTable table)
        {
            if (LocalizationSettings.ProjectLocale == null)
            {
                table = null;
                return false;
            }
            
            if (editorTable == null)
            {
                editorTable =
                    LocalizationSettings.StringDatabase.GetTable(LocalizationTableNames.EDITOR,
                        LocalizationSettings.ProjectLocale);
            }

            table = editorTable;

            if (table == null)
            {
                Debug.LogError($"Failed to get Editor StringTable:{LocalizationTableNames.EDITOR}");
            }

            return editorTable != null;
        }

        public static string GetStringOfEditorTable(string key, string defaultString)
        {
            if (TryGetEditorTable(out StringTable table))
            {
                var entry = table.GetEntry(key);

                if (entry != null)
                {
                    return entry.GetLocalizedString();
                }
                
                Debug.LogWarning($"Failed to find key {key} in Editor StringTable");
            }
            
            return defaultString;
        }

        #endregion

        #region Name List
        
        public static IEnumerable<ValueDropdownItem> GetTableNameList()
        {
            foreach (var collection in LocalizationEditorSettings.GetStringTableCollections())
            {
                yield return new ValueDropdownItem(collection.TableCollectionName,
                    collection.TableCollectionName);
            }
        }

        public static IEnumerable<ValueDropdownItem> GetLocaleNameList()
        {
            foreach (var locale in LocalizationEditorSettings.GetLocales())
            {
                yield return new ValueDropdownItem(locale.Identifier.Code, locale.Identifier.Code);
            }
        }

        #endregion
    }
}

#endif