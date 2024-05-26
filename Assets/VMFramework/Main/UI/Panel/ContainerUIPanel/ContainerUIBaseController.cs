using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;
using VMFramework.Core;
using VMFramework.Containers;

namespace VMFramework.UI
{
    public struct AutoFillContainerConfigRuntime : IAutoFillContainerConfig
    {
        public VisualElement autoFillContainer { get; init; }

        public AutoFillContainerSlotsPreprocessMode autoFillContainerSlotsPreprocessMode { get; init; }

        public int autoFillSlotStartIndex { get; init; }

        public bool slotsDisplayNoneIfNull { get; init; }
    }

    public class ContainerUIBaseController : UIToolkitPanelController, IContainerUIPanel
    {
        protected ContainerUIBasePreset containerUIBasePreset { get; private set; }

        [ShowInInspector]
        protected ContainerUICore containerUICore { get; private set; } = new();

        public int containerUIPriority => containerUIBasePreset.containerUIPriority;

        #region Update

        protected virtual void Update()
        {
            if (containerUICore.bindContainer == null)
            {
                return;
            }

            // if (containerUICore.bindContainer.isDirty && uiEnabled)
            // {
            //     SetEnabled(false);
            // }
            // else if (containerUICore.bindContainer.isDirty == false && uiEnabled == false)
            // {
            //     SetEnabled(true);
            // }
        }

        #endregion

        #region Basic UI Event

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            containerUIBasePreset = preset as ContainerUIBasePreset;

            containerUIBasePreset.AssertIsNotNull(nameof(containerUIBasePreset));

            containerUICore.OnBindContainerEvent += OnBindContainer;
            containerUICore.OnUnbindContainerEvent += OnUnbindContainer;
        }

        protected override void OnRecreate(IUIPanelController newPanel)
        {
            base.OnRecreate(newPanel);

            if (newPanel is ContainerUIBaseController containerUIPanel)
            {
                if (containerUICore.bindContainer != null)
                {
                    containerUIPanel.SetBindContainer(containerUICore.bindContainer);
                }
            }
        }

        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            var slotSourceContainers = new List<VisualElement>();

            if (containerUIBasePreset.useCustomSlotSourceContainer)
            {
                foreach (var customSlotSourceContainerName in containerUIBasePreset
                             .customSlotSourceContainerNames)
                {
                    var customSlotSourceContainer = rootVisualElement.Q(customSlotSourceContainerName);

                    customSlotSourceContainer.AssertIsNotNull(customSlotSourceContainerName);

                    slotSourceContainers.Add(customSlotSourceContainer);
                }
            }
            else
            {
                slotSourceContainers.Add(rootVisualElement);
            }

            List<AutoFillContainerConfigRuntime> autoFillContainerConfigs = new();
            if (containerUIBasePreset.autoFillSlot)
            {
                foreach (var config in containerUIBasePreset.autoFillContainerConfigs)
                {
                    var autoFillSlotContainer = rootVisualElement.Q(config.autoFillSlotContainerName);

                    autoFillSlotContainer.AssertIsNotNull(nameof(autoFillSlotContainer));

                    autoFillContainerConfigs.Add(new AutoFillContainerConfigRuntime
                    {
                        autoFillContainer = autoFillSlotContainer,
                        autoFillContainerSlotsPreprocessMode = config.autoFillContainerSlotsPreprocessMode,
                        autoFillSlotStartIndex = config.autoFillSlotStartIndex,
                        slotsDisplayNoneIfNull = config.slotsDisplayNoneIfNull
                    });
                }
            }

            containerUICore.Open(new ContainerUICoreOpenConfig
            {
                isDebugging = isDebugging,
                source = this,
                slotSourceContainers = slotSourceContainers,
                addVisualElementAction = AddVisualElement,
                autoFillSlots = containerUIBasePreset.autoFillSlot,
                autoFillContainerConfigs = autoFillContainerConfigs.Cast<IAutoFillContainerConfig>(),
                autoAllocateSlotIndex = containerUIBasePreset.autoAllocateSlotIndex,
                ignorePreallocateSlotIndex = containerUIBasePreset.ignorePreallocateSlotIndex
            });
        }

        protected override void OnCloseInstantly(IUIPanelController source)
        {
            base.OnCloseInstantly(source);

            containerUICore.Close();
        }

        protected override void OnSetEnabled()
        {
            base.OnSetEnabled();

            containerUICore.SetEnabled(uiEnabled);
        }

        #endregion

        #region Slot Visual Element

        #endregion

        #region Bind Container

        protected virtual void OnBindContainer(IContainer container)
        {
        }

        protected virtual void OnUnbindContainer(IContainer container)
        {
        }

        public void SetBindContainer(IContainer newBindContainer)
        {
            containerUICore.SetBindContainer(newBindContainer);
        }

        public IEnumerable<IContainer> GetBindContainers()
        {
            yield return containerUICore.bindContainer;
        }

        #endregion
    }
}