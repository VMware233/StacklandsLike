using System.Collections.Generic;

namespace VMFramework.ExtendedTilemap
{
    public partial class Rule
    {
        public IEnumerable<Rule> GenerateRules()
        {
            yield return GetClone(false, false);
            
            if (flipX)
            {
                yield return GetClone(true, false);
            }
            
            if (flipY)
            {
                yield return GetClone(false, true);
            }
            
            if (flipX && flipY)
            {
                yield return GetClone(true, true);
            }
        }
        
        public Rule GetClone(bool flipX, bool flipY)
        {
            var result = new Rule()
            {
                enable = enable,
                enableAnimation = enableAnimation,
                // animationSprites = animationSprites,
                // gap = gap,
                // autoPlayOnStart = autoPlayOnStart,
                upperLeft = upperLeft,
                upper = upper,
                upperRight = upperRight,
                left = left,
                right = right,
                lowerLeft = lowerLeft,
                lower = lower,
                lowerRight = lowerRight,
            };

            if (flipX)
            {
                (result.upperLeft, result.upperRight) = (result.upperRight, result.upperLeft);
                (result.left, result.right) = (result.right, result.left);
                (result.lowerLeft, result.lowerRight) = (result.lowerRight, result.lowerLeft);
            }

            if (flipY)
            {
                (result.upperLeft, result.lowerLeft) = (result.lowerLeft, result.upperLeft);
                (result.upper, result.lower) = (result.lower, result.upper);
                (result.upperRight, result.lowerRight) = (result.lowerRight, result.upperRight);
            }

            foreach (var layer in layers)
            {
                result.layers.Add(new()
                {
                    layer = layer.layer,
                    sprite = layer.sprite.GetFlipChooserConfig(flipX, flipY)
                });
            }

            return result;
        }
    }
}