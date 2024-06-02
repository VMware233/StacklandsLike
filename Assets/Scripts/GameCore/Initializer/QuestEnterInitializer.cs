using System;
using StackLandsLike.Quests;
using UnityEngine.Scripting;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace StackLandsLike.GameCore
{
    [GameInitializerRegister(ServerRunningProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class QuestEnterInitializer : IGameInitializer
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