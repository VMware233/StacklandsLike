namespace VMFramework.Configuration
{
    public class SingleSpritePresetChooserConfig
        : SingleValueChooserConfig<SpritePresetItem>, ISpritePresetChooserConfig
    {
        public ISpritePresetChooserConfig GetFlipChooserConfig(bool flipXReversed, bool flipYReversed)
        {
            var chooserConfig = new SingleSpritePresetChooserConfig
            {
                value = new SpritePresetItem()
                {
                    id = value.id,
                    flipX = flipXReversed ? !value.flipX : value.flipX,
                    flipY = flipYReversed ? !value.flipY : value.flipY
                }
            };

            return chooserConfig;
        }
    }
}