#if UNITY_EDITOR
using VMFramework.Core;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public partial class DescribedGamePrefab
    {
        public override void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            base.AutoConfigureLocalizedString(settings);

            description ??= new();
            description.AutoConfig("", id.ToPascalCase() + "Description", settings.defaultTableName);
        }
    }
}
#endif