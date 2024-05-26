using Newtonsoft.Json;
using Sirenix.OdinInspector;

namespace VMFramework.GameEvents
{
    public abstract partial class InputGameEventConfig : GameEventConfig
    {
        protected const string INPUT_MAPPING_CATEGORY = "Input Mapping";
        
        [TabGroup(TAB_GROUP_NAME, INPUT_MAPPING_CATEGORY)]
        [JsonProperty]
        public bool requireMouseInScreen;
    }
}