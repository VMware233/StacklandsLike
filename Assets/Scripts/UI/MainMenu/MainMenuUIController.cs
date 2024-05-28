using StackLandsLike.GameCore;
using UnityEngine.UIElements;
using VMFramework.Core;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    public sealed class MainMenuUIController : UIToolkitPanelController
    {
        private MainMenuUIPreset mainMenuUIPreset => (MainMenuUIPreset)preset;

        private Button startGameButton;
        
        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);
            
            startGameButton = rootVisualElement.Q<Button>(mainMenuUIPreset.startGameButtonName);
            startGameButton.AssertIsNotNull(nameof(startGameButton));

            startGameButton.clicked += GameStateManager.StartGame;
        }
    }
}