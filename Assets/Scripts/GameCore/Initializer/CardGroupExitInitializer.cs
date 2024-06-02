using System;
using System.Linq;
using StackLandsLike.Cards;
using VMFramework.Procedure;

namespace StackLandsLike.GameCore
{
    [GameInitializerRegister(SettlementProcedure.ID, ProcedureLoadingType.OnExit)]
    public sealed class CardGroupExitInitializer : IGameInitializer
    {
        void IInitializer.OnInit(Action onDone)
        {
            foreach (var cardGroup in CardGroupManager.GetActiveCardGroups().ToList())
            {
                CardGroupManager.DestroyCardGroup(cardGroup);
            }
            
            onDone();
        }
    }
}