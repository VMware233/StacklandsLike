using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using VMFramework.Procedure;

namespace StackLandsLike.GameCore
{
    /// <summary>
    /// 临时的加载器，卡牌测试用的
    /// </summary>
    [GameInitializerRegister(ServerRunningProcedure.ID, ProcedureLoadingType.OnEnter)]
    [Preserve]
    public class BattleInitilizer : IGameInitializer
    {
        void IInitializer.OnPostInit(Action onDone)
        {
            BattleTurnSystem.OnPostInit();
            
            onDone();
        }
        void IInitializer.OnInitComplete(Action onDone)
        {
            BloodUpdate.OnInitComplete();
            BattleUIManage.OnInitComplete();
            onDone();

        }
       
    }
}
