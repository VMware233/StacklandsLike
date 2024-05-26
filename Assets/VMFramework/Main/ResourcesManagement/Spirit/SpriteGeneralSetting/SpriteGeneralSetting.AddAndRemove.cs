using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
#if UNITY_EDITOR
using VMFramework.GameLogicArchitecture.Editor;
#endif

namespace VMFramework.ResourcesManagement
{
    public partial class SpriteGeneralSetting
    {
        public void AddSpritePreset(Sprite sprite)
        {
            if (sprite == null)
            {
                return;
            }
            
            var id = sprite.name.ToSnakeCase();

            if (GamePrefabManager.ContainsGamePrefab(id))
            {
                Debug.LogWarning($"Sprite Preset with id {id} already exists.");
                return;
            }
            
            var spritePreset = new SpritePreset()
            {
                id = id,
                sprite = sprite
            };

#if UNITY_EDITOR
            if (Application.isPlaying == false)
            {
                GamePrefabWrapperCreator.CreateGamePrefabWrapper(spritePreset);
                
                return;
            }
#endif

            GamePrefabManager.RegisterGamePrefab(spritePreset);
        }

        public void RemoveSpritePreset(Sprite sprite)
        {
#if UNITY_EDITOR
            if (Application.isPlaying == false)
            {
                GamePrefabWrapperRemover.RemoveGamePrefabWrapperWhere<SpritePreset>(prefab =>
                    prefab.sprite == sprite);
            }
#endif
        }
    }
}