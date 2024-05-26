using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.Core;

namespace VMFramework.ExtendedTilemap
{
    public partial class SpriteLayer : BaseConfig
    {
        [LabelText("层级")]
        public int layer;

        [HideLabel]
        public ISpritePresetChooserConfig sprite;

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            sprite.AssertIsNotNull(nameof(sprite));
            sprite.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            sprite.Init();
        }
    }
}