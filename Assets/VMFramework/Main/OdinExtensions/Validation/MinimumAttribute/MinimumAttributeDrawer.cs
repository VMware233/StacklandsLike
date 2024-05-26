#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.OdinExtensions
{
    public sealed class MinimumAttributeDrawer : OdinAttributeDrawer<MinimumAttribute>
    {
        private ValueResolver<double> minValueGetter;

        protected override void Initialize() =>
            minValueGetter = ValueResolver.Get(Property, Attribute.MinExpression,
                Attribute.MinValue);

        protected override void DrawPropertyLayout(GUIContent label)
        {
            var value = Property.ValueEntry.WeakSmartValue;

            if (value is not IMinimumValueProvider minimumValueProvider)
            {
                SirenixEditorGUI.ErrorMessageBox(
                    $"{Property.ValueEntry.TypeOfValue.GetNiceName()}没有实现{typeof(IMinimumValueProvider)}");
                CallNextDrawer(label);
                return;
            }

            if (minValueGetter.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(minValueGetter.ErrorMessage);
                CallNextDrawer(label);
            }
            else
            {
                double min = minValueGetter.GetValue();

                minimumValueProvider.ClampByMinimum(min);

                CallNextDrawer(label);
            }
        }
    }
}
#endif