using UnityEngine;
using VMFramework.GameEvents;

namespace VMFramework.Containers
{
    public class ContainerItemChangedEvent<TGameEvent> : GameEvent<TGameEvent>, 
        IReadOnlyContainerItemChangedEvent<TGameEvent>
        where TGameEvent : ContainerItemChangedEvent<TGameEvent>
    {
        public IContainer container { get; private set; }

        public int slotIndex { get; private set; }

        public IContainerItem item { get; private set; }

        public void SetParameters(IContainer container, int slotIndex, IContainerItem item)
        {
            if (isPropagating)
            {
                Debug.LogError("Cannot set parameters of a propagating event.");
                return;
            }
            
            this.container = container;
            this.slotIndex = slotIndex;
            this.item = item;
        }
    }
}