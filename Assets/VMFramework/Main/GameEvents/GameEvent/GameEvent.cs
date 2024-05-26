using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.GameEvents
{
    public abstract partial class GameEvent<TGameEvent> : GameItem, IGameEvent<TGameEvent>
        where TGameEvent : GameEvent<TGameEvent>
    {
        [ShowInInspector]
        private readonly SortedDictionary<int, HashSet<Action<TGameEvent>>> callbacks = new();
        [ShowInInspector]
        private readonly Dictionary<Delegate, int> callbacksLookup = new();

        [ShowInInspector]
        private int disabledCount = 0;
        
        public bool isEnabled => disabledCount <= 0;

        public event IGameEvent.EnabledChangedEventHandler OnEnabledChangedEvent;

        protected override void OnCreate()
        {
            base.OnCreate();

            if (isDebugging)
            {
                AddCallback(gameEvent => Debug.LogWarning($"{gameEvent} was triggered."));
            }
        }

        public void Enable()
        {
            if (disabledCount > 0)
            {
                disabledCount--;

                if (disabledCount == 0)
                {
                    OnEnabledChangedEvent?.Invoke(false, true);
                }
            }
            else
            {
                Debug.LogError("Disabled count cannot be negative.");
            }
        }

        public void Disable()
        {
            disabledCount++;

            if (disabledCount == 1)
            {
                OnEnabledChangedEvent?.Invoke(true, false);
            }
        }
        
        public void AddCallback(Action<TGameEvent> callback, int priority = GameEventPriority.TINY)
        {
            if (callback == null)
            {
                Debug.LogError($"Cannot add null callback to {this}");
                return;
            }
            
            if (callbacks.TryGetValue(priority, out var set))
            {
                if (set.Add(callback) == false)
                {
                    callbacksLookup.Add(callback, priority);
                    return;
                }

                var methodName = callback.Method.Name;
                Debug.LogWarning($"Callback {methodName} already exists in {this} with priority {priority}.");
                
                return;
            }

            set = new() { callback };
            callbacks.Add(priority, set);
            callbacksLookup.Add(callback, priority);
        }
        
        public void RemoveCallback(Action<TGameEvent> callback)
        {
            if (callback == null)
            {
                Debug.LogError($"Cannot remove null callback from {this}");
                return;
            }
            
            if (callbacksLookup.TryGetValue(callback, out var priority) == false)
            {
                Debug.LogWarning($"Callback {callback.Method.Name} does not exist in {this}");
                return;
            }
            
            callbacks[priority].Remove(callback);
            callbacksLookup.Remove(callback);
        }
    }
}