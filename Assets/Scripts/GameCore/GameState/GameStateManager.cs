using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using StackLandsLike.Cards;
using StackLandsLike.Quests;
using UnityEngine;
using VMFramework.Procedure;
using VMFramework.Timers;

namespace StackLandsLike.GameCore
{
    [ManagerCreationProvider(nameof(GameManagerType.GameCore))]
    public sealed class GameStateManager : ManagerBehaviour<GameStateManager>
    {
        public static bool isGameRunning { get; private set; } = false;
        
        [Button]
        public static void StartGame()
        {
            if (ProcedureManager.currentProcedureIDs.Contains(MainMenuProcedure.ID) == false)
            {
                Debug.LogWarning("Cannot start game as main menu is not running.");
                return;
            }
            
            GameTimeManager.Init(new GameTimeInitInfo()
            {
                day = 0,
                tickInDay = 0,
                ticksPerDay = GameSetting.gameTimeGeneralSetting.totalTicksPerDay
            });
            
            ProcedureManager.AddToSwitchQueue(MainMenuProcedure.ID, ServerLoadingProcedure.ID);
            
            isGameRunning = true;
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
            
            QuestManager.StopAllQuests();
            
            LogicTickManager.StopTick();
            
            ProcedureManager.AddToSwitchQueue(ServerRunningProcedure.ID, MainMenuProcedure.ID);

            isGameRunning = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PauseGame()
        {
            if (isGameRunning == false)
            {
                Debug.LogWarning("Cannot pause game as it is not running.");
                return;
            }
            
            LogicTickManager.StopTick();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResumeGame()
        {
            if (isGameRunning == false)
            {
                Debug.LogWarning("Cannot resume game as it is not running.");
                return;
            }
            
            LogicTickManager.StartTick();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TogglePause()
        {
            if (isGameRunning == false)
            {
                Debug.LogWarning("Cannot toggle pause as it is not running.");
                return;
            }
            
            if (LogicTickManager.IsTicking())
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
}