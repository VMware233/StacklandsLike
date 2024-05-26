using VMFramework.GameLogicArchitecture;

namespace VMFramework.GameEvents
{
    public abstract class GameEventConfig : LocalizedGameTypedGamePrefab, IGameEventConfig
    {
        protected override string idSuffix => "event";
    }
}