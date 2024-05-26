#if UNITY_EDITOR
using VMFramework.Core;
using VMFramework.Localization;

namespace VMFramework.UI
{
    public partial class InfoDebugEntry
    {
        public override void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            base.AutoConfigureLocalizedString(settings);

            content ??= new();
            
            content.AutoConfig("", id.ToPascalCase() + "Content", settings.defaultTableName);
        }
    }
}
#endif