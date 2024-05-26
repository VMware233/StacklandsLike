using VMFramework.GameLogicArchitecture;

namespace VMFramework.Configuration
{
    public interface IGamePrefabIDChooserConfig<TGamePrefab> : IChooserConfig<GamePrefabIDConfig<TGamePrefab>>
        where TGamePrefab : IGamePrefab
    {
        public string GetID()
        {
            return GetValue().id;
        }
    }
}