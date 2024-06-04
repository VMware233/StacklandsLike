using StackLandsLike.GameCore;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace StackLandsLike.Cards.Monsters
{
    [ManagerCreationProvider(nameof(GameManagerType.GameCore))]
    public sealed class MonsterSpawnManager : ManagerBehaviour<MonsterSpawnManager>
    {
        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();

            GameTimeManager.OnDayChanged += OnDayChanged;
        }

        private static void OnDayChanged(int day)
        {
            // switch (day)
            // {
            //     case 3:
            //         CreateMonster("spider_card");
            //         CreateMonster("spider_card");
            //         break;
            //     
            //     case 8:
            //         CreateMonster("boar_card");
            //         CreateMonster("spider_card");
            //         break;
            //     
            //     case 12:
            //         CreateMonster("stone_golem_card");
            //         break;
            //     
            //     case 15:
            //         CreateMonster("boar_card");
            //         CreateMonster("boar_card");
            //         break;
            //     
            //     case 20:
            //         CreateMonster("treeent_card");
            //         break;
            //     
            //     case 25:
            //         CreateMonster("bear_card");
            //         CreateMonster("bear_card");
            //         break;
            //     
            //     case 29:
            //         CreateMonster("dragon_card");
            //         break;
            // }
        }

        private static void CreateMonster(string id)
        {
            var monster = IGameItem.Create<ICreatureCard>(id);
            CardGroupManager.CreateCardGroup(monster, Vector2.zero);
        }
    }
}