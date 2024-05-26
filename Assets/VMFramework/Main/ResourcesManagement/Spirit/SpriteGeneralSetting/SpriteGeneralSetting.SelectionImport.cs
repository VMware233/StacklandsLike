#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core.Linq;

namespace VMFramework.ResourcesManagement
{
    public partial class SpriteGeneralSetting
    {
        protected override void OnHandleRegisterGameItemPrefabFromSelectedObject(
            Object obj, bool checkUnique)
        {
            if (obj is not Sprite sprite)
            {
                return;
            }

            if (checkUnique)
            {
                if (SpriteManager.HasSpritePreset(sprite))
                {
                    Debug.LogWarning($"为了放置重复，忽略了{sprite.name}的添加");
                    return;
                }
            }

            AddSpritePreset(sprite);
        }

        protected override void OnHandleUnregisterGameItemPrefabFromSelectedObject(Object obj)
        {
            if (obj is not Sprite sprite)
            {
                return;
            }

            RemoveSpritePreset(sprite);
        }

        protected override bool ShowRegisterGameItemPrefabFromSelectionButton(
            IEnumerable<Object> objects)
        {
            return objects.AnyIs<Sprite>();
        }
    }
}
#endif