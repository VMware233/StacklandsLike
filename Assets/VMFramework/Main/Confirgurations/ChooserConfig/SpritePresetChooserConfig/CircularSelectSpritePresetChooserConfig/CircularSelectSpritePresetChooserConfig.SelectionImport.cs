#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using VMFramework.Core.Linq;

namespace VMFramework.Configuration
{
    public partial class CircularSelectSpritePresetChooserConfig
    {
        private IEnumerable<Sprite> selectedSprites =>
            Selection.objects.Select<Object, Sprite>();

        private int selectedSpritesCount => selectedSprites.Count();

        private bool showAddSelectedSpritesButton => selectedSpritesCount > 0;

        [Button("添加选中的Sprites")]
        [ShowIf(nameof(showAddSelectedSpritesButton))]
        private void AddSelectedSprites()
        {
            foreach (var selectedSprite in selectedSprites)
            {
                if (selectedSprite == null)
                {
                    continue;
                }

                var values = items.Select(item => item.value.sprite).ToList();

                if (values.Contains(selectedSprite) == false)
                {
                    items.Add(new()
                    {
                        value = new(selectedSprite),
                        times = 1
                    });
                }

                OnItemsChangedGUI();
            }
        }
    }
}
#endif