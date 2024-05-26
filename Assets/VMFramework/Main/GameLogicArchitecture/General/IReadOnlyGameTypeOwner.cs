namespace VMFramework.GameLogicArchitecture
{
    public interface IReadOnlyGameTypeOwner
    {
        public IReadOnlyGameTypeSet gameTypeSet { get; }
    }

    public interface IGameTypeOwner
    {
        public IGameTypeSet gameTypeSet { get; }
    }
}
