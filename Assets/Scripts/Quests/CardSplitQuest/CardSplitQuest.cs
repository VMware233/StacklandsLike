using StackLandsLike.Cards;
using StackLandsLike.UI;

namespace StackLandsLike.Quests
{
    public sealed class CardSplitQuest : Quest, IQuest
    {
        void IQuest.OnQuestStarted()
        {
            CardViewSplitManager.OnSplitCard += OnSplitCard;
        }

        private void OnSplitCard(ICard card, int oldCount, int newCount)
        {
            SetDone();
        }

        void IQuest.OnQuestStopped()
        {
            CardViewSplitManager.OnSplitCard -= OnSplitCard;
        }
    }
}