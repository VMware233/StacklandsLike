using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GeneralSetting
    {
#if UNITY_EDITOR
        [field: LabelText("Default Localization Table"), TabGroup(TAB_GROUP_NAME, LOCALIZABLE_SETTING_CATEGORY)]
        [field: InfoBox("Localization Settings is disabled", VisibleIf = "@!localizationEnabled")]
        [field: TableName]
        [field: OnValueChanged(nameof(OnDefaultLocalizationTableNameChanged))]
        [field: EnableIf(nameof(localizationEnabled))]
#endif
        [field: SerializeField]
        [JsonProperty]
        public string defaultLocalizationTableName { get; private set; }
    }
}