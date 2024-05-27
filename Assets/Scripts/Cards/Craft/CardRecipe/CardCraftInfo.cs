using Sirenix.OdinInspector;

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