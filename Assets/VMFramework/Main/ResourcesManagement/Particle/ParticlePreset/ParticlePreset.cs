using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.ResourcesManagement
{
    public partial class ParticlePreset : GameTypedGamePrefab
    {
        protected override string idSuffix => "particle";

        [TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [AssetList]
        [AssetSelector(Paths = "Assets")]
        [AssetsOnly]
        [Required]
        public ParticleSystem particlePrefab;

        [TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [ToggleButtons("Enable", "Disable")]
        public bool enableDurationLimitation = false;

        [TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [ShowIf(nameof(enableDurationLimitation))]
        public IChooserConfig<float> duration = new SingleValueChooserConfig<float>();
    }
}
