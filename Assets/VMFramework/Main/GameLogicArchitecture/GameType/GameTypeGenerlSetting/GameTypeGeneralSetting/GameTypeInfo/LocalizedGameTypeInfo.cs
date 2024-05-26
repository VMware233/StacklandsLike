using Sirenix.OdinInspector;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameTypeGeneralSetting
    {
        private partial class LocalizedGameTypeInfo : GameTypeInfo, INameOwner, ILocalizedNameOwner
        {
            [PropertyOrder(-5000)]
            public LocalizedStringReference name = new();

            #region Interface Implementation

            string INameOwner.name => name;

            IReadOnlyLocalizedStringReference ILocalizedNameOwner.nameReference => name;

            #endregion
        }
    }
}