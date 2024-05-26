using System.Collections.Generic;

namespace VMFramework.Configuration
{
    public partial class CircularSelectSpritePresetChooserConfig
        : CircularSelectChooserConfig<SpritePresetItem>, ISpritePresetChooserConfig
    {
        public ISpritePresetChooserConfig GetFlipChooserConfig(bool flipXReversed, bool flipYReversed)
        {
            var config = new CircularSelectSpritePresetChooserConfig();
            
            var newItems = new List<CircularSelectItemConfig<SpritePresetItem>>();

            foreach (var item in items)
            {
                bool flipX = item.value.flipX;
                bool flipY = item.value.flipY;

                var spritePresetItem = new SpritePresetItem()
                {
                    id = item.value.id,
                    flipX = flipXReversed ? !flipX : flipX,
                    flipY = flipYReversed ? !flipY : flipY,
                };
                
                newItems.Add(new CircularSelectItemConfig<SpritePresetItem>()
                {
                    value = spritePresetItem,
                    times = item.times,
                });
            }
            
            config.items = newItems;
            
            return config;
        }
    }
}