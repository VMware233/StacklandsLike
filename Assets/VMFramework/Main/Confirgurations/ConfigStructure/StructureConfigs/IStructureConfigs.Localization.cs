#if UNITY_EDITOR
using VMFramework.Localization;

namespace VMFramework.Configuration
{
    public partial interface IStructureConfigs<TConfig> : ILocalizedStringOwnerConfig
    {
        
    }
}
#endif