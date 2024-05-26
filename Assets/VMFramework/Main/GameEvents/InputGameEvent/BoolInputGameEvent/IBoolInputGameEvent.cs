namespace VMFramework.GameEvents
{
    public interface IBoolInputGameEvent : IInputGameEvent
    {
        public bool value { get; }
    }
}