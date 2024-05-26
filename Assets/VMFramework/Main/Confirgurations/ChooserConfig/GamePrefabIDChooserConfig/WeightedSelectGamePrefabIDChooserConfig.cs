using VMFramework.GameLogicArchitecture;

namespace VMFramework.Configuration
{
    public class WeightedSelectGamePrefabIDChooserConfig<TGamePrefab>
        : WeightedSelectChooserConfig<GamePrefabIDConfig<TGamePrefab>>,
            IGamePrefabIDChooserConfig<TGamePrefab>
        where TGamePrefab : IGamePrefab
    {

    }
}