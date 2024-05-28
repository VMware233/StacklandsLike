using Sirenix.OdinInspector;
using StackLandsLike.GameCore;
using TMPro;
using VMFramework.Core;
using VMFramework.Timers;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    public sealed class HUDController : UGUIPanelController
    {
        private HUDPreset hudPreset => (HUDPreset)preset;

        [ShowInInspector]
        private TextMeshProUGUI tickInDayLabel;

        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            tickInDayLabel =
                visualRectTransform.QueryFirstComponentInChildren<TextMeshProUGUI>(hudPreset.tickInDayLabel,
                    true);
            
            LogicTickManager.OnPostTick += OnTick;
        }

        protected override void OnCloseInstantly(IUIPanelController source)
        {
            base.OnCloseInstantly(source);
            
            LogicTickManager.OnPostTick -= OnTick;
        }

        private void OnTick()
        {
            tickInDayLabel.text = $"{GameTimeManager.tickInDay}/{GameTimeManager.ticksPerDay}";
        }
    }
}