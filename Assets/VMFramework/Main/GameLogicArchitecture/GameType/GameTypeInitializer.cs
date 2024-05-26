using System;
using UnityEngine.Scripting;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    [GameInitializerRegister(typeof(CoreInitializationProcedure))]
    [Preserve]
    public class GameTypeInitializer : IGameInitializer
    {
        public void OnBeforeInit(Action onDone)
        {
            GameCoreSetting.gameTypeGeneralSetting.CheckGameTypeInfo();
            GameCoreSetting.gameTypeGeneralSetting.InitGameTypeInfo();
            
            onDone();
        }
    }
}