using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.ResourcesManagement;

namespace StackLandsLike.Cards
{
    public class CardCraftInfo
    {
        [ShowInInspector]
        public readonly ICardRecipe recipe;
        
        public int tick;
        
        public CardCraftInfo(ICardRecipe recipe)
        {
            this.recipe = recipe;
            tick = 0;
        }
    }
}