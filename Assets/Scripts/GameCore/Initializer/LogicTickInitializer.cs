using System;
using UnityEngine.Scripting;
using VMFramework.Procedure;
using VMFramework.Timers;

namespace StackLandsLike.GameCore
{
    [GameInitializerRegister(ServerRunningProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class LogicTickInitializer : IGameInitializer
    {
        void IInitializer.OnInitComplete(Action onDone)
        {
            LogicTickManager.StartTick();
            onDone();
        }
    }
}