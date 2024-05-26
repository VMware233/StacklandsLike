using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VMFramework.UI
{
    public interface ICloseAsyncUIPanelController : IUIPanelController
    {
        protected CancellationTokenSource closingCTS { get; set; }
        
        async void IUIPanelController.Close(IUIPanelController source)
        {
            if (isOpening)
            {
                StopOpening();
            }
            else if (isOpened == false)
            {
                Debug.LogWarning("UIPanelController is already closed.");
                return;
            }
            
            if (isClosing)
            {
                Debug.LogWarning("UIPanelController is already closing.");
                return;
            }
            
            isClosing = true;
            
            if (sourceUIPanel != null)
            {
                sourceUIPanel.OnCloseInstantlyEvent -= Close;
            }
            sourceUIPanel = null;
            
            closingCTS = new CancellationTokenSource();
            
            var closeResult = await AwaitToClose(closingCTS.Token);

            isClosing = false;
            
            if (closeResult)
            {
                CloseInstantly(source);
            }
            
            PostClose(source);
        }

        public UniTask<bool> AwaitToClose(CancellationToken token = default);

        void IUIPanelController.StopClosing()
        {
            if (isClosing)
            {
                isClosing = false;
                
                if (closingCTS == null)
                {
                    Debug.LogWarning(
                        "UIPanelController is already closing, but no cancellation token source is available.");
                    return;
                }
                
                closingCTS.Cancel();
            }
            else
            {
                Debug.LogWarning("UIPanelController is not opening.");
            }
        }
    }
}