using StackLandsLike.Cards;
using VMFramework.Containers;

namespace StackLandsLike.Quests
{
    public class CardCollectionQuest : Quest, IQuest
    {
        protected CardCollectionQuestConfig cardCollectionQuestConfig =>
            (CardCollectionQuestConfig)gamePrefab;
        
        void IQuest.OnQuestStarted()
        {
            CardGroupManager.OnCardGroupCreated += OnCardGroupCreated;
        }

        void IQuest.OnQuestStopped()
        {
            CardGroupManager.OnCardGroupCreated -= OnCardGroupCreated;
        }

        private void OnCardGroupCreated(CardGroup cardGroup)
        {
            var count = cardGroup.cardContainer.GetItemCount(cardCollectionQuestConfig.cardID);

            if (count >= 1)
            {
                SetDone();
            }
        }
    }
}