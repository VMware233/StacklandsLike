using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public partial class DescribedGamePrefab : LocalizedGameTypedGamePrefab, IDescribedGamePrefab
    {
        [LabelText(SdfIconType.Bootstrap), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [JsonProperty]
        public TextTagFormat nameFormat = new();

        [TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [JsonProperty]
        public bool hasDescription = false;

        [LabelText(SdfIconType.BlockquoteLeft), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [Indent]
        [ShowIf(nameof(hasDescription))]
        [JsonProperty]
        public LocalizedStringReference description = new();

        [LabelText(SdfIconType.Bootstrap), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [Indent]
        [ShowIf(nameof(hasDescription))]
        [JsonProperty]
        public TextTagFormat descriptionFormat = new();

        #region Interface Implementation

        string INameOwner.name => nameFormat.GetText(name);

        string IDescriptionOwner.description
        {
            get
            {
                if (hasDescription)
                {
                    return descriptionFormat.GetText(description);
                }

                return null;
            }
        }

        #endregion
    }
}