#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    [DrawerPriority(0, 0.0, 5000)]
    public sealed class HideIfNullAttributeDrawer : OdinAttributeDrawer<HideIfNullAttribute>
    {
        public override bool CanDrawTypeFilter(Type type) => type.IsClass;

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (Property.ValueEntry.WeakSmartValue == null)
            {
                Property.State.Visible = false;
            }

            CallNextDrawer(label);
        }
    }
}
#endif