using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Procedure;
using VMFramework.Timers;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public class UIPanelPointerEventManager : ManagerBehaviour<UIPanelPointerEventManager>
    {
        [SerializeField]
        private bool isDebugging;

        #region PanelOnMouseHover

        private static IUIPanelController _panelOnMouseHover;

        [ShowInInspector]
        protected static IUIPanelController panelOnMouseHover
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _panelOnMouseHover;
            private set
            {
                var oldPanelOnMouseHover = _panelOnMouseHover;
                _panelOnMouseHover = value;
                OnPanelOnMouseHoverChanged?.Invoke(oldPanelOnMouseHover, _panelOnMouseHover);
            }
        }

        protected static event Action<IUIPanelController, IUIPanelController> OnPanelOnMouseHoverChanged;

        #endregion

        #region PanelOnMouseClick

        private static IUIPanelController _panelOnMouseClick;

        [ShowInInspector]
        protected static IUIPanelController panelOnMouseClick
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _panelOnMouseClick;
            private set
            {
                var oldPanelOnMouseClick = _panelOnMouseClick;
                _panelOnMouseClick = value;
                OnPanelOnMouseClickChanged?.Invoke(oldPanelOnMouseClick, _panelOnMouseClick);
            }
        }

        public static event Action<IUIPanelController, IUIPanelController> OnPanelOnMouseClickChanged;

        #endregion
        
        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();
            
            UIPanelManager.OnPanelCreatedEvent += OnPanelCreated;
            
            UpdateDelegateManager.AddUpdateDelegate(UpdateType.Update, StaticUpdate);
        }
        
        private static void StaticUpdate()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                panelOnMouseClick = panelOnMouseHover;
            }
        }

        private void OnPanelCreated(IUIPanelController panelController)
        {
            if (panelController is IUIPanelPointerEventProvider)
            {
                panelController.OnOpenEvent += OnPanelOpen;
                panelController.OnCloseEvent += OnPanelClose;
                panelController.OnDestructEvent += OnPanelDestruct;
            }
        }

        private void OnPanelOpen(IUIPanelController panelController)
        {
            if (panelController is IUIPanelPointerEventProvider pointerEventProvider)
            {
                pointerEventProvider.AddPointerEvent(OnPointerEnter, OnPointerLeave);
            }
        }

        private void OnPanelClose(IUIPanelController panelController)
        {
            if (panelController is IUIPanelPointerEventProvider pointerEventProvider)
            {
                pointerEventProvider.RemovePointerEvent();
                
                OnPointerLeave(panelController);
            }
        }

        private void OnPanelDestruct(IUIPanelController panelController)
        {
            if (panelController is IUIPanelPointerEventProvider pointerEventProvider)
            {
                pointerEventProvider.RemovePointerEvent();
                
                OnPointerLeave(panelController);
            }
        }

        private void OnPointerEnter(IUIPanelController panelController)
        {
            if (panelController == null)
            {
                return;
            }
            
            panelOnMouseHover = panelController;

            if (isDebugging)
            {
                Debug.LogWarning($"{name}鼠标进入");
            }

            if (panelController is IUIPanelPointerEventReceiver receiver)
            {
                receiver.OnPointerEnter();
            }
        }

        private void OnPointerLeave(IUIPanelController panelController)
        {
            if (panelController == null)
            {
                return;
            }
            
            if (panelOnMouseHover != panelController)
            {
                return;
                
            }
            
            panelOnMouseHover = null;

            if (isDebugging)
            {
                Debug.LogWarning($"{name}鼠标离开");
            }

            if (panelController is IUIPanelPointerEventReceiver receiver)
            {
                receiver.OnPointerLeave();
            }
        }
    }
}