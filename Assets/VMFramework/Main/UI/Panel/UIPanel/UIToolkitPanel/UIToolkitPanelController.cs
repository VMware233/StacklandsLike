using System;
using UnityEngine.UIElements;
using VMFramework.Core;

namespace VMFramework.UI
{
    public partial class UIToolkitPanelController : UIPanelController, IUIToolkitUIPanelController
    {
        protected UIDocument uiDocument => GetComponent<UIDocument>();

        protected UIToolkitPanelPreset uiToolkitPanelPreset { get; private set; }

        protected VisualElement rootVisualElement;
        
        public override event Action<IUIPanelController> OnOpenEvent;
        public override event Action<IUIPanelController> OnCloseEvent;

        #region Init

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            uiToolkitPanelPreset = preset as UIToolkitPanelPreset;

            uiToolkitPanelPreset.AssertIsNotNull(nameof(uiToolkitPanelPreset));

            var uiDocument = this.GetOrAddComponent<UIDocument>();

            uiDocument.panelSettings = uiToolkitPanelPreset.panelSettings;
            uiDocument.visualTreeAsset = uiToolkitPanelPreset.visualTree;

            uiDocument.enabled = true;
        }

        #endregion

        #region Open
        
        void IUIPanelController.PreOpen(IUIPanelController source)
        {
            uiDocument.enabled = true;

            rootVisualElement = uiDocument.rootVisualElement;
            rootVisualElement.style.visibility = Visibility.Hidden;
            
            OnOpen(source);
            
            OnOpenEvent?.Invoke(this);
            
            1.DelayFrameAction(() =>
            {
                if (isOpened || isOpening)
                {
                    OnLayoutChange();

                    OnPostLayoutChange();
                }
            });
        }

        protected virtual void OnOpen(IUIPanelController source)
        {
            
        }

        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            if (uiToolkitPanelPreset.closeUIButtonName.IsNullOrEmpty() == false)
            {
                var closeButton = rootVisualElement.Q<Button>(uiToolkitPanelPreset.closeUIButtonName);

                closeButton.clicked += this.Close;
            }
        }

        #endregion

        #region Close

        void IUIPanelController.PostClose(IUIPanelController source)
        {
            OnClose(source);
            
            OnCloseEvent?.Invoke(this);
        }

        protected virtual void OnClose(IUIPanelController source)
        {
            uiDocument.enabled = false;
        }

        #endregion

        #region Layout Change
        
        protected virtual void OnLayoutChange()
        {

        }

        protected virtual void OnPostLayoutChange()
        {
            rootVisualElement.style.visibility = Visibility.Visible;

            if (uiToolkitPanelPreset.ignoreMouseEvents)
            {
                foreach (var visualElement in rootVisualElement.GetAll<VisualElement>())
                {
                    visualElement.pickingMode = PickingMode.Ignore;
                }
            }
        }

        #endregion

        #region Add Visual Element

        protected void AddVisualElement(VisualElement parent, VisualElement newChild)
        {
            parent.Add(newChild);

            OnNewVisualElementPostProcessing(newChild);
        }

        protected virtual void OnNewVisualElementPostProcessing(VisualElement newVisualElement)
        {
            if (uiToolkitPanelPreset.ignoreMouseEvents)
            {
                foreach (var visualElement in newVisualElement.GetAll<VisualElement>())
                {
                    visualElement.pickingMode = PickingMode.Ignore;
                }
            }
        }

        #endregion
    }
}