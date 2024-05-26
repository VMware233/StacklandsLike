using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EnumsNET;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Core
{
    public static class SpriteUtility
    {
        #region PhysicsShape

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Vector2> GetPhysicsShape(this Sprite sprite, int shapeIndex)
        {
            var shapePoints = new List<Vector2>();
            sprite.GetPhysicsShape(0, shapePoints);
            return shapePoints;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Vector2> GetPhysicsShape(this Sprite sprite)
        {
            return GetPhysicsShape(sprite, 0);
        }

        #endregion

        #region Flip

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Sprite Flip(this Sprite sprite, FlipType2D flipType,
            SpritePivotFlipType spritePivotOperationType)
        {
            if (flipType.GetFlagCount() > 1)
            {
                throw new System.Exception("FlipType2D 最多只能包含一个方向");
            }

            var (flipX, flipY) = flipType.ToBool();
            return Flip(sprite, flipX, flipY, spritePivotOperationType);
        }

        public static Sprite Flip(this Sprite sprite, bool flipX, bool flipY,
            SpritePivotFlipType spritePivotOperationType)
        {
            sprite.AssertIsNotNull(nameof(sprite));

            var textureIsReadable = sprite.texture.isReadable;
            textureIsReadable.AssertIsTrue(nameof(textureIsReadable));

            if (flipX == false && flipY == false)
            {
                return sprite;
            }

            var texture = sprite.texture;

            var spriteRect = sprite.rect;

            var spriteWidth = (int)spriteRect.width;
            var spriteHeight = (int)spriteRect.height;

            var spriteMinX = (int)spriteRect.min.x;
            var spriteMinY = (int)spriteRect.min.y;

            var result = new Texture2D(spriteWidth, spriteHeight)
                {
                    filterMode = texture.filterMode,
                    wrapMode = texture.wrapMode,
                };

            for (int x = 0; x < spriteWidth; x++)
            {
                for (int y = 0; y < spriteHeight; y++)
                {
                    var originX = x;
                    var originY = y;

                    if (flipX)
                    {
                        originX = spriteWidth - x - 1;
                    }

                    if (flipY)
                    {
                        originY = spriteHeight - y - 1;
                    }

                    var color = texture.GetPixel(spriteMinX + originX,
                        spriteMinY + originY);
                    result.SetPixel(x, y, color);
                }
            }

            result.Apply();

            Vector2 resultPivot = sprite.pivot.Divide(new Vector2(spriteWidth, spriteHeight));

            if (spritePivotOperationType == SpritePivotFlipType.Flip)
            {
                if (flipX)
                {
                    resultPivot.x = 1 - resultPivot.x;
                }

                if (flipY)
                {
                    resultPivot.y = 1 - resultPivot.y;
                }
            }

            return Sprite.Create(result,
                new Rect(Vector2.zero, new Vector2(spriteWidth, spriteHeight)),
                resultPivot, sprite.pixelsPerUnit);
        }

        #endregion
    }
}
