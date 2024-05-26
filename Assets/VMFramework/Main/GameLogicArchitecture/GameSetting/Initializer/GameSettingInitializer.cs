using System;
using UnityEngine.Scripting;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    [GameInitializerRegister(typeof(CoreInitializationProcedure))]
    [Preserve]
    public sealed class GameSettingInitializer : IGameInitializer
    {
        void IInitializer.OnInit(Action onDone)
        {
            GameCoreSetting.Init();
            onDone();
        }
    }
}
