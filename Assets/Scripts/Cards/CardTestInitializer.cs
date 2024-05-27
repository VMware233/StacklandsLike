using System;
using StackLandsLike.GameCore;
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
            10.Repeat(() =>
            {
                var cardID = GamePrefabManager.GetRandomGamePrefab<ICardConfig>().id;
                CardGroupManager.CreateCardGroup(cardID);
            });
            
            GameStateManager.StartGame();
            
            onDone();
        }
    }
}