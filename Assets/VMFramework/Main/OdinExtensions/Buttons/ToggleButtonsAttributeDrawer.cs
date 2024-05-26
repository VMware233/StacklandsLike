#if UNITY_EDITOR
using System;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
#pragma warning disable

namespace VMFramework.OdinExtensions
{
    public class ToggleButtonsAttributeDrawer : OdinAttributeDrawer<ToggleButtonsAttribute>
    {
        private static readonly bool DO_MANUAL_COLORING = UnityVersion.IsVersionOrGreater(2019, 3);

        private static readonly Color ACTIVE_COLOR = EditorGUIUtility.isProSkin
            ? Color.white
            : new Color(0.802f, 0.802f, 0.802f, 1f);

        private static readonly Color INACTIVE_COLOR = EditorGUIUtility.isProSkin
            ? new Color(0.75f, 0.75f, 0.75f, 1f)
            : Color.white;

        private Color?[] m_selectionColors;
        private Color? m_color;

        private ValueResolver<string>[] m_nameGetters;
        private ValueResolver<string>[] m_tooltipGetters;
        private ValueResolver<Texture>[] m_iconGetters;
        private ValueResolver<Color>[] m_colorGetters;

        private GUIContent[] m_buttonContents;
        private float[] m_nameSizes;
        private int m_rows = 1;
        private float m_previousControlRectWidth;

        private bool m_needUpdate;
        private float m_totalNamesSize;

        public override bool CanDrawTypeFilter(Type type)
        {
            return type == typeof(bool);
        }

        protected override void Initialize()
        {
            base.Initialize();

            m_nameGetters = new[]
            {
                ValueResolver.GetForString(Property, Attribute.m_trueText),
                ValueResolver.GetForString(Property, Attribute.m_falseText)
            };

            m_tooltipGetters = new[]
            {
                ValueResolver.GetForString(Property, Attribute.m_trueTooltip),
                ValueResolver.GetForString(Property, Attribute.m_falseTooltip)
            };

            m_iconGetters = new[]
            {
                ValueResolver.Get(Property, Attribute.m_trueIcon, (Texture)null),
                ValueResolver.Get(Property, Attribute.m_falseIcon, (Texture)null)
            };

            m_colorGetters = new[]
            {
                ValueResolver.Get(Property, Attribute.m_trueColor, Color.white),
                ValueResolver.Get(Property, Attribute.m_falseColor, Color.white)
            };

            m_buttonContents = new GUIContent[2];

            for (var i = 0; i < 2; i++)
            {
                m_buttonContents[i] = new GUIContent(m_nameGetters[i].GetValue(), m_iconGetters[i].GetValue(),
                    m_tooltipGetters[i].GetValue());
            }

            m_nameSizes = m_buttonContents.Select(x => SirenixGUIStyles.MiniButtonMid.CalcSize(x).x)
                .ToArray();

            m_rows = 1;

            GUIHelper.RequestRepaint();

            if (!DO_MANUAL_COLORING)
            {
                return;
            }

            m_selectionColors = new Color?[2];
            m_color = new Color?();
        }

        private void UpdateNames()
        {
            UpdateName(0);
            UpdateName(1);

            // Add extra padding to smaller button
            if (m_nameSizes[0] > m_nameSizes[1])
            {
                m_nameSizes[1] = Mathf.Lerp(m_nameSizes[1], m_nameSizes[0],
                    Attribute.m_sizeCompensationCompensation);
            }
            else
            {
                m_nameSizes[0] = Mathf.Lerp(m_nameSizes[0], m_nameSizes[1],
                    Attribute.m_sizeCompensationCompensation);
            }

            m_totalNamesSize = m_nameSizes[0] + m_nameSizes[1];
        }

