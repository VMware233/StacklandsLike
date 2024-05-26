using System;
using System.Collections.Generic;
using VMFramework.UI;
using VMFramework.GameLogicArchitecture;
using VMFramework.Core;
using Sirenix.OdinInspector;
using Newtonsoft.Json;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Property
{
    public sealed partial class PropertyGeneralSetting : GamePrefabGeneralSetting
    {
        #region Categories

        public const string TOOLTIP_SETTING_CATEGORY = "提示框设置";

        public const string PROPERTY_SETTING_CATEGORY = "属性设置";

        #endregion

        #region Metadata

        public override string gameItemName => "Property";

        public override Type baseGamePrefabType => typeof(PropertyConfig);

        #endregion

        [LabelText("属性字典"), TabGroup(TAB_GROUP_NAME, PROPERTY_SETTING_CATEGORY)]
        [ShowInInspector]
        [ReadOnly]
        private static Dictionary<Type, List<PropertyConfig>> propertyConfigs = new();

        [LabelText("提示框"), TabGroup(TAB_GROUP_NAME, TOOLTIP_SETTING_CATEGORY)]
        [GamePrefabID(typeof(UIToolkitTracingTooltipPreset))]
        [IsNotNullOrEmpty]
        [JsonProperty]
        public string tooltipID;

        #region Init & Check

        protected override void OnPreInit()
        {
            base.OnPreInit();

            propertyConfigs.Clear();

            foreach (var propertyConfig in GamePrefabManager.GetAllGamePrefabs<PropertyConfig>())
            {
                if (propertyConfigs.ContainsKey(propertyConfig.targetType) == false)
                {
                    propertyConfigs[propertyConfig.targetType] = new();
                }

                propertyConfigs[propertyConfig.targetType].Add(propertyConfig);
            }
        }

        #endregion

        [Button(nameof(GetPropertyConfigs)), TabGroup(TAB_GROUP_NAME, PROPERTY_SETTING_CATEGORY)]
        public static List<PropertyConfig> GetPropertyConfigs(Type targetType)
        {
            var result = new List<PropertyConfig>();

            if (propertyConfigs.Count == 0)
            {
                Debug.LogWarning("还没加载属性字典");
                return result;
            }

            foreach (var (type, propertyConfig) in propertyConfigs)
            {
                if (type.IsAssignableFrom(targetType))
                {
                    result.AddRange(propertyConfig);
                }
            }

            return result;
        }

        public IEnumerable<ValueDropdownItem> GetPropertyNameList(Type targetType)
        {
            if (targetType == null)
            {
                yield break;
            }
            
            foreach (var config in GamePrefabManager.GetAllGamePrefabs<PropertyConfig>())
            {
                if (config is { isActive: true } && targetType.IsDerivedFrom(config.targetType, true))
                {
                    yield return new(config.name, config.id);
                }
            }
        }
    }
}
