#if UNITY_EDITOR
using UnityEngine;
using Sirenix.Utilities.Editor;

namespace VMFramework.Core.Editor
{
    public static class IMGUIUtility
    {
        private static readonly Color ERROR_RECT_BACKGROUND_COLOR =
            new(1, 0, 0, 0.15f);

        private static readonly Color ERROR_RECT_SHADOW_COLOR = new(0, 0, 0, 0.3f);
        private static readonly Color ERROR_RECT_BAND_COLOR = new(1, 0, 0, 0.5f);

        private static readonly Color WARNING_RECT_BACKGROUND_COLOR =
            new(0.79f, 0.52f, 0f, 0.15f);

        private static readonly Color WARNING_RECT_SHADOW_COLOR = new(0, 0, 0, .3f);

        private static readonly Color WARNING_RECT_BAND_COLOR =
            new(1f, 0.62f, 0.22f, 0.5f);

        public static void DrawErrorRect(this Rect rect)
        {
            SirenixEditorGUI.DrawSolidRect(rect,
                ERROR_RECT_BACKGROUND_COLOR);
            SirenixEditorGUI.DrawBorders(rect, 0, 0, 1, 0,
                ERROR_RECT_SHADOW_COLOR);
            SirenixEditorGUI.DrawBorders(rect, 3, 0, 0, 0,
                ERROR_RECT_BAND_COLOR);
        }

        public static void DrawWarningRect(this Rect rect)
        {
            SirenixEditorGUI.DrawSolidRect(rect,
                WARNING_RECT_BACKGROUND_COLOR);
            SirenixEditorGUI.DrawBorders(rect, 0, 0, 1, 0,
                WARNING_RECT_SHADOW_COLOR);
            SirenixEditorGUI.DrawBorders(rect, 3, 0, 0, 0,
                WARNING_RECT_BAND_COLOR);
        }
    }
}

#endif