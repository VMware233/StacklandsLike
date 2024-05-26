using System;
using VMFramework.Core.Linq;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace StackLandsLike.Cards
{
    /// <summary>
    /// 临时的加载器，卡牌测试用的
    /// </summary>
    [GameInitializerRegister(typeof(GameInitializationProcedure))]
    public sealed class CardTestInitializer : IGameInitializer
    {
        void IInitializer.OnInitComplete(Action onDone)
        {
            5.Repeat(() =>
            {
                var cardID = GamePrefabManager.GetRandomGamePrefab<ICardConfig>().id;
                CardGroupManager.CreateCardGroup(cardID);
            });
        }
    }
}