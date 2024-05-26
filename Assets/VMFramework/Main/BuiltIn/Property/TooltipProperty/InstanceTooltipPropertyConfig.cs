using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Property
{
    public class InstanceTooltipPropertyConfig : BaseConfig
    {
        [HideInInspector]
        public Type filterType;

        [LabelText("属性")]
        [ValueDropdown(nameof(GetPropertyNameList))]
        [IsNotNullOrEmpty]
        public string propertyID;

        [HideInEditorMode]
        public PropertyConfig propertyConfig;

        [LabelText("是否静态")]
        public bool isStatic;

        #region GUI

        private IEnumerable<ValueDropdownItem> GetPropertyNameList()
        {
            return GameCoreSetting.propertyGeneralSetting.GetPropertyNameList(filterType);
        }

        #endregion

        public InstanceTooltipPropertyConfigRuntime ConvertToRuntime()
        {
            return new InstanceTooltipPropertyConfigRuntime()
            {
                propertyConfig = propertyConfig,
                isStatic = isStatic
            };
        }

        protected override void OnInit()
        {
            base.OnInit();

            propertyConfig = GamePrefabManager.GetGamePrefabStrictly<PropertyConfig>(propertyID);
        }
    }
}