        private void UpdateName(int index)
        {
            var newText = m_nameGetters[index].GetValue();
            var newIcon = m_iconGetters[index].GetValue();

            m_buttonContents[index].tooltip = m_tooltipGetters[index].GetValue();

            var needUpdate = (m_buttonContents[index].text != newText) |
                             (m_buttonContents[index].image != newIcon);

            m_needUpdate |= needUpdate;

            m_buttonContents[index].text = newText;
            m_buttonContents[index].image = newIcon;
            m_nameSizes[index] = SirenixGUIStyles.MiniButton.CalcSize(m_buttonContents[index]).x;
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            foreach (var valueResolver in m_nameGetters)
            {
                valueResolver.DrawError();
            }

            foreach (var valueResolver in m_iconGetters)
            {
                valueResolver.DrawError();
            }

            foreach (var valueResolver in m_tooltipGetters)
            {
                valueResolver.DrawError();
            }

            foreach (var valueResolver in m_colorGetters)
            {
                valueResolver.DrawError();
            }

            if (Event.current.type == EventType.Layout)
            {
                UpdateNames();
            }

            var currentValue = (bool)Property.ValueEntry.WeakSmartValue;

            var buttonIndex = 0;

            var rect = new Rect();

            SirenixEditorGUI.GetFeatureRichControlRect(label,
                Mathf.CeilToInt(EditorGUIUtility.singleLineHeight * (Attribute.m_singleButton ? 1 : m_rows)),
                out var _, out var _, out var valueRect);

            if (Attribute.m_singleButton)
            {
                DrawSingleButton(currentValue, valueRect);
            }
            else
            {
                valueRect.height = EditorGUIUtility.singleLineHeight;

                rect = valueRect;

                for (var rowIndex = 0; rowIndex < m_rows; ++rowIndex)
                {
                    valueRect.xMin = rect.xMin;
                    valueRect.xMax = rect.xMax;

                    var xMax = valueRect.xMax;

                    for (var columnIndex = 0; columnIndex < (m_rows == 2 ? 1 : 2); ++columnIndex)
                    {
                        valueRect.width = (int)rect.width * m_nameSizes[buttonIndex] / m_totalNamesSize;
                        valueRect = DrawButton(buttonIndex, currentValue, valueRect, columnIndex, rowIndex,
                            xMax);
                        ++buttonIndex;
                    }

                    valueRect.y += valueRect.height;
                }
            }

            if (Event.current.type != EventType.Repaint ||
                (m_previousControlRectWidth == rect.width && !m_needUpdate) || Attribute.m_singleButton)
            {
                return;
            }

            m_previousControlRectWidth = rect.width;

            m_rows = rect.width < m_nameSizes[0] + m_nameSizes[1] + 6f ? 2 : 1;

            m_needUpdate = false;
        }

        private void DrawSingleButton(bool currentValue, Rect valueRect)
        {
            if (DO_MANUAL_COLORING)
            {
                m_color = UpdateColor(m_color, currentValue ? ACTIVE_COLOR : INACTIVE_COLOR);
            }

            var style = currentValue ? SirenixGUIStyles.MiniButtonSelected : SirenixGUIStyles.MiniButton;

            GUI.backgroundColor = m_colorGetters[currentValue ? 0 : 1].GetValue();

            if (DO_MANUAL_COLORING)
            {
                GUIHelper.PushColor(m_color.Value * GUI.color);
            }

            valueRect.x--;
            valueRect.xMax += 2;

            if (GUI.Button(valueRect, m_buttonContents[currentValue ? 0 : 1], style))
            {
                Property.ValueEntry.WeakSmartValue = !currentValue;
            }

            if (DO_MANUAL_COLORING)
            {
                GUIHelper.PopColor();
            }

            GUI.backgroundColor = Color.white;
        }

        private Rect DrawButton(int buttonIndex, bool currentValue, Rect valueRect, int columnIndex,
            int rowIndex, float xMax)
        {
            var selectionColor = new Color?();
            var buttonValue = buttonIndex == 0;

            if (DO_MANUAL_COLORING)
            {
                var color = currentValue == buttonValue ? ACTIVE_COLOR : INACTIVE_COLOR;
                selectionColor = m_selectionColors[buttonValue ? 0 : 1];

                selectionColor = UpdateColor(selectionColor, color);

                m_selectionColors[buttonValue ? 0 : 1] = selectionColor;
            }

            var position = valueRect;
            GUIStyle style;

            if (columnIndex == 0 && columnIndex == (m_rows == 2 ? 1 : 2) - 1)
            {
                style = currentValue ? SirenixGUIStyles.MiniButtonSelected : SirenixGUIStyles.MiniButton;
                --position.x;
                position.xMax = xMax + 1f;
            }
            else if (buttonIndex == 0)
            {
                style = currentValue
                    ? SirenixGUIStyles.MiniButtonLeftSelected
                    : SirenixGUIStyles.MiniButtonLeft;
            }
            else
            {
                style = currentValue
                    ? SirenixGUIStyles.MiniButtonRightSelected
                    : SirenixGUIStyles.MiniButtonRight;
                position.xMax = xMax;
            }

            GUI.backgroundColor = m_colorGetters[buttonIndex].GetValue();

            if (DO_MANUAL_COLORING)
            {
                GUIHelper.PushColor(selectionColor.Value * GUI.color);
            }

            if (GUI.Button(position, m_buttonContents[buttonIndex], style))
            {
                Property.ValueEntry.WeakSmartValue = buttonValue;
            }

            GUI.backgroundColor = Color.white;

            if (DO_MANUAL_COLORING)
            {
                GUIHelper.PopColor();
            }


            valueRect.x += valueRect.width;

            return valueRect;
        }

        private static Color? UpdateColor(Color? nullable, Color color)
        {
            if (!nullable.HasValue)
            {
                nullable = color;
            }
            else if (nullable.Value != color && Event.current.type == EventType.Layout)
            {
                nullable = color.MoveTowards(color, EditorTimeHelper.Time.DeltaTime * 4f);

                GUIHelper.RequestRepaint();
            }

            return nullable;
        }
    }
}
#endif