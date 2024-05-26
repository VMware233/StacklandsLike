using System.Threading;
using VMFramework.Core;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.UI
{
    public class UGUIPopupController : UGUITracingUIPanelController, ICloseAsyncUIPanelController,
        IOpenAsyncUIPanelController
    {
        protected UGUIPopupPreset uguiPopupPreset { get; private set; }

        [ShowInInspector]
        protected Transform popupContainer;
        
        CancellationTokenSource IOpenAsyncUIPanelController.openingCTS { get; set; }
        
        CancellationTokenSource ICloseAsyncUIPanelController.closingCTS { get; set; }

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            uguiPopupPreset = preset as UGUIPopupPreset;

            uguiPopupPreset.AssertIsNotNull(nameof(uguiPopupPreset));

            popupContainer = visualObject.transform.QueryFirstComponentInChildren<RectTransform>(
                uguiPopupPreset.popupContainerName, true);

            popupContainer.AssertIsNotNull(nameof(popupContainer));
        }

        protected override void OnOpen(IUIPanelController source)
        {
            base.OnOpen(source);
            
            popupContainer.ResetLocalArguments();
        }
        
        public async UniTask<bool> AwaitToOpen(IUIPanelController source, CancellationToken token = default)
        {
            if (uguiPopupPreset.enableContainerAnimation)
            {
                if (uguiPopupPreset.splitContainerAnimation)
                {
                    await uguiPopupPreset.startContainerAnimation.RunAndAwait(popupContainer, token);
                }
            }
            
            return true;
        }

        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            if (uguiPopupPreset.enableContainerAnimation)
            {
                if (uguiPopupPreset.splitContainerAnimation == false)
                {
                    if (uguiPopupPreset.autoCloseAfterContainerAnimation)
                    {
                        this.Close();
                    }
                }
            }
        }

        public async UniTask<bool> AwaitToClose(CancellationToken token = default)
        {
            if (uguiPopupPreset.enableContainerAnimation)
            {
                if (uguiPopupPreset.splitContainerAnimation)
                {
                    uguiPopupPreset.startContainerAnimation.Kill(popupContainer);

                    await uguiPopupPreset.endContainerAnimation.RunAndAwait(popupContainer, token);
                }
                else
                {
                    uguiPopupPreset.containerAnimation.Kill(popupContainer);
                    
                    await uguiPopupPreset.containerAnimation.RunAndAwait(popupContainer, token);
                }
            }
            
            return true;
        }

        protected override void OnCloseInstantly(IUIPanelController source)
        {
            base.OnCloseInstantly(source);

            if (uguiPopupPreset.enableContainerAnimation &&
                uguiPopupPreset.splitContainerAnimation)
            {
                uguiPopupPreset.endContainerAnimation.Kill(popupContainer);
            }
        }
    }
}
