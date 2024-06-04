using StackLandsLike.GameCore;
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.Core;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    public sealed class MainMenuUIController : UIToolkitPanelController
    {
        private MainMenuUIPreset mainMenuUIPreset => (MainMenuUIPreset)preset;

        private Button startGameButton;
        
        private Button quitGameButton;
        
        private Button producersButton;
        
        private VisualElement producersContainer;
        
        private bool isProducersContainerVisible = false;
        
        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);
            
            startGameButton = rootVisualElement.Q<Button>(mainMenuUIPreset.startGameButtonName);
            quitGameButton = rootVisualElement.Q<Button>(mainMenuUIPreset.quitGameButtonName);
            producersButton = rootVisualElement.Q<Button>(mainMenuUIPreset.producersButtonName);
            producersContainer = rootVisualElement.Q(mainMenuUIPreset.producersContainerName);

            startGameButton.clicked += GameStateManager.StartGame;
            quitGameButton.clicked += Application.Quit;
            producersButton.clicked += ToggleProducersContainer;
            
            isProducersContainerVisible = false;
            producersContainer.style.display = DisplayStyle.None;
        }
        
        private void ToggleProducersContainer()
        {
            if (isProducersContainerVisible)
            {
                producersContainer.style.display = DisplayStyle.None;
                isProducersContainerVisible = false;
            }
            else
            {
                producersContainer.style.display = DisplayStyle.Flex;
                isProducersContainerVisible = true;
            }
        }
    }
}