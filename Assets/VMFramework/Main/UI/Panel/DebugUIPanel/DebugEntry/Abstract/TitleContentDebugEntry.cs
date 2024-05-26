using VMFramework.Configuration;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.UI
{
    public abstract partial class TitleContentDebugEntry : DebugEntry
    {
        [LabelText("显示标题"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [JsonProperty]
        public bool displayTitle = true;

        [LabelText("标题格式"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [ShowIf(nameof(displayTitle))]
        [SerializeField, JsonProperty]
        protected TextTagFormat titleFormat = new()
        {
            overrideFontColor = true,
            fontColor = Color.white
        };

        [LabelText("内容格式"), TabGroup(TAB_GROUP_NAME, BASIC_SETTING_CATEGORY)]
        [SerializeField, JsonProperty]
        protected TextTagFormat contentFormat = new()
        {
            overrideFontColor = true,
            fontColor = Color.white
        };
        
        protected virtual string GetTitle() => name;

        protected abstract string GetContent();
        
        public sealed override string GetText()
        {
            if (displayTitle)
            {
                return titleFormat.GetText(GetTitle() + ":") +
                       contentFormat.GetText(GetContent());
            }
            else
            {
                return contentFormat.GetText(GetContent());
            }
        }
    }
}
