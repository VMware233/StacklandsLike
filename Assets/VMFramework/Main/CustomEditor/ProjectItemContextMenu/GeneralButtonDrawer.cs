#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using UnityEngine;
using VMFramework.Core.Editor;

namespace VMFramework.Editor.ProjectItemContextMenu
{
    public abstract class GeneralButtonDrawer : IButtonDrawer
    {
        public abstract bool CanDrawButton(ProjectItem item);
        
        protected abstract Icon icon { get; }
        
        protected abstract string tooltip { get; }

        public bool Button(Rect rect)
        {
            if (icon.iconType == EditorIconType.SdfIcon)
            {
                return SirenixEditorGUI.SDFIconButton(rect, tooltip, icon.sdfIconType, style: GUIStyle.none);
            }

            Texture texture = null;

            if (icon.iconType == EditorIconType.Texture)
            {
                texture = icon.textureIcon;
            }
            else if (icon.iconType == EditorIconType.Sprite)
            {
                texture = icon.spriteIcon.GetAssetPreview();
            }

            return GUI.Button(rect, new GUIContent(texture, tooltip));
        }
    }
}
#endif