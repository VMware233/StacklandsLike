#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Localization;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GeneralSetting : ILocalizedStringOwnerConfig
    {
        public virtual bool localizationEnabled => false;
        
        private void OnDefaultLocalizationTableNameChanged()
        {
            AutoConfigureLocalizedString(new()
            {
                defaultTableName = defaultLocalizationTableName,
                save = true,
            });
        }

        public virtual void AutoConfigureLocalizedString(LocalizedStringAutoConfigSettings settings)
        {
            
        }

        [Button, TabGroup(TAB_GROUP_NAME, LOCALIZABLE_SETTING_CATEGORY)]
        [EnableIf(nameof(localizationEnabled))]
        public virtual void SetKeyValueByDefault()
        {
            
        }
        
        #region JSON

        public bool ShouldSerializedefaultLocalizationTableName()
        {
            return localizationEnabled;
        }

        #endregion
    }
}
#endif