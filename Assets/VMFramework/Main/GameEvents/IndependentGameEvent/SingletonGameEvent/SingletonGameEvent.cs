using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GameEvents
{
    public abstract class SingletonGameEvent<TGameEvent> : IndependentGameEvent<TGameEvent>
        where TGameEvent : SingletonGameEvent<TGameEvent>, new()
    {
        protected static readonly TGameEvent instance = new();
        
        private static bool isPropagationStopped = false;
        
        private static bool isPropagating = false;
        
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
        public static void Propagate()
        {
            if (isPropagating)
            {
                Debug.LogWarning($"Cannot propagate {typeof(TGameEvent)} while it is already propagating.");
                return;
            }
            
            if (instance.CanPropagate() == false)
            {
                return;
            }
            
            isPropagating = true;
            
            isPropagationStopped = false;

            foreach (var (_, set) in GetCallbacks())
            {
                foreach (var callback in set)
                {
                    callback(instance);
                }
                
                if (isPropagationStopped)
                {
                    break;
                }
            }
            
            instance.OnPropagationStopped();
            
            isPropagating = false;
        }

        protected virtual void OnPropagationStopped()
        {
            
        }
    }
}