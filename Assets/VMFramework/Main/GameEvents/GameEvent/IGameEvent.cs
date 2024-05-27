using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.GameEvents
{
    public interface IReadOnlyGameEvent : IGameItem
    {
        public bool isEnabled { get; }
        
        public delegate void EnabledChangedEventHandler(bool previous, bool current);

        public event EnabledChangedEventHandler OnEnabledChangedEvent;
        
        public void Enable();
        
        public void Disable();
        
        public void AddCallback(Delegate callback, int priority);
        
        public void RemoveCallback(Delegate callback);
    }
    
    public interface IGameEvent : IReadOnlyGameEvent
    {
        public void Propagate();
        
        public void StopPropagation();
    }

    public interface IReadOnlyGameEvent<out TGameEvent> : IReadOnlyGameEvent
        where TGameEvent : IReadOnlyGameEvent<TGameEvent>
    {
        public void AddCallback(Action<TGameEvent> callback, int priority);
        
        public void RemoveCallback(Action<TGameEvent> callback);

        void IReadOnlyGameEvent.AddCallback(Delegate callback, int priority)
        {
            AddCallback((Action<TGameEvent>)callback, priority);
        }

        void IReadOnlyGameEvent.RemoveCallback(Delegate callback)
        {
            RemoveCallback((Action<TGameEvent>)callback);
        }
    }

    public interface IGameEvent<out TGameEvent> : IGameEvent, IReadOnlyGameEvent<TGameEvent>
        where TGameEvent : IGameEvent<TGameEvent>
    {
        
    }
}