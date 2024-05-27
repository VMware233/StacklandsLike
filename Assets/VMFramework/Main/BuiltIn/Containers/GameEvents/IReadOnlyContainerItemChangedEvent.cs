using VMFramework.GameEvents;

namespace VMFramework.Containers
{
    public interface IReadOnlyContainerItemChangedEvent<out TGameEvent> : IReadOnlyGameEvent<TGameEvent>
        where TGameEvent : IReadOnlyContainerItemChangedEvent<TGameEvent>
    {
        
    }
}