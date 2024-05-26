#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public static class IconUtility
    {
        public static void Add(this OdinMenuTree tree, string path, object instance, Icon icon)
        {
            switch (icon.iconType)
            {
                case EditorIconType.Sprite:
                    tree.Add(path, instance, icon.spriteIcon);
                    break;
                case EditorIconType.SdfIcon:
                    tree.Add(path, instance, icon.sdfIconType);
                    break;
                case EditorIconType.Texture:
                    tree.Add(path, instance, icon.textureIcon);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static bool IsNull(this Icon icon)
        {
            switch (icon.iconType)
            {
                case EditorIconType.Sprite:
                    return icon.spriteIcon == null;
                case EditorIconType.SdfIcon:
                    return icon.sdfIconType == SdfIconType.None;
                case EditorIconType.Texture:
                    return icon.textureIcon == null;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static Texture GetTexture(this Icon icon)
        {
            if (icon.iconType == EditorIconType.Sprite)
            {
                return icon.spriteIcon.GetAssetPreview();
            }

            if (icon.iconType == EditorIconType.Texture)
            {
                return icon.textureIcon;
            }

            return null;
        }
    }
}
#endif