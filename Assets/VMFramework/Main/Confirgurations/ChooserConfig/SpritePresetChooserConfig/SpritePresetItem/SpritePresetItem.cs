using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;
using VMFramework.ResourcesManagement;

namespace VMFramework.Configuration
{
    public sealed partial class SpritePresetItem : BaseConfig
    {
        private const string SPRITE_PREVIEW_GROUP = "SpritePreview";
        
        private const string SPRITE_PREVIEW_LEFT_GROUP = SPRITE_PREVIEW_GROUP + "/SpritePreviewLeft";
        
        private const string SPRITE_PREVIEW_FLIP_GROUP = SPRITE_PREVIEW_GROUP + "/SpritePreviewLeft/Flip";

        [field: HideLabel, HorizontalGroup(SPRITE_PREVIEW_GROUP, 200), VerticalGroup(SPRITE_PREVIEW_LEFT_GROUP)]
        [field: GamePrefabID(typeof(SpritePreset))]
        [field: SerializeField]
        public string spritePresetID { get; private set; }

        public string id
        {
            get => spritePresetID;
            init => spritePresetID = value;
        }

        [field: LabelText("X轴翻转"), LabelWidth(50), HorizontalGroup(SPRITE_PREVIEW_FLIP_GROUP)]
        [field: SerializeField]
        public bool flipX { get; init; } = false;

        [field: LabelText("Y轴翻转"), LabelWidth(50), HorizontalGroup(SPRITE_PREVIEW_FLIP_GROUP)]
        [field: SerializeField]
        public bool flipY { get; init; } = false;

        [HideLabel, HorizontalGroup(SPRITE_PREVIEW_GROUP)]
        [PreviewField(40, ObjectFieldAlignment.Right)]
        [ShowInInspector]
        public Sprite sprite
        {
            get => SpriteManager.GetSprite(spritePresetID, (flipX, flipY).ToFlipType2D());
#if UNITY_EDITOR
            private set
            {
                if (value == null)
                {
                    spritePresetID = null;
                }

                if (SpriteManager.HasSpritePreset(value) == false)
                {
                    GameCoreSetting.spriteGeneralSetting.AddSpritePreset(value);
                }

                spritePresetID = SpriteManager.GetSpritePreset(value)?.id;
            }
#endif
        }

        #region Constructor

        public SpritePresetItem() : this(null) { }

        public SpritePresetItem(Sprite sprite)
        {
            if (sprite == null)
            {
                return;
            }

            if (SpriteManager.HasSpritePreset(sprite) == false)
            {
                GameCoreSetting.spriteGeneralSetting.AddSpritePreset(sprite);
            }

            var spritePreset = SpriteManager.GetSpritePreset(sprite);

            spritePresetID = spritePreset?.id;
        }

        #endregion

        #region Conversion

        public static implicit operator Sprite(SpritePresetItem item)
        {
            return item?.sprite;
        }

        #endregion
    }
}