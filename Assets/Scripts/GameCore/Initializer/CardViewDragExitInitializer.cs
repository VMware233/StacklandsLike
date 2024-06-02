using System;
using StackLandsLike.UI;
using VMFramework.Procedure;

namespace StackLandsLike.GameCore
{
    [GameInitializerRegister(ServerRunningProcedure.ID, ProcedureLoadingType.OnExit)]
    public sealed class CardViewDragExitInitializer : IGameInitializer
    {
        void IInitializer.OnInitComplete(Action onDone)
        {
            CardViewDragManager.DisableDrag();
            
            onDone();
        }
    }
}