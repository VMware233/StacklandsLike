using EnumsNET;
using VMFramework.GameLogicArchitecture;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.ResourcesManagement
{
    public partial class SpritePreset : GameTypedGamePrefab
    {
        protected const string SPRITE_PREVIEW_GROUP =
            TAB_GROUP_NAME + "/" + BASIC_SETTING_CATEGORY + "/Sprite Preview Group";

        protected const string FLIP_GROUP = TAB_GROUP_NAME + "/" + BASIC_SETTING_CATEGORY + "/Flip Group";

        [HideLabel, HorizontalGroup(SPRITE_PREVIEW_GROUP)]
        [PreviewField(40, ObjectFieldAlignment.Center)]
        public Sprite sprite;
        
        [HorizontalGroup(FLIP_GROUP)]
        public FlipType2D preloadFlipType = FlipType2D.None;

        [HorizontalGroup(FLIP_GROUP)]
        [SerializeField]
        private SpritePivotFlipType spritePivotFlipType = SpritePivotFlipType.NoChange;

        #region Init

        public override bool isPreInitializationRequired => true;

        public override bool isInitializationRequired => false;

        public override bool isPostInitializationRequired => false;

        public override bool isInitializationCompleteRequired => false;

        protected override void OnPreInit()
        {
            base.OnPreInit();

            foreach (var flipType in preloadFlipType.GetFlags())
            {
                SpriteManager.GetSprite(id, flipType);
            }
        }

        #endregion

        public Sprite GenerateSprite(FlipType2D flipType)
        {
            if (sprite is null)
            {
                return null;
            }

            if (flipType == FlipType2D.None)
            {
                return sprite;
            }

            var resultSprite = sprite.Flip(flipType, spritePivotFlipType);
            resultSprite.name = id;

            return resultSprite;
        }
    }
}
