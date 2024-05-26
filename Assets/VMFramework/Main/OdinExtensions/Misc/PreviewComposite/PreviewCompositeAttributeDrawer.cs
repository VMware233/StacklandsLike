#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.OdinExtensions
{
    [DrawerPriority(0, 0, 50)]
    public class PreviewCompositeAttributeDrawer : OdinAttributeDrawer<PreviewCompositeAttribute>
    {
        private ValueResolver<string> suffixResolver;

        private PreviewCompositeSettingsAttribute settings;

        protected override void Initialize()
        {
            settings = Property.GetAttribute<PreviewCompositeSettingsAttribute>();

            if (settings != null)
            {
                suffixResolver =
                    ValueResolver.GetForString(Property, settings.Suffix);
            }
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (suffixResolver is { HasError: true })
            {
                SirenixEditorGUI.ErrorMessageBox(suffixResolver.ErrorMessage);
            }

            if (label == null)
            {
                CallNextDrawer(null);
                return;
            }

            string previewContent = "";
            var value = Property.ValueEntry.WeakSmartValue;

            if (value != null)
            {
                previewContent = value.ToString();
            }

            if (previewContent.IsNullOrEmpty() == false)
            {
                if (suffixResolver != null)
                {
                    previewContent += suffixResolver.GetValue();
                }
            }

            if (previewContent.IsNullOrEmpty() == false)
            {
                previewContent = previewContent.Replace("\n", " ");
            }

            GUILayout.BeginVertical();
            CallNextDrawer(label);
            GUILayout.EndVertical();

            GUIHelper.PushGUIEnabled(enabled: true);
            Rect position = GUILayoutUtility.GetLastRect();
            position = position.TakeFromTop(EditorGUIUtility.singleLineHeight)
                .HorizontalPadding(GUIHelper.BetterLabelWidth + 4f, 8f);
            GUI.Label(position, previewContent);
            GUIHelper.PopGUIEnabled();
        }
    }
}
#endif