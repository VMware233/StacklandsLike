using UnityEngine.Localization;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.UI
{
    public partial class UIToolkitPanelController : ILocalizedUIPanelController
    {
        protected Locale lastLocale { get; private set; }

        void ILocalizedUIPanelController.OnCurrentLanguageChanged(Locale currentLocale)
        {
            OnCurrentLanguageChanged(currentLocale);
        }

        protected virtual void OnCurrentLanguageChanged(Locale currentLocale)
        {
            if (GameCoreSetting.uiPanelGeneralSetting.enableLanguageConfigs)
            {
                if (lastLocale != null)
                {
                    var previousLanguageConfig =
                        GameCoreSetting.uiPanelGeneralSetting.languageConfigs.GetConfig(currentLocale
                            .Identifier.Code);
        
                    if (previousLanguageConfig != null)
                    {
                        rootVisualElement.styleSheets.Remove(previousLanguageConfig.styleSheet);
                    }
                }
                
                lastLocale = currentLocale;
        
                var currentLanguageConfig =
                    GameCoreSetting.uiPanelGeneralSetting.languageConfigs.GetConfig(currentLocale
                        .Identifier.Code);
        
                if (currentLanguageConfig != null)
                {
                    rootVisualElement.styleSheets.Add(currentLanguageConfig.styleSheet);
                }
            }
        }
    }
}