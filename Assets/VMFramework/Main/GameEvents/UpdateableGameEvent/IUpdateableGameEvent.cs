namespace VMFramework.GameEvents
{
    public interface IUpdateableGameEvent : IGameEvent
    {
        public void Update();
    }
}