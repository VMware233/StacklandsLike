using System;
using UnityEngine.Scripting;
using VMFramework.Procedure;
using VMFramework.Timers;

namespace StackLandsLike.GameCore
{
    [GameInitializerRegister(typeof(ServerLoadingProcedure))]
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