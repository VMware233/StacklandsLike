using Sirenix.OdinInspector;
using VMFramework.Procedure;
using VMFramework.Timers;

namespace StackLandsLike.GameCore
{
    [ManagerCreationProvider(nameof(GameManagerType.GameCore))]
    public sealed class GameStateManager : ManagerBehaviour<GameStateManager>
    {
        [Button]
        public static void StartGame()
        {
            GameTimeManager.Init(new GameTimeInitInfo()
            {
                day = 0,
                tickInDay = 0,
                ticksPerDay = GameSetting.gameTimeGeneralSetting.totalTicksPerDay
            });
            
            ProcedureManager.AddToSwitchQueue(MainMenuProcedure.ID, ServerLoadingProcedure.ID);
        }
    }
}