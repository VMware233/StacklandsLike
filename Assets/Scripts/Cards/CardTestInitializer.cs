using System;
using StackLandsLike.GameCore;
using UnityEngine.Scripting;
using VMFramework.Procedure;

namespace StackLandsLike.Cards
{
    /// <summary>
    /// 临时的加载器，卡牌测试用的
    /// </summary>
    [GameInitializerRegister(typeof(ServerLoadingProcedure))]
    [Preserve]
    public sealed class CardTestInitializer : IGameInitializer
    {
        void IInitializer.OnInit(Action onDone)
        {
            foreach (var config in GameSetting.cardGeneralSetting.initialCards)
            {
                var card = config.GenerateItem();
                CardGroupManager.CreateCardGroup(card);
            }
            
            onDone();
        }
    }
}