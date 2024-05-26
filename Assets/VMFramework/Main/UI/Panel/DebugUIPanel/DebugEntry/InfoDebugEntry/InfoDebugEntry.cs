using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Localization;

namespace VMFramework.UI
{
    public sealed partial class InfoDebugEntry : TitleContentDebugEntry
    {
        [LabelText("内容")]
        [JsonProperty]
        public LocalizedStringReference content = new();

        protected override string GetContent() => content;
    }
}
