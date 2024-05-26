using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.Core.Linq;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Property
{
    public partial class TooltipPropertyConfig : BaseConfig, IIDOwner<Type>
    {
        #region Configs

        [LabelText("实例类型")]
#if UNITY_EDITOR
        [OnValueChanged(nameof(OnInstanceTypeChangedGUI))]
        [IsNotNullOrEmpty]
        [TypeValueDropdown(typeof(IGameItem), IncludingInterfaces = false, IncludingAbstract = true,
            IncludingGeneric = false)]
#endif
        [SerializeField]
        private Type _instanceType;
        
        [LabelText("属性")]
#if UNITY_EDITOR
        [OnCollectionChanged(nameof(OnInstanceTypeChangedGUI))]
#endif
        [SerializeField]
        private List<InstanceTooltipPropertyConfig> tooltipPropertyConfigs = new();

        #endregion

        #region Properties

        public Type instanceType
        {
            init => _instanceType = value;
            get => _instanceType;
        }

        public IEnumerable<InstanceTooltipPropertyConfigRuntime> tooltipPropertyConfigsRuntime
        {
            init
            {
                tooltipPropertyConfigs.Clear();
                
                foreach (var configRuntime in value)
                {
                    var config = new InstanceTooltipPropertyConfig()
                    {
                        propertyID = configRuntime.propertyConfig.id,
                        propertyConfig = configRuntime.propertyConfig,
                        isStatic = configRuntime.isStatic
                    };
                    
                    tooltipPropertyConfigs.Add(config);
                }
            }
            get
            {
                foreach (var config in tooltipPropertyConfigs)
                {
                    yield return config.ConvertToRuntime();
                }
            }
        }

        #endregion

        #region Interface Implementation

        Type IIDOwner<Type>.id => instanceType;

        #endregion

        public override void CheckSettings()
        {
            base.CheckSettings();
        
            foreach (var tooltipPropertyConfig in tooltipPropertyConfigs)
            {
                tooltipPropertyConfig.propertyID.AssertIsNotNullOrEmpty(
                    $"{nameof(tooltipPropertyConfig)}." +
                    $"{nameof(tooltipPropertyConfig.propertyID)}");
            }
        
            if (tooltipPropertyConfigs.ContainsSame(config => config.propertyID))
            {
                Debug.LogWarning("默认的提示框属性设置有重复的属性ID");
            }
        
            foreach (var config in tooltipPropertyConfigs)
            {
                config.CheckSettings();
            }
        }
        
        protected override void OnInit()
        {
            base.OnInit();
        
            foreach (var config in tooltipPropertyConfigs)
            {
                config.Init();
            }
        }
    }
}