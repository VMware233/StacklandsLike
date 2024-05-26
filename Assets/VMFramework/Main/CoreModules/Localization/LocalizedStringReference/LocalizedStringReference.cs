using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization.Settings;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.Localization
{
    [PreviewComposite]
    public partial class LocalizedStringReference : BaseConfig, IReadOnlyLocalizedStringReference
    {
        private const string TABLE_HORIZONTAL_GROUP = "TableHorizontalGroup";
        
        private const string KEY_TOOL_HORIZONTAL_GROUP = "KeyToolHorizontalGroup";

#if UNITY_EDITOR
        [HideLabel, HorizontalGroup(TABLE_HORIZONTAL_GROUP)]
        [OnValueChanged(nameof(OnTableNameChanged))]
        [TableName]
#endif
        [SerializeField]
        private string _tableName;

        public string tableName
        {
            get => _tableName;
            set
            {
                _tableName = value;
#if UNITY_EDITOR
                if (Application.isPlaying == false)
                {
                    OnTableNameChanged();
                }
#endif
            }
        }
        
#if UNITY_EDITOR
        [LabelWidth(40)]
        [ShowIf(nameof(ExistsTable))]
#endif
        [KeyName("@" + nameof(tableName))]
        public string key;
        
        public string defaultValue;

        #region To String

        public static implicit operator string(LocalizedStringReference localizedStringReference)
        {
            if (localizedStringReference == null)
            {
                return "Null";
            }
            
            return localizedStringReference.ToString();
        }

        public override string ToString()
        {
            if (tableName.IsNullOrEmpty())
            {
                return defaultValue;
            }

#if UNITY_EDITOR
            if (Application.isPlaying == false)
            {
                return ToStringEditor();
            }
#endif

            var stringTableRuntime = LocalizationSettings.StringDatabase.GetTable(tableName,
                LocalizationSettings.SelectedLocale);

            if (stringTableRuntime == null)
            {
                Debug.LogWarning("Table not found: " + tableName);
                return defaultValue;
            }
                
            var entry = stringTableRuntime.GetEntry(key);
                
            if (entry == null)
            {
                Debug.LogWarning("Key not found: " + key);
                return defaultValue;
            }
                
            return entry.GetLocalizedString();
        }

        #endregion
    }
}
