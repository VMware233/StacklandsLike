using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GameEvents
{
    public abstract class IndependentGameEvent<TGameEvent>
        where TGameEvent : IndependentGameEvent<TGameEvent>
    {
        [ShowInInspector]
        private static readonly SortedDictionary<int, HashSet<Action<TGameEvent>>> _callbacks = new();
        [ShowInInspector]
        private static readonly Dictionary<Delegate, int> callbacksLookup = new();

        [ShowInInspector]
        private static int disabledCount = 0;
        
        public static bool isEnabled => disabledCount <= 0;

        public static event IGameEvent.EnabledChangedEventHandler OnEnabledChangedEvent;

        #region Enabled

        public static void Enable()
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

        public static void Disable()
        {
            disabledCount++;

            if (disabledCount == 1)
            {
                OnEnabledChangedEvent?.Invoke(true, false);
            }
        }

        #endregion

        #region Callback

        public static void AddCallback(Action<TGameEvent> callback, int priority = GameEventPriority.TINY)
        {
            if (callback == null)
            {
                Debug.LogError($"Cannot add null callback to {typeof(TGameEvent)}");
                return;
            }
            
            if (_callbacks.TryGetValue(priority, out var set))
            {
                if (set.Add(callback) == false)
                {
                    callbacksLookup.Add(callback, priority);
                    return;
                }

                var methodName = callback.Method.Name;
                Debug.LogWarning($"Callback {methodName} already exists in {typeof(TGameEvent)} with priority {priority}.");
                
                return;
            }

            set = new() { callback };
            _callbacks.Add(priority, set);
            callbacksLookup.Add(callback, priority);
        }
        
        public static void RemoveCallback(Action<TGameEvent> callback)
        {
            if (callback == null)
            {
                Debug.LogError($"Cannot remove null callback from {typeof(TGameEvent)}");
                return;
            }
            
            if (callbacksLookup.TryGetValue(callback, out var priority) == false)
            {
                Debug.LogWarning($"Callback {callback.Method.Name} does not exist in {typeof(TGameEvent)}");
                return;
            }
            
            _callbacks[priority].Remove(callback);
            callbacksLookup.Remove(callback);
        }

        protected static IEnumerable<(int priority, IEnumerable<Action<TGameEvent>> callbacks)> GetCallbacks()
        {
            foreach (var (priority, set) in _callbacks)
            {
                yield return (priority, set);
            }
        }

        #endregion
    }
}