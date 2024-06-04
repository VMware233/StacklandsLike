using System;
using StackLandsLike.Cards;
using UnityEngine;
using UnityEngine.Scripting;
using VMFramework.Core;
using VMFramework.Procedure;

namespace StackLandsLike.GameCore
{
    /// <summary>
    /// 临时的加载器，卡牌测试用的
    /// </summary>
    [GameInitializerRegister(ServerRunningProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public sealed class CardTestInitializer : IGameInitializer
    {       
        void IInitializer.OnInit(Action onDone)
        {
            foreach (var config in GameSetting.cardGeneralSetting.initialCards)
            {
                var card = config.GenerateItem();
                CardGroup cardgroup=CardGroupManager.CreateCardGroup(card);
                if (card.id == "person_card")
                {
                   cardgroup.gameObject.tag = "PlayerUnit";
                   cardgroup.AddComponent<UnitStats>().SetStates((ICreatureCard)card); ;  //给人类和怪物加上战斗的脚本

                }
                else if (card.id == "pig_card")                         //test!
                {
                    cardgroup.gameObject.tag = "EnemyUnit";
                    cardgroup.AddComponent<UnitStats>().SetStates((ICreatureCard)card); ;                  
          ;
                }            

            }
            onDone();
        }
    }
}