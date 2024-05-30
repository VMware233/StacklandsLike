using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using StackLandsLike.Quests;
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace StackLandsLike.UI
{
    public partial class QuestAndRecipeUIController
    {
        [ShowInInspector]
        private readonly Dictionary<IQuest, QuestEntryInfo> questEntries = new();
        
        [ShowInInspector]
        private VisualElement questContainer;
        
        private void OpenQuestPanel()
        {
            questContainer = rootVisualElement.Q(questAndRecipeUIPreset.questContainerName);
            
            foreach (var quest in QuestManager.currentQuests)
            {
                AddQuest(quest);
            }
            
            QuestManager.OnQuestStarted += AddQuest;
            QuestManager.OnQuestStopped += RemoveQuest;
        }

        private void CloseQuestPanel()
        {
            QuestManager.OnQuestStarted -= AddQuest;
            QuestManager.OnQuestStopped -= RemoveQuest;

            foreach (var quest in questEntries.Keys.ToList())
            {
                RemoveQuest(quest);
            }
            
            questEntries.Clear();
        }

        private void AddQuest(IQuest quest)
        {
            if (quest == null)
            {
                Debug.LogWarning("Quest is null.");
                return;
            }

            if (questEntries.ContainsKey(quest))
            {
                Debug.LogWarning($"Quest {quest.name} already exists in questEntries dictionary.");
                return;
            }
            
            var toggle = new Toggle
            {
                label = "",
                text = quest.name,
                focusable = false,
                value = quest.isDone
            };
            toggle.RegisterCallback<MouseUpEvent>(e =>
            {
                toggle.value = !toggle.value;
                e.StopPropagation();
            });
            
            questContainer.Add(toggle);
            
            if (quest.isDone == false)
            {
                toggle.SetAsFirstSibling();
            }
            
            questEntries.Add(quest, new QuestEntryInfo()
            {
                quest = quest,
                toggle = toggle
            });
            
            quest.OnQuestCompleted += OnQuestCompleted;
        }

        private void RemoveQuest(IQuest quest)
        {
            if (quest == null)
            {
                Debug.LogWarning("Quest is null.");
                return;
            }
            
            if (questEntries.TryGetValue(quest, out var entry) == false)
            {
                Debug.LogWarning($"Quest {quest.name} not found in questEntries dictionary.");
                return;
            }
            
            questContainer.Remove(entry.toggle);
            questEntries.Remove(quest);
            
            quest.OnQuestCompleted -= OnQuestCompleted;
        }

        private void OnQuestCompleted(IQuest quest)
        {
            if (questEntries.TryGetValue(quest, out var entry) == false)
            {
                Debug.LogWarning($"Quest {quest.name} not found in questEntries dictionary.");
                return;
            }
            
            entry.toggle.value = true;
            entry.toggle.SetAsLastSibling();
        }

        [Button]
        private void AddQuest([GamePrefabID] string taskID)
        {
            var quest = IGameItem.Create<IQuest>(taskID);
            
            AddQuest(quest);
        }
    }
}