using StackLandsLike.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;
using VMFramework.Timers;

namespace StackLandsLike.GameCore
{
    //IGameInitializer
    public sealed class EnemyGenerate : MonoBehaviour
    {
        public GameObject bloodBar;
         void Start()
        {
            GameTimeManager.OnDayChanged += CreateMonsterByDay;
        }
   
        void CreateMonsterByDay(int day)
        {
           // ulong day = LogicTickManager.tick;
            switch (day)
            {         
                case 2://2蜘蛛
                    CreateMonster(1,"spider_card");
                    break;
                case 3:
                    CreateMonster(1, "pig_card");
                    CreateMonster(1, "spider_card");
                    break;
                case 12:
                    CreateMonster(1, "pig_card");
                    CreateMonster(4, "spider_card");
                    break;
                case 20:
                    CreateMonster(3, "pig_card");
                    break;
                case 24:
                    CreateMonster(2, "pig_card");
                    CreateMonster(1, "bear_card");
                    break;
                case 26:
                    CreateMonster(2, "bear_card");
                    break;
                case 28:
                    CreateMonster(3, "bear_card");
                    break;
                case 30:
                    CreateMonster(4, "bear_card");
                    break;
                default:
                    break;
            }
        }
        void CreateMonster(int count,string monsterName)
        {
            for(int i=0;i<count;i++)
            { 
                var card = IGameItem.Create<ICard>(monsterName);
                CardGroup cardgroup=CardGroupManager.CreateCardGroup(card);
                cardgroup.gameObject.tag = "EnemyUnit";
                cardgroup.AddComponent<UnitStats>().SetStates((ICreatureCard)card);
                GameObject enemyBloodBar = Instantiate(bloodBar) as GameObject;
                enemyBloodBar.transform.SetParent(GameObject.Find("BloodBarGroup").transform, false);
                enemyBloodBar.GetComponent<BloodUpdate>().owner = cardgroup.gameObject;
                BattleUIManage.blood.Add(cardgroup.gameObject, enemyBloodBar);
                enemyBloodBar.SetActive(true);
                BattleEnemy(cardgroup.gameObject);
            }

        }
        void BattleEnemy(GameObject enemy)
        {
            BattleTurnSystem.enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
            BattleTurnSystem.playerUnits.AddRange(GameObject.FindGameObjectsWithTag("PlayerUnit"));
            BattleTurnSystem.battleUnits.Add(enemy);
            //开始战斗
            BattleTurnSystem.ToBattle();
        }
       
    }
}