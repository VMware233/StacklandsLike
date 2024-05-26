using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public abstract partial class LocalizedGamePrefab : GamePrefab, ILocalizedGamePrefab
    {
        #region Configs

        [LabelText(SdfIconType.FileEarmarkPersonFill),
         TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY, SdfIconType.Info, TextColor = "blue")]
        [JsonProperty(Order = -5000), PropertyOrder(-5000)]
        public LocalizedStringReference name = new();

        #endregion

        #region Interface Implementations

        string INameOwner.name => name;

        IReadOnlyLocalizedStringReference ILocalizedNameOwner.nameReference => name;

        #endregion
    }
}