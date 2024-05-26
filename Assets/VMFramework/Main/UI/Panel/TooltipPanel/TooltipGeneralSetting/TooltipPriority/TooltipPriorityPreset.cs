using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public class TooltipPriorityPreset : BaseConfig, IIDOwner, INameOwner
    {
        [PropertyTooltip("ID")]
        [HorizontalGroup]
        [IsNotNullOrEmpty]
        public string presetID;

        [PropertyTooltip("优先级")]
        [HorizontalGroup]
        public int priority;

        string IIDOwner<string>.id => presetID;

        string INameOwner.name => ToString();

        public override string ToString()
        {
            return $"{presetID}-{priority}";
        }
    }
}