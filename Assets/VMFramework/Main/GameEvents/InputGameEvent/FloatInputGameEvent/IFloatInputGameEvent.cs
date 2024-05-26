namespace VMFramework.GameEvents
{
    public interface IFloatInputGameEvent : IInputGameEvent
    {
        public float value { get; }
    }
}