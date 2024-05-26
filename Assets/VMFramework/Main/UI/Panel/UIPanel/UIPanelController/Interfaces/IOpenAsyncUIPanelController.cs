using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VMFramework.UI
{
    public interface IOpenAsyncUIPanelController : IUIPanelController
    {
        protected CancellationTokenSource openingCTS { get; set; }
        
        async void IUIPanelController.Open(IUIPanelController source)
        {
            if (isClosing)
            {
                StopClosing();
            }
            else if (isOpened)
            {
                Debug.LogWarning("UIPanelController is already opened.");
                return;
            }

            if (isOpening)
            {
                Debug.LogWarning("UIPanelController is already opening.");
                return;
            }

            isOpening = true;
            
            sourceUIPanel = source;
            if (sourceUIPanel != null)
            {
                sourceUIPanel.OnCloseInstantlyEvent += Close;
            }
            
            PreOpen(source);
            
            openingCTS = new CancellationTokenSource();
            
            var openResult = await AwaitToOpen(source, openingCTS.Token);

            isOpening = false;
            
            if (openResult)
            {
                OpenInstantly(source);
            }
        }

        public UniTask<bool> AwaitToOpen(IUIPanelController source, CancellationToken token = default);

        void IUIPanelController.StopOpening()
        {
            if (isOpening)
            {
                isOpening = false;
                
                if (openingCTS == null)
                {
                    Debug.LogWarning(
                        "UIPanelController is already opening but no cancellation token source is available.");
                    return;
                }
                
                openingCTS.Cancel();
            }
            else
            {
                Debug.LogWarning("UIPanelController is not opening.");
            }
        }
    }
}