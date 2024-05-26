using UnityEngine.Localization;

namespace VMFramework.UI
{
    public interface ILocalizedUIPanelController : IUIPanelController
    {
        public void OnCurrentLanguageChanged(Locale currentLocale);
    }
}