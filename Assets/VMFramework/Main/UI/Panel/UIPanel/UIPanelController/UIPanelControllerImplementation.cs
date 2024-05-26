using System;
using Sirenix.OdinInspector;

namespace VMFramework.UI
{
    public partial class UIPanelController : IUIPanelController
    {
        public string id => preset.id;

        public bool isUnique => preset.isUnique;
        
        [ShowInInspector]
        public bool uiEnabled { get; private set; }
        
        [ShowInInspector]
        public bool isOpening { get; private set; }

        [ShowInInspector]
        public bool isClosing { get; private set; }

        [ShowInInspector]
        public bool isOpened { get; private set; }

        [ShowInInspector]
        public IUIPanelController sourceUIPanel { get; private set; }

        IUIPanelController IUIPanelController.sourceUIPanel
        {
            get => sourceUIPanel;
            set => sourceUIPanel = value;
        }
        
        public abstract event Action<IUIPanelController> OnOpenEvent;
        
        public abstract event Action<IUIPanelController> OnCloseEvent;

        public event Action<IUIPanelController> OnOpenInstantlyEvent;

        public event Action<IUIPanelController> OnCloseInstantlyEvent;
        
        public event Action<IUIPanelController> OnDestructEvent;

        public event Action<IUIPanelController> OnCrashEvent;

        [ShowInInspector]
        bool IUIPanelController.isOpening
        {
            get => isOpening;
            set => isOpening = value;
        }
        
        [ShowInInspector]
        bool IUIPanelController.isClosing
        {
            get => isClosing;
            set => isClosing = value;
        }

        void IUIPanelController.OnRecreate(IUIPanelController newPanel)
        {
            OnRecreate(newPanel);
        }

        void IUIPanelController.OnCreate()
        {
            OnCreate();
        }

        void IUIPanelController.Crash()
        {
            OnCrashEvent?.Invoke(this);

            OnCrash();
            
            if (isUnique)
            {
                UIPanelManager.RecreateUniquePanel(preset.id);
            }
            else
            {
                this.Destruct();
            }
        }

        void IUIPanelController.Destruct()
        {
            OnDestruction();
            OnDestructEvent?.Invoke(this);
            Destroy(gameObject);
        }
        
        void IUIPanelController.PreOpen(IUIPanelController source)
        {
            
        }

        void IUIPanelController.PostClose(IUIPanelController source)
        {
            
        }
    }
}