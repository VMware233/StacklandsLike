using VMFramework.GameLogicArchitecture;

namespace VMFramework.Configuration
{
    public class SingleGamePrefabIDChooserValue<TGamePrefab>
        : SingleValueChooserConfig<GamePrefabIDConfig<TGamePrefab>>, IGamePrefabIDChooserConfig<TGamePrefab>
        where TGamePrefab : IGamePrefab
    {

    }
}