using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Localization.Settings;

namespace VMFramework.Localization
{
    public class LocalizedTempString : IEnumerable<KeyValuePair<string, string>>
    {
        private Dictionary<string, string> languages = new();
        
        public void Add(string language, string value) => languages.Add(language, value);

        #region Enumerable

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return languages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public string GetLocalizedString(string language, string defaultString = "")
        {
            return CollectionExtensions.GetValueOrDefault(languages, language, defaultString);
        }
        
        public static implicit operator string(LocalizedTempString localizedString)
        {
            if (localizedString == null)
            {
                return "Null";
            }
            
            if (localizedString.languages.Count == 0)
            {
                return "";
            }

            if (LocalizationSettings.HasSettings == false)
            {
                return "No Localization Settings";
            }
            
            if (LocalizationSettings.ProjectLocale == null)
            {
                return localizedString.languages.Values.First();
            }

            return localizedString.GetLocalizedString(LocalizationSettings.ProjectLocale.Identifier
                .Code);
        }

        public override string ToString()
        {
            return this;
        }
    }
}
