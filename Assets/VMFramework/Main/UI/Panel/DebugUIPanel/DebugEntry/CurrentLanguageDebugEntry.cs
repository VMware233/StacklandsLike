using UnityEngine.Localization.Settings;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.UI
{
    [GamePrefabTypeAutoRegister(ID)]
    public class CurrentLanguageDebugEntry : TitleContentDebugEntry
    {
        public const string ID = "current_language_debug_entry";

        public override bool ShouldDisplay()
        {
            return LocalizationSettings.SelectedLocale != null;
        }

        protected override string GetContent()
        {
            return LocalizationSettings.SelectedLocale.Identifier.CultureInfo.Name;
        }
    }
}
