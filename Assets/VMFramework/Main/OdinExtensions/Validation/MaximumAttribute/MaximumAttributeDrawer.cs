#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.OdinExtensions
{
    public sealed class MaximumAttributeDrawer : OdinAttributeDrawer<MaximumAttribute>
    {
        private ValueResolver<double> maxValueGetter;

        protected override void Initialize() =>
            maxValueGetter = ValueResolver.Get(Property, Attribute.MaxExpression,
                Attribute.MaxValue);

        protected override void DrawPropertyLayout(GUIContent label)
        {
            var value = Property.ValueEntry.WeakSmartValue;

            if (value is not IMaximumValueProvider minimumValueProvider)
            {
                SirenixEditorGUI.ErrorMessageBox(
                    $"{Property.ValueEntry.TypeOfValue.GetNiceName()}没有实现{typeof(IMaximumValueProvider)}");
                CallNextDrawer(label);
                return;
            }

            if (maxValueGetter.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(maxValueGetter.ErrorMessage);
                CallNextDrawer(label);
            }
            else
            {
                double max = maxValueGetter.GetValue();

                minimumValueProvider.ClampByMaximum(max);

                CallNextDrawer(label);
            }
        }
    }
}
#endif