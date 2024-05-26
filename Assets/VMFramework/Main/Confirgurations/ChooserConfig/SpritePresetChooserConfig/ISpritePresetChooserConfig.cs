namespace VMFramework.Configuration
{
    public interface ISpritePresetChooserConfig : IChooserConfig<SpritePresetItem>
    {
        public ISpritePresetChooserConfig GetFlipChooserConfig(bool flipXReversed, bool flipYReversed);
    }
}