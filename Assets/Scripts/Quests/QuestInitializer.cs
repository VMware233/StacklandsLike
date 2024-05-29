using System;
using UnityEngine.Scripting;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace StackLandsLike.Quests
{
    [GameInitializerRegister(typeof(ServerLoadingProcedure))]
    [Preserve]
    public sealed class QuestInitializer : IGameInitializer
    {
        void IInitializer.OnInitComplete(Action onDone)
        {
            foreach (var config in GamePrefabManager.GetAllActiveGamePrefabs<IQuestConfig>())
            {
                var task = IGameItem.Create<IQuest>(config.id);
                
                QuestManager.StartQuest(task);
            }
            
            onDone();
        }
    }
}