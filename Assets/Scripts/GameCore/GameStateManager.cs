using System.Linq;
using Sirenix.OdinInspector;
using StackLandsLike.Cards;
using UnityEngine;
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

        [Button]
        public static void EndGame()
        {
            if (ProcedureManager.currentProcedureIDs.Contains(ServerRunningProcedure.ID) == false)
            {
                Debug.LogWarning("Cannot end game as server is not running.");
                return;
            }
            
            foreach (var cardGroup in CardGroupManager.GetActiveCardGroups().ToList())
            {
                CardGroupManager.DestroyCardGroup(cardGroup);
            }
            
            ProcedureManager.AddToSwitchQueue(ServerRunningProcedure.ID, MainMenuProcedure.ID);
        }

        public static void PauseGame()
        {
            LogicTickManager.StopTick();
        }
        
        public static void ResumeGame()
        {
            LogicTickManager.StartTick();
        }
    }
}