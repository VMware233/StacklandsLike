namespace VMFramework.Configuration
{
    public interface IGameTypeBasedConfigs<TConfig> : IDictionaryConfigs<string, TConfig>
        where TConfig : IConfig
    {
        
    }
}