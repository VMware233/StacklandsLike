using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using VMFramework.Configuration;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public sealed partial class UIPanelGeneralSetting : GamePrefabGeneralSetting
    {
        #region Category

        private const string PANEL_SETTING_CATEGORY = "面板设置";

        #endregion

        #region MetaData;

        public override Type baseGamePrefabType => typeof(UIPanelPreset);

        #endregion

        [HideLabel, TabGroup(TAB_GROUP_NAME, PANEL_SETTING_CATEGORY)]
        public ContainerChooser container;

        [LabelText("默认主题"), TabGroup(TAB_GROUP_NAME, PANEL_SETTING_CATEGORY)]
        public ThemeStyleSheet defaultTheme;

        [LabelText("默认屏幕匹配模式"), TabGroup(TAB_GROUP_NAME, PANEL_SETTING_CATEGORY)]
        [JsonProperty]
        public PanelScreenMatchMode defaultScreenMatchMode = PanelScreenMatchMode.MatchWidthOrHeight;

        [LabelText("默认宽高匹配度"), TabGroup(TAB_GROUP_NAME, PANEL_SETTING_CATEGORY)]
        [PropertyRange(0, 1)]
        [JsonProperty]
        public float defaultMatch = 0.5f;

        [LabelText("默认参考分辨率"), TabGroup(TAB_GROUP_NAME, PANEL_SETTING_CATEGORY)]
        [JsonProperty]
        public Vector2Int defaultReferenceResolution = new(1920, 1080);

        [LabelText("面板设置字典"), TabGroup(TAB_GROUP_NAME, PANEL_SETTING_CATEGORY)]
        [ShowInInspector]
        private Dictionary<int, PanelSettings> panelSettingsBySortingOrder = new();

        [LabelText("开启语言设置"), TabGroup(TAB_GROUP_NAME, LOCALIZABLE_SETTING_CATEGORY)]
        [ToggleButtons("开启", "关闭")]
        public bool enableLanguageConfigs = true;

        [LabelText("语言设置"), TabGroup(TAB_GROUP_NAME, LOCALIZABLE_SETTING_CATEGORY)]
        [ShowIf(nameof(enableLanguageConfigs))]
        public DictionaryConfigs<string, UIPanelLanguageConfig> languageConfigs = new();

        #region Init

        protected override void OnPreInit()
        {
            base.OnPreInit();
            
            languageConfigs.Init();

            panelSettingsBySortingOrder.Clear();

            foreach (var prefab in GamePrefabManager.GetAllGamePrefabs<UIPanelPreset>())
            {
                if (panelSettingsBySortingOrder.ContainsKey(prefab.sortingOrder) == false)
                {
                    panelSettingsBySortingOrder[prefab.sortingOrder] = CreateInstance<PanelSettings>();
                }
            }

            foreach (var (sortingOrder, panelSetting) in panelSettingsBySortingOrder)
            {
                panelSetting.name = sortingOrder.ToString();
                panelSetting.sortingOrder = sortingOrder;
                panelSetting.themeStyleSheet = defaultTheme;
                panelSetting.scaleMode = PanelScaleMode.ScaleWithScreenSize;
                panelSetting.screenMatchMode = defaultScreenMatchMode;
                panelSetting.match = defaultMatch;
                panelSetting.referenceResolution = defaultReferenceResolution;
            }
        }

        #endregion

        #region Check

        public override void CheckSettings()
        {
            base.CheckSettings();

            if (enableLanguageConfigs)
            {
                languageConfigs.CheckSettings();
            }
        }

        #endregion

        private IEnumerable<PanelSettings> GetAllPanelSettings()
        {
            return panelSettingsBySortingOrder.Values;
        }

        public PanelSettings GetPanelSetting(int sortingOrder)
        {
            return panelSettingsBySortingOrder[sortingOrder];
        }
    }
}