using Sirenix.OdinInspector;
using StackLandsLike.GameCore;
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    public sealed class SettlementScreenUIController : UIToolkitPanelController
    {
        private SettlementScreenUIPreset settlementScreenUIPreset => (SettlementScreenUIPreset)preset;
        
        [ShowInInspector]
        private Label titleLabel;
        
        [ShowInInspector]
        private Label settlementLabel;
        
        [ShowInInspector]
        private VisualElement victoryBackground;
        
        [ShowInInspector]
        private VisualElement defeatBackground;
        
        [ShowInInspector]
        private Button restartGameButton;
        
        [ShowInInspector]
        private Button exitGameButton;

        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            titleLabel = rootVisualElement.Q<Label>(settlementScreenUIPreset.titleLabelName);
            settlementLabel = rootVisualElement.Q<Label>(settlementScreenUIPreset.settlementLabelName);

            victoryBackground = rootVisualElement.Q(settlementScreenUIPreset.victoryBackgroundName);
            defeatBackground = rootVisualElement.Q(settlementScreenUIPreset.defeatBackgroundName);
            
            restartGameButton = rootVisualElement.Q<Button>(settlementScreenUIPreset.restartGameButtonName);
            exitGameButton = rootVisualElement.Q<Button>(settlementScreenUIPreset.exitGameButtonName);
            
            restartGameButton.clicked += OnRestartGameButtonClick;
            exitGameButton.clicked += OnExitGameButtonClick;

            if (GameStateManager.isVictory)
            {
                titleLabel.text = GameSetting.settlementScreenGeneralSetting.victoryTitle;
                victoryBackground.style.display = DisplayStyle.Flex;
                defeatBackground.style.display = DisplayStyle.None;
            }
            else
            {
                titleLabel.text = GameSetting.settlementScreenGeneralSetting.defeatTitle;
                defeatBackground.style.display = DisplayStyle.Flex;
                victoryBackground.style.display = DisplayStyle.None;
            }
        }

        protected override void OnCloseInstantly(IUIPanelController source)
        {
            base.OnCloseInstantly(source);

            if (restartGameButton != null)
            {
                restartGameButton.clicked -= OnRestartGameButtonClick;
            }

            if (exitGameButton != null)
            {
                exitGameButton.clicked -= OnExitGameButtonClick;
            }
        }

        private void OnRestartGameButtonClick()
        {
            GameStateManager.EndGame();
        }
        
        private void OnExitGameButtonClick()
        {
            Application.Quit();
        }
    }
}