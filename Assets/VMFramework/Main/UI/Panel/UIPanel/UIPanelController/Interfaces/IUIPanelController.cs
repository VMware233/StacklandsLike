using System;
using System.Runtime.CompilerServices;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.UI
{
    public interface IUIPanelController : INameOwner
    {
        public string id { get; }
        
        public bool isUnique { get; }
        
        /// <summary>
        /// Whether the panel is currently opening
        /// </summary>
        public bool isOpening { get; protected set; }
        
        /// <summary>
        /// Whether the panel is currently closing
        /// </summary>
        public bool isClosing { get; protected set; }
        
        public bool isOpened { get; }
        
        public bool uiEnabled { get; }
        
        public IUIPanelController sourceUIPanel { get; protected set; }

        public event Action<IUIPanelController> OnOpenEvent;
        
        public event Action<IUIPanelController> OnCloseEvent;
        
        public event Action<IUIPanelController> OnOpenInstantlyEvent;

        public event Action<IUIPanelController> OnCloseInstantlyEvent;
        
        public event Action<IUIPanelController> OnDestructEvent;
        
        public event Action<IUIPanelController> OnCrashEvent;

        public void Init(IUIPanelPreset preset);

        #region Open

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Open(IUIPanelController source)
        {
            if (isOpened)
            {
                return;
            }
            
            isOpening = false;
            PreOpen(source);
            OpenInstantly(source);

            sourceUIPanel = source;

            if (sourceUIPanel != null)
            {
                sourceUIPanel.OnCloseInstantlyEvent += Close;
            }
        }

        public void PreOpen(IUIPanelController source);

        public void OpenInstantly(IUIPanelController source);

        public void StopOpening()
        {
            isOpening = false;
        }

        #endregion

        #region Close

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Close(IUIPanelController source)
        {
            if (isOpened == false)
            {
                return;
            }
            
            if (sourceUIPanel != null)
            {
                sourceUIPanel.OnCloseInstantlyEvent -= Close;
            }

            sourceUIPanel = null;
            
            isClosing = false;
            CloseInstantly(source);
            PostClose(source);
        }

        public void PostClose(IUIPanelController source);
        
        public void CloseInstantly(IUIPanelController source);

        public void StopClosing()
        {
            isClosing = false;
        }

        #endregion

        public void SetEnabled(bool enableState);

        public void OnRecreate(IUIPanelController newPanel);

        public void OnCreate();
        
        public void Destruct();

        public void Crash();
    }
}