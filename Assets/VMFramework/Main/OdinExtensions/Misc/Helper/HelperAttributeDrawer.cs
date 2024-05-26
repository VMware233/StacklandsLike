#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.WSA;

namespace VMFramework.OdinExtensions
{
    public class HelperAttributeDrawer : OdinAttributeDrawer<HelperAttribute>
    {
        private ValueResolver<string> urlResolver;

        protected override void Initialize()
        {
            urlResolver = ValueResolver.GetForString(Property, Attribute.URL);
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (urlResolver.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(urlResolver.ErrorMessage);
            }

            var url = urlResolver.GetValue();

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            CallNextDrawer(label);
            GUILayout.EndVertical();
            GUIHelper.PushGUIEnabled(true);

            var rect = EditorGUILayout.GetControlRect(
                false, EditorGUIUtility.singleLineHeight,
                GUILayout.Width(12f)).AlignCenter(12f);

            if (SirenixEditorGUI.SDFIconButton(rect, url,
                    SdfIconType.QuestionCircleFill, style: SirenixGUIStyles.Label))
            {
                Launcher.LaunchUri(url, false);
            }

            GUIHelper.PopGUIEnabled();
            GUILayout.EndHorizontal();
        }
    }
}
#endif