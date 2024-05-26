using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameEvents;

namespace VMFramework.UI
{
    public abstract partial class UIPanelController : MonoBehaviour
    {
        [ShowInInspector]
        protected bool isDebugging { get; private set; } = false;

        [ShowInInspector]
        public UIPanelPreset preset { get; private set; }

        #region Init & Destroy

        public void Init(IUIPanelPreset presetInterface)
        {
            var preset = presetInterface as UIPanelPreset;
            preset.AssertIsNotNull(nameof(preset));
            
            OnPreInit(preset);
            OnInit(preset);
            OnPostInit(preset);
        }

        protected virtual void OnPreInit(UIPanelPreset preset)
        {
            this.preset = preset;

            if (preset.isDebugging)
            {
                isDebugging = true;
            }

            if (preset.enableUICloseGameEvent)
            {
                GameEventManager.AddCallback(preset.uiCloseGameEventID, Close);
            }

            if (preset.enableUIGameEvent)
            {
                GameEventManager.AddCallback(preset.uiToggleGameEventID, Toggle);
            }
        }

        protected virtual void OnInit(UIPanelPreset preset)
        {

        }

        protected virtual void OnPostInit(UIPanelPreset preset)
        {

        }

        #endregion

        #region Basic Event

        protected virtual void OnCrash()
        {
            
        }

        protected virtual void OnRecreate(IUIPanelController newPanel)
        {
            if (isOpened)
            {
                newPanel.Open();
            }
        }

        protected virtual void OnCreate()
        {
            if (preset.autoOpenOnCreation)
            {
                this.Open();
            }
        }

        protected virtual void OnOpenInstantly(IUIPanelController source)
        {

        }

        protected virtual void OnCloseInstantly(IUIPanelController source)
        {

        }

        protected virtual void OnSetEnabled()
        {

        }
        
        protected virtual void OnDestruction()
        {
            UIPanelPool.Unregister(this);
            
            if (preset.enableUICloseGameEvent)
            {
                GameEventManager.RemoveCallback(preset.uiCloseGameEventID, Close);
            }

            if (preset.enableUIGameEvent)
            {
                GameEventManager.RemoveCallback(preset.uiToggleGameEventID, Toggle);
            }

            if (isOpened)
            {
                GameEventManager.Enable(preset.gameEventDisabledOnOpen);
            }
        }

        #endregion

        #region Basic Operation

        public void OpenInstantly(IUIPanelController source)
        {
            if (isClosing)
            {
                Debug.LogWarning($"{name}正在关闭，打开UI失败");
                return;
            }

            if (isDebugging)
            {
                Debug.LogWarning($"{name}打开");
            }

            if (isOpened)
            {
                return;
            }

            GameEventManager.Disable(preset.gameEventDisabledOnOpen);

            isOpened = true;

            OnOpenInstantly(source);

            OnOpenInstantlyEvent?.Invoke(this);
        }
        
        public void CloseInstantly(IUIPanelController source)
        {
            if (isDebugging)
            {
                Debug.LogWarning($"{name}关闭");
            }

            isClosing = false;

            isOpened = false;

            OnCloseInstantly(source);

            OnCloseInstantlyEvent?.Invoke(this);

            GameEventManager.Enable(preset.gameEventDisabledOnOpen);
        }
        
        public void SetEnabled(bool enableState)
        {
            if (isDebugging)
            {
                Debug.LogWarning($"{name}激活状态：{enableState}");
            }

            uiEnabled = enableState;

            OnSetEnabled();
        }

        #endregion
    }
}
