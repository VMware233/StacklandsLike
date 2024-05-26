#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Core.Linq;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.ExtendedTilemap
{
    public partial class ExtendedRuleTile : IGameEditorMenuTreeNode
    {
        Icon IGameEditorMenuTreeNode.icon
        {
            get
            {
                var defaultIcon = GetIconFromSpriteLayers(defaultSpriteLayers);

                if (defaultIcon != null)
                {
                    return defaultIcon;
                }
                
                if (ruleSet.IsNullOrEmpty())
                {
                    return Icon.None;
                }
                    
                var lastRule = ruleSet[^1];
                    
                return GetIconFromSpriteLayers(lastRule.layers);
            }
        }

        private Sprite GetIconFromSpriteLayers(IList<SpriteLayer> spriteLayers)
        {
            if (spriteLayers.Count == 0)
            {
                return null;
            }
            
            return spriteLayers[0].sprite?.GetAvailableValues().FirstOrDefault()?.sprite;
        }
    }
}
#endif