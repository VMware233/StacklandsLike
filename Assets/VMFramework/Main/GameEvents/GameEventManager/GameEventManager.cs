using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.GameEvents
{
    [ManagerCreationProvider(ManagerType.EventCore)]
    public sealed partial class GameEventManager : ManagerBehaviour<GameEventManager>
    {
        [ShowInInspector]
        private static Dictionary<string, IGameEvent> allGameEvents = new();

        public static event Action<IGameEvent> OnGameEventRegistered;
        public static event Action<IGameEvent> OnGameEventUnregistered;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Register(string gameEventID)
        {
            var gameEvent = IGameItem.Create<IGameEvent>(gameEventID);

            if (gameEvent == null)
            {
                Debug.LogError(
                    $"GameEventManager: Could not find {nameof(IGameEvent)} with ID {gameEventID}");
                return;
            }
            
            Register(gameEvent);
        }
        
        public static void Register(IGameEvent gameEvent)
        {
            if (gameEvent == null)
            {
                Debug.LogError("GameEventManager: Cannot register null game event.");
                return;
            }
            
            if (allGameEvents.TryAdd(gameEvent.id, gameEvent) == false)
            {
                Debug.LogError($"Game Event with ID: {gameEvent.id} already exists.");
                return;
            }
            
            OnGameEventRegistered?.Invoke(gameEvent);
        }

        public static void Unregister(string gameEventID)
        {
            if (allGameEvents.Remove(gameEventID, out var gameEvent) == false)
            {
                Debug.LogWarning($"Game Event with ID: {gameEventID} does not exist.");
                return;
            }
            
            OnGameEventUnregistered?.Invoke(gameEvent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unregister(IGameEvent gameEvent)
        {
            if (gameEvent == null)
            {
                Debug.LogError("GameEventManager: Cannot unregister null game event.");
                return;
            }
            Unregister(gameEvent.id);
        }
    }
}