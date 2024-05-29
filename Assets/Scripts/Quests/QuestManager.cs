using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using StackLandsLike.GameCore;
using VMFramework.Procedure;

namespace StackLandsLike.Quests
{
    [ManagerCreationProvider(nameof(GameManagerType.Quest))]
    public sealed class QuestManager : ManagerBehaviour<QuestManager>
    {
        [ShowInInspector]
        private static readonly HashSet<IQuest> _currentQuests = new();
        
        public static IReadOnlyCollection<IQuest> currentQuests => _currentQuests;

        public static event Action<IQuest> OnQuestStarted;
        public static event Action<IQuest> OnQuestStopped;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StartQuest(IQuest quest)
        {
            _currentQuests.Add(quest);
            OnQuestStarted?.Invoke(quest);
            quest.OnQuestStarted();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StopQuest(IQuest quest)
        {
            _currentQuests.Remove(quest);
            OnQuestStopped?.Invoke(quest);
            quest.OnQuestStopped();
        }
    }
}