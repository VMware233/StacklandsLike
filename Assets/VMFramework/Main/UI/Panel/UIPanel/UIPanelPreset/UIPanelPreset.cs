using System;
using System.Collections.Generic;
using VMFramework.GameLogicArchitecture;
using VMFramework.Core;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.GameEvents;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public partial class UIPanelPreset : LocalizedGamePrefab, IUIPanelPreset
    {
        protected override string idSuffix => "ui";

        [TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [ShowInInspector]
        public virtual Type controllerType => typeof(UIPanelController);

        [SuffixLabel("UI With larger Sorting Order will cover smaller ones")]
        [TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [JsonProperty]
        public int sortingOrder = 0;

        [TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [JsonProperty]
        public bool isUnique = true;
        
        [TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [HideIf(nameof(isUnique))]
        [MinValue(0)]
        [JsonProperty]
        public int prewarmCount = 0;

        [TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [JsonProperty]
        public bool autoOpenOnCreation = false;

        [TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [GamePrefabID(typeof(IGameEventConfig))]
        [ListDrawerSettings(ShowFoldout = false)]
        [DisallowDuplicateElements]
        [JsonProperty]
        public List<string> gameEventDisabledOnOpen = new();

        [TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [JsonProperty]
        public bool enableUICloseGameEvent = false;

        [TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [GamePrefabID(typeof(IGameEventConfig))]
        [IsNotNullOrEmpty]
        [ShowIf(nameof(enableUICloseGameEvent))]
        [JsonProperty]
        public string uiCloseGameEventID;

        [TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [JsonProperty]
        public bool enableUIGameEvent = false;

        [TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [GamePrefabID(typeof(IGameEventConfig))]
        [IsNotNullOrEmpty]
        [ShowIf(nameof(enableUIGameEvent))]
        [JsonProperty]
        public string uiToggleGameEventID;

        #region Interface Implementation

        bool IUIPanelPreset.isUnique => isUnique;

        int IUIPanelPreset.prewarmCount => prewarmCount;

        #endregion

        public override void CheckSettings()
        {
            base.CheckSettings();

            controllerType.AssertIsDerivedFrom(typeof(IUIPanelController), true, false);
        }
    }
}
