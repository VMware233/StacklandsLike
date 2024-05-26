#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Localization;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Editor;

namespace VMFramework.Localization
{
    public static class StringTableCollectionUtility
    {
        public static IEnumerable<string> GetKeys(this StringTableCollection collection)
        {
            if (collection == null)
            {
                yield break;
            }

            foreach (var row in collection.GetRowEnumerator())
            {
                var tableEntries = row.TableEntries;

                if (tableEntries.Length == 0)
                {
                    continue;
                }
                
                var entry = tableEntries[0];
                
                yield return entry.Key;
            }
        }

        public static bool TryGetStringTableCollection(string tableName, out StringTableCollection collection)
        {
            if (tableName.IsNullOrEmpty())
            {
                collection = null;
                return false;
            }

            collection = LocalizationEditorSettings.GetStringTableCollection(tableName);
            return collection != null;
        }

        public static bool ExitsKey(this StringTableCollection collection, string key)
        {
            if (collection == null)
            {
                return false;
            }

            if (key.IsNullOrEmptyAfterTrim())
            {
                return false;
            }

            var table = collection.StringTables.FirstOrDefault();

            if (table == null)
            {
                return false;
            }

            return table.GetEntry(key) != null;
        }

        public static bool CreateKey(this StringTableCollection collection, string key, string defaultValue)
        {
            collection.AssertIsNotNull(nameof(collection));

            if (collection.ExitsKey(key))
            {
                Debug.LogWarning($"Key : {key} already exists in table : {collection.name}");
                return false;
            }
            
            foreach (var stringTable in collection.StringTables)
            {
                stringTable.AddEntry(key, defaultValue);
                
                stringTable.SetEditorDirty();
            }
            
            collection.EnforceSave();

            return true;
        }
    }
}
#endif