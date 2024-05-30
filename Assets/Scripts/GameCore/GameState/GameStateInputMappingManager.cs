using System;
using VMFramework.GameEvents;
using VMFramework.Procedure;

namespace StackLandsLike.GameCore
{
    [ManagerCreationProvider(nameof(GameManagerType.GameCore))]
    public sealed class GameStateInputMappingManager : ManagerBehaviour<GameStateInputMappingManager>, IManagerBehaviour
    {
        public void OnInitComplete(Action onDone)
        {
            GameEventManager.AddCallback(GameSetting.gameStateGeneralSetting.toggleGameStateGameEventID,
                ToggleGameState, GameEventPriority.TINY);
            
            GameEventManager.AddCallback(GameSetting.gameStateGeneralSetting.endGameStateGameEventID,
                EndGameState, GameEventPriority.TINY);
            
            onDone();
        }

        private void ToggleGameState(BoolInputGameEvent e)
        {
            GameStateManager.TogglePause();
        }

        private void EndGameState(BoolInputGameEvent e)
        {
            GameStateManager.EndGame();
        }
    }
}