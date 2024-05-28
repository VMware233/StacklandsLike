using System;
using VMFramework.Core.Linq;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace StackLandsLike.Cards
{
    /// <summary>
    /// 临时的加载器，卡牌测试用的
    /// </summary>
    [GameInitializerRegister(typeof(ServerLoadingProcedure))]
    public sealed class CardTestInitializer : IGameInitializer
    {
        void IInitializer.OnInit(Action onDone)
        {
            10.Repeat(() =>
            {
                var cardID = GamePrefabManager.GetRandomGamePrefab<ICardConfig>().id;
                CardGroupManager.CreateCardGroup(cardID);
            });
            
            onDone();
        }
    }
}