#if UNITY_EDITOR
using VMFramework.Core;
using VMFramework.Localization;

namespace StackLandsLike.UI
{
    public partial class RecipeCategoryConfig : ILocalizedStringOwnerConfig
    {
        void ILocalizedStringOwnerConfig.AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            categoryName.AutoConfigNameByID(gameTypeID, settings.defaultTableName);
        }

        void ILocalizedStringOwnerConfig.SetKeyValueByDefault()
        {
            categoryName.SetKeyValueByDefault();
        }
    }
}
#endif