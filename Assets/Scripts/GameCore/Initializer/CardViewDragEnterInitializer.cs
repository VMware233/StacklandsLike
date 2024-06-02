using System;
using StackLandsLike.UI;
using VMFramework.Procedure;

namespace StackLandsLike.GameCore
{
    [GameInitializerRegister(ServerRunningProcedure.ID, ProcedureLoadingType.OnEnter)]
    public sealed class CardViewDragEnterInitializer : IGameInitializer
    {
        void IInitializer.OnInitComplete(Action onDone)
        {
            CardViewDragManager.EnableDrag();
            
            onDone();
        }
    }
}