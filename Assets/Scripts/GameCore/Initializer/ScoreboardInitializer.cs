using System;
using UnityEngine.Scripting;
using VMFramework.Procedure;

namespace StackLandsLike.GameCore
{
    [GameInitializerRegister(ServerRunningProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class ScoreboardInitializer : IGameInitializer
    {
        void IInitializer.OnBeforeInit(Action onDone)
        {
            Scoreboard.ResetScoreboard();
            onDone();
        }
    }
}