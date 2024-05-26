using VMFramework.Core;
using Sirenix.OdinInspector;
using VMFramework.UI.Components;

namespace VMFramework.UI
{
    public class UGUIPopupProgressController : UGUIPopupController
    {
        protected UGUIPopupProgressPreset uguiPopupProgressPreset { get; private set; }

        [ShowInInspector]
        public ProgressUIComponent progress { get; private set; }

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            uguiPopupProgressPreset = preset as UGUIPopupProgressPreset;

            uguiPopupProgressPreset.AssertIsNotNull(nameof(uguiPopupProgressPreset));

            progress = visualRectTransform.QueryFirstComponentInChildren<ProgressUIComponent>(
                uguiPopupProgressPreset.progressName, true);

            progress.AssertIsNotNull(nameof(progress));
        }

        
    }
}
