using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Procedure;

namespace VMFramework.GameEvents
{
    [ManagerCreationProvider(ManagerType.EventCore)]
    public sealed class GameEventUpdateManager : ManagerBehaviour<GameEventUpdateManager>
    {
        [ShowInInspector]
        private HashSet<IUpdateableGameEvent> updateableGameEvents = new();
        
        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();
            
            GameEventManager.OnGameEventRegistered += OnGameEventRegistered;
            GameEventManager.OnGameEventUnregistered += OnGameEventUnregistered;
        }

        private void Update()
        {
            foreach (var updateableGameEvent in updateableGameEvents)
            {
                if (updateableGameEvent.isEnabled == false)
                {
                    continue;
                }
                
                updateableGameEvent.Update();
            }
        }

        private void OnGameEventRegistered(IGameEvent gameEvent)
        {
            if (gameEvent is IUpdateableGameEvent updateableGameEvent)
            {
                updateableGameEvents.Add(updateableGameEvent);
            }
        }
        
        private void OnGameEventUnregistered(IGameEvent gameEvent)
        {
            if (gameEvent is IUpdateableGameEvent updateableGameEvent)
            {
                updateableGameEvents.Remove(updateableGameEvent);
            }
        }
    }
}