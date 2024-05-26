using System.Collections.Generic;

namespace VMFramework.Configuration
{
    public partial class WeightedSelectSpritePresetChooserConfig
        : WeightedSelectChooserConfig<SpritePresetItem>, ISpritePresetChooserConfig
    {
        public ISpritePresetChooserConfig GetFlipChooserConfig(bool flipXReversed, bool flipYReversed)
        {
            var config = new WeightedSelectSpritePresetChooserConfig();
            
            var newItems = new List<WeightedSelectItemConfig<SpritePresetItem>>();

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
                
                newItems.Add(new WeightedSelectItemConfig<SpritePresetItem>()
                {
                    value = spritePresetItem,
                    ratio = item.ratio
                });
            }
            
            config.items = newItems;
            
            return config;
        }
    }
}