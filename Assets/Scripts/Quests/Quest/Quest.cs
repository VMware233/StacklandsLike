using System;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Quests
{
    public class Quest : VisualGameItem, IQuest
    {
        [ShowInInspector]
        public bool isDone { get; private set; }

        public event Action<IQuest> OnQuestCompleted; 
        
        protected void SetDone()
        {
            if (isDone)
            {
                return;
            }
            
            isDone = true;
            OnQuestCompleted?.Invoke(this);
        }
    }
}