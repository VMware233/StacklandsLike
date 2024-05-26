using Sirenix.OdinInspector;

namespace VMFramework.GameEvents
{
    public sealed class ColliderMouseEvent : GameEvent<ColliderMouseEvent>
    {
        [ShowInInspector]
        public ColliderMouseEventTrigger trigger;
        
        public void SetTrigger(ColliderMouseEventTrigger trigger)
        {
            this.trigger = trigger;
        }

        protected override void OnPropagationStopped()
        {
            base.OnPropagationStopped();

            trigger = null;
        }
    }
}