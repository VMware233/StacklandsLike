using VMFramework.GameLogicArchitecture;

namespace VMFramework.Configuration
{
    public class CircularSelectGamePrefabIDChooser<TGamePrefab>
        : CircularSelectChooserConfig<GamePrefabIDConfig<TGamePrefab>>,
            IGamePrefabIDChooserConfig<TGamePrefab>
        where TGamePrefab : IGamePrefab
    {

    }
}