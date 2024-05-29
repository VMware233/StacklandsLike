using System;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Quests
{
    public interface IQuest : IVisualGameItem
    {
        public bool isDone { get; }
        
        public event Action<IQuest> OnQuestCompleted;

        public void OnQuestStarted()
        {
            
        }

        public void OnQuestStopped()
        {
            
        }
    }
}