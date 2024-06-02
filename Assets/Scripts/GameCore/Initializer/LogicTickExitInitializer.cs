using System;
using VMFramework.Procedure;
using VMFramework.Timers;

namespace StackLandsLike.GameCore
{
    [GameInitializerRegister(ServerRunningProcedure.ID, ProcedureLoadingType.OnExit)]
    public sealed class LogicTickExitInitializer : IGameInitializer
    {
        void IInitializer.OnBeforeInit(Action onDone)
        {
            LogicTickManager.StopTick();
            onDone();
        }
    }
}