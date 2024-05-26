#if UNITY_EDITOR
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using VMFramework.Core;
using VMFramework.Core.Editor;

namespace VMFramework.Localization
{
    public partial class LocalizedStringReference
    {
        #region Init

        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();
            
            OnTableNameChanged();
        }

        #endregion

        #region Default Value

        private bool ExistsDefaultValue() => defaultValue.IsNullOrEmptyAfterTrim() == false;

        #endregion
        
        #region Table

        [HideLabel, HorizontalGroup(TABLE_HORIZONTAL_GROUP)]
        [ShowInInspector]
        private StringTableCollection stringTableCollection;

        private void OnTableNameChanged()
        {
            StringTableCollectionUtility.TryGetStringTableCollection(tableName, out stringTableCollection);
        }

        private bool ExistsTable()
        {
            return stringTableCollection!= null;
        }

        #endregion

        #region Key

        private bool ExistsKey()
        {
            return stringTableCollection.ExitsKey(key);
        }

        #region Set Key Value By Default

        [Button, HorizontalGroup(KEY_TOOL_HORIZONTAL_GROUP)]
        [ShowIf(nameof(ExistsDefaultValue))]
        public void SetKeyValueByDefault()
        {
            SetKeyValueInEditor(defaultValue, false);
        }

        #endregion
        
        #region Set Key Value In Editor

        public void SetKeyValueInEditor(string value, bool replace)
        {
            if (stringTableCollection == null)
            {
                Debug.LogWarning("No table selected.");
                return;
            }

            if (key.IsNullOrEmptyAfterTrim())
            {
                Debug.LogWarning("Key cannot be empty.");
                return;
            }

            foreach (var stringTable in stringTableCollection.StringTables)
            {
                var entry = stringTable.GetEntry(key);

                if (entry != null)
                {
                    if (entry.GetLocalizedString().IsNullOrEmptyAfterTrim() == false && replace == false)
                    {
                        continue;
                    }
                }

                stringTable.AddEntry(key, value);
                
                stringTable.SetEditorDirty();
            }
            
            stringTableCollection.SetEditorDirty();
            stringTableCollection.SharedData.SetEditorDirty();
            
            AssetDatabase.SaveAssets();
        }

        #endregion

        #endregion

        #region To String

        private string ToStringEditor()
        {
            StringTable table;

            if (LocalizationSettings.ProjectLocale == null)
            {
                var collection = LocalizationEditorSettings.GetStringTableCollection(tableName);

                if (collection == null)
                {
                    table = null;
                }
                else
                {
                    table = collection.StringTables.First();
                }
            }
            else
            {
                table = LocalizationSettings.StringDatabase.GetTable(tableName,
                    LocalizationSettings.ProjectLocale);
            }

            if (table == null)
            {
                return $"(Table not found: {tableName})" + defaultValue;
            }
                
            if (key.IsNullOrEmptyAfterTrim())
            {
                return $"Key cannot be empty." + defaultValue;
            }
                
            var entryEditor = table.GetEntry(key);

            if (entryEditor == null)
            {
                return $"(Key not found: {key})" + defaultValue;
            }
                
            return entryEditor.GetLocalizedString();
        }

        #endregion
    }
}
#endif