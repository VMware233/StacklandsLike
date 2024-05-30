using StackLandsLike.Cards;

namespace StackLandsLike.Quests
{
    public class CardCraftQuest : Quest, IQuest
    {
        protected CardCraftQuestConfig cardCraftQuestConfig => (CardCraftQuestConfig)gamePrefab;
        
        public void OnQuestStarted()
        {
            CardCraftManager.OnRecipeCompleted += OnRecipeCompleted;
        }

        public void OnQuestStopped()
        {
            CardCraftManager.OnRecipeCompleted -= OnRecipeCompleted;
        }
        
        private void OnRecipeCompleted(CardGroup cardGroup, ICardRecipe recipe)
        {
            if (recipe.id == cardCraftQuestConfig.recipeID)
            {
                SetDone();
            }
        }
    }
}