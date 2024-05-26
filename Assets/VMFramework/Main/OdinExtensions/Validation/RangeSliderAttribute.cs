using System;
using System.Diagnostics;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEditor;
#endif

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]

    public class RangeSliderAttribute : Attribute
    {
        public float MinValue;

        public float MaxValue;

        public string MinValueGetter;

        public string MaxValueGetter;

        public bool ShowFields;

        #region Constructor

        public RangeSliderAttribute(float minValue, float maxValue,
            bool showFields = true)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            ShowFields = showFields;
        }

        public RangeSliderAttribute(string minValueGetter, string maxValueGetter,
            bool showFields = true)
        {
            MinValueGetter = minValueGetter;
            MaxValueGetter = maxValueGetter;
            ShowFields = showFields;
        }

        public RangeSliderAttribute(float minValue, string maxValueGetter,
            bool showFields = true)
        {
            MinValue = minValue;
            MaxValueGetter = maxValueGetter;
            ShowFields = showFields;
        }

        public RangeSliderAttribute(string minValueGetter, float maxValue,
            bool showFields = true)
        {
            MinValueGetter = minValueGetter;
            MaxValue = maxValue;
            ShowFields = showFields;
        }

        #endregion
    }

    public interface IRangeSliderValueProvider
    {
        public float min { get; set; }

        public float max { get; set; }
    }

#if UNITY_EDITOR

    [DrawerPriority(DrawerPriorityLevel.WrapperPriority)]
    public sealed class
        RangeSliderAttributeDrawer : OdinAttributeDrawer<RangeSliderAttribute>
    {
        private ValueResolver<float> minGetter;

        private ValueResolver<float> maxGetter;

        protected override void Initialize()
        {
            minGetter = ValueResolver.Get(Property, Attribute.MinValueGetter,
                Attribute.MinValue);
            maxGetter = ValueResolver.Get(Property, Attribute.MaxValueGetter,
                Attribute.MaxValue);
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (Property.ValueEntry.WeakSmartValue is not IRangeSliderValueProvider
                valueProvider)
            {
                SirenixEditorGUI.ErrorMessageBox(
                    $"{Property.Name} must implement {nameof(IRangeSliderValueProvider)}");

                CallNextDrawer(label);
                return;
            }

            Vector2 limits = new(minGetter.GetValue(), maxGetter.GetValue());
            EditorGUI.BeginChangeCheck();
            Vector2 smartValue = SirenixEditorFields.MinMaxSlider(label,
                new Vector2(valueProvider.min, valueProvider.max), limits,
                Attribute.ShowFields);
            if (EditorGUI.EndChangeCheck())
            {
                valueProvider.min = smartValue.x;
                valueProvider.max = smartValue.y;
            }
        }
    }

#endif
}
