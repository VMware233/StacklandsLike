using Sirenix.OdinInspector;
using StackLandsLike.GameCore;
using TMPro;
using UnityEngine.UIElements;
using VMFramework.Core;
using VMFramework.Timers;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    public sealed class HUDController : UIToolkitPanelController
    {
        private HUDPreset hudPreset => (HUDPreset)preset;
        
        private Label personCountLabel;
        private Label foodInfoLabel;

        private VisualElement clockwise;

        private Label dayCountLabel;

        private Button pauseButton;
        private VisualElement continueIcon;
        private VisualElement pauseIcon;

        private Button speedButton;
        private VisualElement speed2xIcon;
        private VisualElement speed1xIcon;

        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            personCountLabel = rootVisualElement.Q<Label>(hudPreset.personCountLabelName);
            foodInfoLabel = rootVisualElement.Q<Label>(hudPreset.foodInfoLabelName);

            clockwise = rootVisualElement.Q(hudPreset.clockwiseName);
            
            dayCountLabel = rootVisualElement.Q<Label>(hudPreset.dayCountLabelName);
            
            pauseButton = rootVisualElement.Q<Button>(hudPreset.pauseButtonName);
            continueIcon = rootVisualElement.Q(hudPreset.continueIconName);
            pauseIcon = rootVisualElement.Q(hudPreset.pauseIconName);

            speedButton = rootVisualElement.Q<Button>(hudPreset.speedButtonName);
            speed2xIcon = rootVisualElement.Q(hudPreset.speed2xIconName);
            speed1xIcon = rootVisualElement.Q(hudPreset.speed1xIconName);
            
            LogicTickManager.OnPostTick += OnTick;

            pauseButton.clicked += GameStateManager.TogglePause;
            
            OnPauseStateChange(GameStateManager.isPaused);
            GameStateManager.OnPauseStateChange += OnPauseStateChange;
            
            speedButton.clicked += GameStateManager.ToggleSpeed;
            
            OnSpeedStateChange(GameStateManager.isSpeed2x);
            GameStateManager.OnSpeedStateChange += OnSpeedStateChange;
            
            OnScoreboardUpdated();
            Scoreboard.OnScoreboardUpdated += OnScoreboardUpdated;
        }

        protected override void OnCloseInstantly(IUIPanelController source)
        {
            base.OnCloseInstantly(source);
            
            LogicTickManager.OnPostTick -= OnTick;
            
            GameStateManager.OnPauseStateChange -= OnPauseStateChange;
            GameStateManager.OnSpeedStateChange -= OnSpeedStateChange;
            
            Scoreboard.OnScoreboardUpdated -= OnScoreboardUpdated;
        }

        private void OnScoreboardUpdated()
        {
            personCountLabel.text = Scoreboard.personCount.ToString();
            foodInfoLabel.text = $"{Scoreboard.nutritionRequiredCount}/{Scoreboard.nutritionCount}";
        }

        private void OnSpeedStateChange(bool is2x)
        {
            if (is2x)
            {
                speed2xIcon.style.display = DisplayStyle.Flex;
                speed1xIcon.style.display = DisplayStyle.None;
            }
            else
            {
                speed2xIcon.style.display = DisplayStyle.None;
                speed1xIcon.style.display = DisplayStyle.Flex;
            }
        }

        private void OnPauseStateChange(bool isPaused)
        {
            if (isPaused)
            {
                continueIcon.style.display = DisplayStyle.None;
                pauseIcon.style.display = DisplayStyle.Flex;
            }
            else
            {
                continueIcon.style.display = DisplayStyle.Flex;
                pauseIcon.style.display = DisplayStyle.None;
            }
        }

        private void OnTick()
        {
            var ratio = GameTimeManager.tickInDay / (float)GameTimeManager.ticksPerDay;

            var angle = ratio.NormalizeTo(0, 1, hudPreset.clockwiseAngleRange.min,
                hudPreset.clockwiseAngleRange.max);

            clockwise.style.rotate = new Rotate(angle);
            
            dayCountLabel.text = $"{GameTimeManager.day + 1:D2}";
        }
    }
}