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
            LogicTickManager.StartTick();
        }
    }
}