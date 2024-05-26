using System;
using System.Collections.Generic;
using System.Linq;
using VMFramework.Core;
using VMFramework.Core.Linq;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Property
{
    public static class TooltipPropertyManager
    {
        private static readonly Dictionary<string, List<InstanceTooltipPropertyConfigRuntime>>
            tooltipPropertyConfigsRuntimeDict = new();

        public static IEnumerable<InstanceTooltipPropertyConfigRuntime> GetTooltipPropertyConfigsRuntime(
            string gamePrefabID)
        {
            if (tooltipPropertyConfigsRuntimeDict.TryGetValue(gamePrefabID, out var result))
            {
                return result;
            }

            return Enumerable.Empty<InstanceTooltipPropertyConfigRuntime>();
        }

        public static void AddTooltipPropertyConfigRuntime(string gamePrefabID,
            InstanceTooltipPropertyConfigRuntime tooltipPropertyConfigRuntime)
        {
            if (tooltipPropertyConfigsRuntimeDict.TryGetValue(gamePrefabID, out var result))
            {
                result.Add(tooltipPropertyConfigRuntime);
            }
            else
            {
                result = new List<InstanceTooltipPropertyConfigRuntime> { tooltipPropertyConfigRuntime };
                tooltipPropertyConfigsRuntimeDict.Add(gamePrefabID, result);
            }
        }

        public static IEnumerable<InstanceTooltipPropertyConfigRuntime> GetTooltipPropertyConfigsRuntime(
            Type currentInstanceType)
        {
            var result = new List<InstanceTooltipPropertyConfigRuntime>();

            foreach (var (instanceType, tooltipPropertyConfig) in
                     GameCoreSetting.tooltipPropertyGeneralSetting.tooltipPropertyConfigs)
            {
                if (currentInstanceType.IsDerivedFrom(instanceType, true))
                {
                    foreach (var configRuntime in tooltipPropertyConfig.tooltipPropertyConfigsRuntime)
                    {
                        result.Add(configRuntime);
                    }
                }
            }

            return result.Distinct(config => config.propertyConfig);
        }
    }
}