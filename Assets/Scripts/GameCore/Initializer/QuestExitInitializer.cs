using System;
using StackLandsLike.Quests;
using VMFramework.Procedure;

namespace StackLandsLike.GameCore
{
    [GameInitializerRegister(ServerRunningProcedure.ID, ProcedureLoadingType.OnExit)]
    public sealed class QuestExitInitializer : IGameInitializer
    {
        void IInitializer.OnInit(Action onDone)
        {
            QuestManager.StopAllQuests();
            
            onDone();
        }
    }
}