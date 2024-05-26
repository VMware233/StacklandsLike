using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;
using VMFramework.Core;
using VMFramework.GameEvents;

namespace VMFramework.UI
{
    public class UIToolkitContextMenuUIController : UIToolkitTracingUIPanelController, IContextMenu
    {
        private struct ContextMenuEntryInfo
        {
            public IconLabelVisualElement iconLabel;
        }

        protected UIToolkitContextMenuUIPreset contextMenuUIPreset { get; private set; }

        [ShowInInspector]
        private VisualElement contextMenuEntryContainer;

        [ShowInInspector]
        private List<ContextMenuEntryInfo> entryInfos = new();

        [ShowInInspector]
        private IContextMenuProvider provider;

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            contextMenuUIPreset = preset as UIToolkitContextMenuUIPreset;

            contextMenuUIPreset.AssertIsNotNull(nameof(contextMenuUIPreset));
        }

        protected override void OnInit(UIPanelPreset preset)
        {
            base.OnInit(preset);

            UIPanelPointerEventManager.OnPanelOnMouseClickChanged += (oldPanel, currentPanel) =>
            {
                if ((UIToolkitContextMenuUIController)currentPanel != this)
                {
                    this.Close();
                }
            };

            if (contextMenuUIPreset.gameEventIDsToClose != null)
            {
                foreach (var gameEventID in contextMenuUIPreset.gameEventIDsToClose)
                {
                    GameEventManager.AddCallback(gameEventID, Close);
                }
            }
        }

        protected override void OnDestruction()
        {
            base.OnDestruction();
            
            if (contextMenuUIPreset.gameEventIDsToClose != null)
            {
                foreach (var gameEventID in contextMenuUIPreset.gameEventIDsToClose)
                {
                    GameEventManager.RemoveCallback(gameEventID, Close);
                }
            }
        }

        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            contextMenuEntryContainer = rootVisualElement.Q(
                contextMenuUIPreset.contextMenuEntryContainerName);

            contextMenuEntryContainer.AssertIsNotNull(nameof(contextMenuEntryContainer));
        }

        protected override void OnCloseInstantly(IUIPanelController source)
        {
            base.OnCloseInstantly(source);

            entryInfos.Clear();

            provider = null;
        }

        public void Open(IContextMenuProvider provider, IUIPanelController source)
        {
            this.Close();

            var entryContents = provider.GetContextMenuContent().ToArray();

            if (entryContents.Length == 0)
            {
                return;
            }

            if (entryContents.Length == 1 && contextMenuUIPreset.autoExecuteIfOnlyOneEntry)
            {
                entryContents[0].action.Invoke();
                return;
            }

            this.provider = provider;

            this.Open(source);

            contextMenuEntryContainer.Clear();

            foreach (var entryConfig in entryContents)
            {
                var iconLabel = new IconLabelVisualElement
                {
                    iconAlwaysDisplay = true
                };

                iconLabel.SetIcon(entryConfig.icon);
                iconLabel.SetContent(entryConfig.title);

                iconLabel.RegisterCallback<MouseUpEvent>(evt =>
                {
                    if (contextMenuUIPreset.clickMouseButtonType.HasMouseButton(evt.button))
                    {
                        entryConfig.action.Invoke();
                        Close(provider);
                    }
                });

                iconLabel.RegisterCallback<MouseEnterEvent>(evt =>
                {
                    iconLabel.SetIcon(contextMenuUIPreset.entrySelectedIcon);
                });
                iconLabel.RegisterCallback<MouseLeaveEvent>(evt => { iconLabel.SetIcon(entryConfig.icon); });

                AddVisualElement(contextMenuEntryContainer, iconLabel);

                entryInfos.Add(new ContextMenuEntryInfo
                {
                    iconLabel = iconLabel
                });
            }
        }

        public void Close(IContextMenuProvider provider)
        {
            if (this.provider == provider)
            {
                this.Close();
            }
        }
    }
}