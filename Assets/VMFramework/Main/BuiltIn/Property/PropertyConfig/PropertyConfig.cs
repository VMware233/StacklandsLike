using System;
using VMFramework.GameLogicArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Property
{
    public abstract partial class PropertyConfig : LocalizedGameTypedGamePrefab
    {
        protected override string idSuffix => "property";

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        [ShowInInspector]
        public abstract Type targetType { get; }

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public bool displayTooltip = true;

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        [PreviewField(50, ObjectFieldAlignment.Center)]
        [Required]
        public Sprite icon;

        public abstract string GetValueString(object target);

        public override void CheckSettings()
        {
            base.CheckSettings();

            if (icon == null)
            {
                Debug.LogWarning($"{this} icon is not set.");
            }
        }
    }
}
