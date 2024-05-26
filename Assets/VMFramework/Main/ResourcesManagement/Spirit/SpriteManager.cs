using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.ResourcesManagement
{
    [ManagerCreationProvider(ManagerType.ResourcesCore)]
    public class SpriteManager : SerializedMonoBehaviour
    {
        [LabelText("Sprite缓存")]
        [ShowInInspector]
        private static readonly Dictionary<int, Sprite> spriteCache = new();
        
        [LabelText("SpriteID缓存")]
        [ShowInInspector]
        private static readonly Dictionary<Sprite, string> spriteIDLookup = new();

        #region Get Sprite

        public static Sprite GetSprite(string spritePresetID, FlipType2D flipType)
        {
            if (spritePresetID.IsNullOrEmpty())
            {
                return null;
            }

            var id = HashCode.Combine(spritePresetID, flipType);
            
            if (spriteCache.TryGetValue(id, out var existedSprite))
            {
                return existedSprite;
            }
            
            var spritePreset = GamePrefabManager.GetGamePrefab<SpritePreset>(spritePresetID);

            if (spritePreset == null)
            {
                return null;
            }
            
            var sprite = spritePreset.GenerateSprite(flipType);
            
            spriteCache.Add(id, sprite);
            spriteIDLookup.Add(sprite, spritePresetID);
            
            return sprite;
        }

        #endregion

        #region Sprite Preset

        public static bool HasSpritePreset(Sprite sprite)
        {
            if (sprite == null)
            {
                return false;
            }
            
            if (spriteIDLookup.TryGetValue(sprite, out var spritePresetID))
            {
                if (GamePrefabManager.ContainsGamePrefab(spritePresetID))
                {
                    return true;
                }
                
                spriteIDLookup.Remove(sprite);
                    
                return false;
            }

            var spritePreset = GamePrefabManager.GetAllGamePrefabs<SpritePreset>()
                .FirstOrDefault(prefab => prefab.sprite == sprite);

            if (spritePreset == null)
            {
                return false;
            }
            
            spriteIDLookup.Add(sprite, spritePreset.id);

            return true;
        }
        
        public static SpritePreset GetSpritePreset(Sprite sprite)
        {
            if (sprite == null)
            {
                return null;
            }
            
            if (spriteIDLookup.TryGetValue(sprite, out var spritePresetID))
            {
                var existedSpritePreset = GamePrefabManager.GetGamePrefab<SpritePreset>(spritePresetID);
                
                if (existedSpritePreset == null)
                {
                    spriteIDLookup.Remove(sprite);
                }
                
                return existedSpritePreset;
            }

            var spritePreset = GamePrefabManager.GetAllGamePrefabs<SpritePreset>()
                .FirstOrDefault(prefab => prefab.sprite == sprite);
            
            if (spritePreset == null)
            {
                return null;
            }
            
            spriteIDLookup.Add(sprite, spritePreset.id);

            return spritePreset;
        }

        #endregion
    }
}
