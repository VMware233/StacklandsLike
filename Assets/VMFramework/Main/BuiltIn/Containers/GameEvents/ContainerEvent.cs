using VMFramework.GameEvents;

namespace VMFramework.Containers
{
    public abstract class ContainerEvent<TContainerEvent> : PooledGameEvent<TContainerEvent>
        where TContainerEvent : ContainerEvent<TContainerEvent>, new()
    {
        public IContainer container { get; private set; }
        
        public void SetContainer(IContainer newContainer)
        {
            container = newContainer;
        }

        protected override void OnPropagationStopped()
        {
            base.OnPropagationStopped();
            
            container = null;
        }
    }
}