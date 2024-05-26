using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GameEvents
{
    public abstract class PooledGameEvent<TGameEvent> : IndependentGameEvent<TGameEvent>, IDisposable
        where TGameEvent : PooledGameEvent<TGameEvent>, new()
    {
        protected static readonly Stack<TGameEvent> pool = new();
        
        private bool isPropagationStopped = false;
        
        private bool isPropagating = false;
        
        public void StopPropagation()
        {
            isPropagationStopped = true;
        }

        protected virtual bool CanPropagate()
        {
            if (isEnabled == false)
            {
                Debug.LogWarning($"{typeof(TGameEvent)} is disabled. Cannot propagate.");
                return false;
            }
            
            return true;
        }

        [Button]
        public void Propagate()
        {
            if (isPropagating)
            {
                Debug.LogWarning($"Cannot propagate {typeof(TGameEvent)} while it is already propagating.");
                return;
            }
            
            if (CanPropagate() == false)
            {
                return;
            }
            
            isPropagating = true;
            
            isPropagationStopped = false;

            foreach (var (_, set) in GetCallbacks())
            {
                foreach (var callback in set)
                {
                    callback((TGameEvent)this);
                }
                
                if (isPropagationStopped)
                {
                    break;
                }
            }
            
            OnPropagationStopped();
            
            isPropagating = false;
        }

        protected virtual void OnPropagationStopped()
        {
            
        }

        [Button]
        public static TGameEvent Get()
        {
            if (pool.Count > 0)
            {
                return pool.Pop();
            }
            
            return new TGameEvent();
        }
        
        public static void Release(TGameEvent gameEvent)
        {
            pool.Push(gameEvent);
        }

        void IDisposable.Dispose()
        {
            Release((TGameEvent)this);
        }
    }
}