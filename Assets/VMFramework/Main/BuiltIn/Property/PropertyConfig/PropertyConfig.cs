using System;
using VMFramework.GameLogicArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Property
{
    public abstract partial class PropertyConfig : LocalizedGameTypedGamePrefab
    {
        protected override string idSuffix => "property";

        [LabelText("目标类型"), TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        [ShowInInspector]
        public virtual Type targetType => typeof(object);

        [LabelText("是否显示提示框"), TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public bool displayTooltip = true;

        [LabelText("图标"), TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        [PreviewField(50, ObjectFieldAlignment.Center)]
        [Required]
        public Sprite icon;

        public abstract string GetValueString(object target);

        public override void CheckSettings()
        {
            base.CheckSettings();

            if (icon == null)
            {
                Debug.LogWarning("缺失图标");
            }
        }
    }
}
