using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GameEvents
{
    public partial class GameEvent<TGameEvent>
    {
        private bool isPropagationStopped = false;

        public bool isPropagating { get; private set; } = false;

        private List<(int priority, Action<TGameEvent> callback)> tempCallbacks = new();

        public void StopPropagation()
        {
            isPropagationStopped = true;
        }

        protected virtual bool CanPropagate()
        {
            if (isEnabled == false)
            {
                Debug.LogWarning($"GameEvent:{id} is disabled. Cannot propagate.");
                return false;
            }
            
            return true;
        }

        [Button]
        public void Propagate()
        {
            if (isPropagating)
            {
                Debug.LogWarning($"GameEvent:{id} is already propagating. Cannot propagate.");
                return;
            }
            
            if (CanPropagate() == false)
            {
                return;
            }
            
            isPropagating = true;
            isPropagationStopped = false;

            foreach (var (priority, set) in callbacks)
            {
                foreach (var callback in set)
                {
                    tempCallbacks.Add((priority, callback));
                }
            }

            int currentPriority = 0;
            foreach (var (priority, callback) in tempCallbacks)
            {
                if (priority != currentPriority)
                {
                    currentPriority = priority;
                    if (isPropagationStopped)
                    {
                        break;
                    }
                }
                
                callback((TGameEvent)this);
            }
            
            tempCallbacks.Clear();
            
            OnPropagationStopped();
            
            isPropagating = false;
        }

        protected virtual void OnPropagationStopped()
        {
            
        }
    }
}