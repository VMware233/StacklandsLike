#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Editor
{
    public readonly struct Icon
    {
        public static readonly Icon None = new(SdfIconType.None);
        
        public readonly EditorIconType iconType;
        
        public readonly SdfIconType sdfIconType;

        public readonly Sprite spriteIcon;
        
        public readonly Texture textureIcon;

        public Icon(SdfIconType sdfIconType)
        {
            iconType = EditorIconType.SdfIcon;
            this.sdfIconType = sdfIconType;
            spriteIcon = null;
            textureIcon = null;
        }

        public Icon(Sprite spriteIcon)
        {
            iconType = EditorIconType.Sprite;
            sdfIconType = SdfIconType.None;
            this.spriteIcon = spriteIcon;
            textureIcon = null;
        }

        public Icon(Texture textureIcon)
        {
            iconType = EditorIconType.Texture;
            sdfIconType = SdfIconType.None;
            spriteIcon = null;
            this.textureIcon = textureIcon;
        }
        
        public static implicit operator Icon(SdfIconType sdfIconType) => new(sdfIconType);
        
        public static implicit operator Icon(Sprite spriteIcon) => new(spriteIcon);
        
        public static implicit operator Icon(Texture textureIcon) => new(textureIcon);
    }
}
#endif