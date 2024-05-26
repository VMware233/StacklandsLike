using UnityEngine.Localization.Settings;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public class LocalizedUIPanelManager : ManagerBehaviour<LocalizedUIPanelManager>
    {
        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();
            
            UIPanelManager.OnPanelCreatedEvent += OnUIPanelCreated;
        }

        private void OnUIPanelCreated(IUIPanelController uiPanelController)
        {
            if (uiPanelController is ILocalizedUIPanelController)
            {
                uiPanelController.OnOpenEvent += OnUIPanelOpen;
                uiPanelController.OnCloseEvent += OnUIPanelClose;
                uiPanelController.OnDestructEvent += OnUIPanelDestruct;
            }
        }

        private void OnUIPanelOpen(IUIPanelController uiPanelController)
        {
            if (uiPanelController is ILocalizedUIPanelController localizedUIPanelController)
            {
                localizedUIPanelController.OnCurrentLanguageChanged(LocalizationSettings.SelectedLocale);

                LocalizationSettings.SelectedLocaleChanged +=
                    localizedUIPanelController.OnCurrentLanguageChanged;
            }
        }

        private void OnUIPanelClose(IUIPanelController uiPanelController)
        {
            if (uiPanelController is ILocalizedUIPanelController localizedUIPanelController)
            {
                LocalizationSettings.SelectedLocaleChanged -=
                    localizedUIPanelController.OnCurrentLanguageChanged;
            }
        }

        private void OnUIPanelDestruct(IUIPanelController uiPanelController)
        {
            if (uiPanelController is ILocalizedUIPanelController localizedUIPanelController)
            {
                LocalizationSettings.SelectedLocaleChanged -=
                    localizedUIPanelController.OnCurrentLanguageChanged;
            }
        }
    }
}