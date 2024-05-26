#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace VMFramework.OdinExtensions
{
    [AllowGUIEnabledForReadonly]
    [DrawerPriority(DrawerPriorityLevel.WrapperPriority)]
    public class PlaceholderTextAttributeDrawer : OdinAttributeDrawer<PlaceholderAttribute, string>
    {
        private static GUIStyle _rightAlignedGreyMiniLabel;

        private static GUIStyle RightAlignedGreyMiniLabel =>
            _rightAlignedGreyMiniLabel ??= new GUIStyle(SirenixGUIStyles.RightAlignedGreyMiniLabel)
            {
                alignment = TextAnchor.UpperRight,
                padding = new RectOffset(2, 2, 3, 2)
            };

        private static GUIStyle _leftAlignedGreyMiniLabel;

        private static GUIStyle LeftAlignedGreyMiniLabel =>
            _leftAlignedGreyMiniLabel ??= new GUIStyle(SirenixGUIStyles.LeftAlignedGreyMiniLabel)
            {
                alignment = TextAnchor.UpperLeft,
                padding = new RectOffset(2, 2, 3, 2)
            };

        private ValueResolver<string> labelResolver;

        protected override void Initialize()
        {
            labelResolver = ValueResolver.GetForString(Property, Attribute.Placeholder);
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            labelResolver.DrawError();

            CallNextDrawer(label);

            if (ValueEntry.SmartValue.IsNullOrWhitespace() || Attribute.AlwaysShow)
            {
                GUIHelper.PushGUIEnabled(true);
                if (Attribute.RightSide)
                {
                    GUI.Label(GUILayoutUtility.GetLastRect().HorizontalPadding(0.0f, 4.0f),
                        labelResolver.GetValue(), RightAlignedGreyMiniLabel);
                }
                else
                {
                    GUI.Label(
                        GUILayoutUtility.GetLastRect()
                            .HorizontalPadding(4.0f + (label == null ? 0 : GUIHelper.BetterLabelWidth), 0.0f),
                        labelResolver.GetValue(), LeftAlignedGreyMiniLabel);
                }

                GUIHelper.PopGUIEnabled();
            }
        }
    }
}
#endif