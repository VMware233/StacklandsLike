using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.GameEvents
{
    public interface IGameEvent : IGameItem
    {
        public bool isEnabled { get; }
        
        public delegate void EnabledChangedEventHandler(bool previous, bool current);

        public event EnabledChangedEventHandler OnEnabledChangedEvent;
        
        public void Enable();
        
        public void Disable();
        
        public void AddCallback(Delegate callback, int priority = GameEventPriority.TINY);
        
        public void RemoveCallback(Delegate callback);

        public void Propagate();
        
        public void StopPropagation();
    }

    public interface IGameEvent<out TGameEvent> : IGameEvent
        where TGameEvent : IGameEvent<TGameEvent>
    {
        public void AddCallback(Action<TGameEvent> callback, int priority = GameEventPriority.TINY);
        
        public void RemoveCallback(Action<TGameEvent> callback);

        void IGameEvent.AddCallback(Delegate callback, int priority)
        {
            AddCallback((Action<TGameEvent>)callback, priority);
        }

        void IGameEvent.RemoveCallback(Delegate callback)
        {
            RemoveCallback((Action<TGameEvent>)callback);
        }
    }
}