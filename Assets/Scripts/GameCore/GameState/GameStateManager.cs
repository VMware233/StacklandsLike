using System;
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
        
        public static bool isVictory { get; private set; } = false;
        
        public static bool isPaused { get; private set; } = false;
        
        public static bool isSpeed2x { get; private set; } = false;
        
        public static event Action<bool> OnPauseStateChange;
        
        public static event Action<bool> OnSpeedStateChange;
        
        [Button]
        public static void StartGame()
        {
            if (ProcedureManager.HasCurrentProcedure(MainMenuProcedure.ID) == false)
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
            
            ProcedureManager.EnterProcedure(MainMenuProcedure.ID, ServerRunningProcedure.ID);
            
            isGameRunning = true;
        }

        [Button]
        public static void EnterSettlement(bool isVictory)
        {
            if (ProcedureManager.HasCurrentProcedure(ServerRunningProcedure.ID) == false)
            {
                Debug.LogWarning("Cannot enter settlement as server is not running.");
                return;
            }
            
            ProcedureManager.EnterProcedure(ServerRunningProcedure.ID, SettlementProcedure.ID);
            
            GameStateManager.isVictory = isVictory;
        }

        [Button]
        public static void EndGame()
        {
            if (ProcedureManager.HasCurrentProcedure(SettlementProcedure.ID) == false)
            {
                Debug.LogWarning("Cannot end game as settlement is not running.");
                return;
            }
            
            ProcedureManager.EnterProcedure(SettlementProcedure.ID, MainMenuProcedure.ID);

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
            
            isPaused = true;
            
            OnPauseStateChange?.Invoke(true);
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
            
            isPaused = false;
            
            OnPauseStateChange?.Invoke(false);
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

        [Button]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Speed1x()
        {
            if (isGameRunning == false)
            {
                Debug.LogWarning("Cannot set speed as game is not running.");
                return;
            }
            
            LogicTickManager.SetTickGap(1/60f);
            
            isSpeed2x = false;
            
            OnSpeedStateChange?.Invoke(false);
        }

        [Button]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Speed2x()
        {
            if (isGameRunning == false)
            {
                Debug.LogWarning("Cannot set speed as game is not running.");
                return;
            }
            
            LogicTickManager.SetTickGap(1/120f);
            
            isSpeed2x = true;
            
            OnSpeedStateChange?.Invoke(true);
        }
        
        [Button]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToggleSpeed()
        {
            if (isGameRunning == false)
            {
                Debug.LogWarning("Cannot toggle speed as game is not running.");
                return;
            }
            
            if (isSpeed2x)
            {
                Speed1x();
            }
            else
            {
                Speed2x();
            }
        }
    }
}