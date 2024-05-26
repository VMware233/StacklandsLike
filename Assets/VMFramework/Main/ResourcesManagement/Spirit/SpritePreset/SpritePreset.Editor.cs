#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.ResourcesManagement
{
    public partial class SpritePreset
    {
        [HideLabel, HorizontalGroup(SPRITE_PREVIEW_GROUP)]
        [ShowInInspector]
        [ShowIf(nameof(hasPreloadFlipX))]
        [PreviewField(40, ObjectFieldAlignment.Center)]
        private Sprite preloadFlipXPreview => SpriteManager.GetSprite(id, FlipType2D.X);
        
        [HideLabel, HorizontalGroup(SPRITE_PREVIEW_GROUP)]
        [ShowInInspector]
        [ShowIf(nameof(hasPreloadFlipY))]
        [PreviewField(40, ObjectFieldAlignment.Center)]
        private Sprite preloadFlipYPreview => SpriteManager.GetSprite(id, FlipType2D.Y);
        
        [HideLabel, HorizontalGroup(SPRITE_PREVIEW_GROUP)]
        [ShowInInspector]
        [ShowIf(nameof(hasPreloadFlipXY))]
        [PreviewField(40, ObjectFieldAlignment.Center)]
        private Sprite preloadFlipXYPreview => SpriteManager.GetSprite(id, FlipType2D.XY);

        private bool hasPreloadFlipX => preloadFlipType.HasFlag(FlipType2D.X);
        
        private bool hasPreloadFlipY => preloadFlipType.HasFlag(FlipType2D.Y);
        
        private bool hasPreloadFlipXY => preloadFlipType.HasFlag(FlipType2D.XY);
    }
}
#endif