using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.Core;
using VMFramework.Core.Linq;
using VMFramework.Containers;

namespace VMFramework.UI
{
    public enum AutoFillContainerSlotsPreprocessMode
    {
        [LabelText("忽略")]
        Ignore,

        [LabelText("移除所有")]
        RemoveAll,

        [LabelText("移除序号小于1的")]
        RemoveWhereIndexLessThanOne,

        [LabelText("移除序号小于0的")]
        RemoveWhereIndexLessThanZero
    }

    public interface IAutoFillContainerConfig
    {
        public VisualElement autoFillContainer { get; }

        public AutoFillContainerSlotsPreprocessMode autoFillContainerSlotsPreprocessMode { get; }

        public int autoFillSlotStartIndex { get; }

        public bool slotsDisplayNoneIfNull { get; }
    }

    public struct ContainerUICoreOpenConfig
    {
        public bool isDebugging;

        public UIPanelController source;

        public IReadOnlyList<VisualElement> slotSourceContainers;

        public Action<VisualElement, VisualElement> addVisualElementAction;

        public bool autoFillSlots;

        public IEnumerable<IAutoFillContainerConfig> autoFillContainerConfigs;

        public bool autoAllocateSlotIndex;

        public bool ignorePreallocateSlotIndex;
    }

    public class ContainerUICore
    {
        [ShowInInspector]
        public bool isOpen { get; private set; }

        private ContainerUICoreOpenConfig config;

        private readonly List<IAutoFillContainerConfig> autoFillContainerConfigs = new();

        #region Basic Operation

        public void Open(ContainerUICoreOpenConfig config)
        {
            if (config.autoFillSlots)
            {
                autoFillContainerConfigs.Clear();

                foreach (var autoFillContainerConfig in config.autoFillContainerConfigs)
                {
                    autoFillContainerConfig.autoFillContainer.AssertIsNotNull(
                        nameof(autoFillContainerConfig.autoFillContainer));

                    autoFillContainerConfigs.Add(autoFillContainerConfig);
                }
            }

            this.config = config;

            isOpen = true;

            BuildSlotVisualElement();
        }

        public void Close()
        {
            bindContainer?.Close();
            SetBindContainer(null);

            allSlotVisualElements.Clear();
            allSlotVisualElementsBySlotIndex.Clear();

            isOpen = false;
        }

        public void SetEnabled(bool enabled)
        {
            foreach (var slot in allSlotVisualElements)
            {
                slot.SetEnabled(enabled);
            }
        }

        #endregion

        #region Slots

        [ShowInInspector]
        [ListDrawerSettings(DefaultExpandedState = false)]
        private HashSet<SlotVisualElement> allSlotVisualElements = new();

        [ShowInInspector]
        private Dictionary<int, HashSet<SlotVisualElement>> allSlotVisualElementsBySlotIndex = new();

        public event Action<SlotVisualElement, int> OnSetSlotVisualElement;

        [Button]
        public void BuildSlotVisualElement()
        {
            allSlotVisualElements.Clear();
            allSlotVisualElementsBySlotIndex.Clear();

            InitAutoFillContainer();

            var slotVisualElementsList = config.slotSourceContainers.GetAll<SlotVisualElement>().ToList();

            if (config.autoAllocateSlotIndex)
            {
                if (config.ignorePreallocateSlotIndex)
                {
                    foreach (var (slotIndex, slotVisualElement) in slotVisualElementsList.Enumerate())
                    {
                        SetSlotVisualElement(slotVisualElement, slotIndex);
                    }
                }
                else
                {
                    var leftSlotVisualElementsSet = new HashSet<SlotVisualElement>(slotVisualElementsList);

                    foreach (var slotVisualElement in slotVisualElementsList)
                    {
                        if (HasSlotVisualElement(slotVisualElement.slotIndex) == false)
                        {
                            SetSlotVisualElement(slotVisualElement, slotVisualElement.slotIndex);

                            leftSlotVisualElementsSet.Remove(slotVisualElement);
                        }
                    }

                    var leftSlotVisualElementsStack = new Stack<SlotVisualElement>(leftSlotVisualElementsSet);

                    var slotIndex = 0;
                    while (leftSlotVisualElementsStack.Count > 0)
                    {
                        if (HasSlotVisualElement(slotIndex) == false)
                        {
                            var slotVisualElement = leftSlotVisualElementsStack.Pop();

                            SetSlotVisualElement(slotVisualElement, slotIndex);
                        }

                        slotIndex++;
                    }
                }
            }
            else
            {
                foreach (var slotVisualElement in slotVisualElementsList)
                {
                    SetSlotVisualElement(slotVisualElement, slotVisualElement.slotIndex);
                }
            }

            if (bindContainer != null)
            {
                AutoFillSlots();
            }

            FreshAll();
        }

        private void InitAutoFillContainer()
        {
            if (config.autoFillSlots == false)
            {
                return;
            }

            foreach (var config in autoFillContainerConfigs)
            {
                if (config.autoFillContainerSlotsPreprocessMode ==
                    AutoFillContainerSlotsPreprocessMode.RemoveAll)
                {
                    config.autoFillContainer.ClearAll<SlotVisualElement>();
                }
                else if (config.autoFillContainerSlotsPreprocessMode ==
                         AutoFillContainerSlotsPreprocessMode.RemoveWhereIndexLessThanOne)
                {
                    config.autoFillContainer.ClearAll<SlotVisualElement>(slot => slot.slotIndex < 1);
                }
                else if (config.autoFillContainerSlotsPreprocessMode ==
                         AutoFillContainerSlotsPreprocessMode.RemoveWhereIndexLessThanZero)
                {
                    config.autoFillContainer.ClearAll<SlotVisualElement>(slot => slot.slotIndex < 0);
                }
            }
        }

        private void AutoFillSlots()
        {
            if (config.autoFillSlots)
            {
                if (bindContainer.size - allSlotVisualElementsBySlotIndex.Count > 0)
                {
                    for (var slotIndex = 0; slotIndex < bindContainer.size; slotIndex++)
                    {
                        if (HasSlotVisualElement(slotIndex))
                        {
                            continue;
                        }

                        foreach (var autoFillContainerConfig in autoFillContainerConfigs)
                        {
                            if (slotIndex >= autoFillContainerConfig.autoFillSlotStartIndex)
                            {
                                var newSlotVisualElement = new SlotVisualElement
                                {
                                    displayNoneIfNull = autoFillContainerConfig.slotsDisplayNoneIfNull
                                };

                                config.addVisualElementAction(autoFillContainerConfig.autoFillContainer,
                                    newSlotVisualElement);
                                SetSlotVisualElement(newSlotVisualElement, slotIndex);
                            }
                        }
                    }
                }
            }
        }

        protected bool HasSlotVisualElement(int slotIndex)
        {
            return allSlotVisualElementsBySlotIndex.ContainsKey(slotIndex);
        }

        protected virtual void SetSlotVisualElement(SlotVisualElement slotVisualElement, int slotIndex)
        {
            if (allSlotVisualElementsBySlotIndex.ContainsKey(slotIndex) == false)
            {
                allSlotVisualElementsBySlotIndex[slotIndex] = new();
            }

            allSlotVisualElementsBySlotIndex[slotIndex].Add(slotVisualElement);

            allSlotVisualElements.Add(slotVisualElement);

            slotVisualElement.SetSource(config.source);

            OnSetSlotVisualElement?.Invoke(slotVisualElement, slotIndex);
        }

        public IEnumerable<SlotVisualElement> GetAllSlots()
        {
            return allSlotVisualElements;
        }

        public IEnumerable<(int slotIndex, IEnumerable<SlotVisualElement> slots)> GetAllSlotsBySlotIndex()
        {
            foreach (var (slotIndex, slots) in allSlotVisualElementsBySlotIndex)
            {
                yield return (slotIndex, slots);
            }
        }

        #endregion

        #region Container Change

        private void OnItemCountChanged(IContainer container, int slotIndex, IContainerItem item,
            int previousCount, int currentCount)
        {
            if (config.isDebugging)
            {
                Debug.Log($"序号为{slotIndex}的槽位的物品{item}，数量由{previousCount}变为{currentCount}");
            }

            ISlotProvider slotProvider = null;

            if (item != null)
            {
                slotProvider = item as ISlotProvider;

                if (slotProvider == null)
                {
                    Debug.LogWarning($"物品{item}不是{nameof(ISlotProvider)}");
                    return;
                }
            }

            if (allSlotVisualElementsBySlotIndex.TryGetValue(slotIndex, out var slotVisualElements))
            {
                foreach (var slotVisualElement in slotVisualElements)
                {
                    slotVisualElement.SetSlotProvider(slotProvider);
                }
            }
            else
            {
                Debug.LogWarning($"没找到{slotIndex}对应的{nameof(slotVisualElements)}");
            }
        }

        private void OnItemChanged(IContainer container, int slotIndex, IContainerItem item)
        {
            if (config.isDebugging)
            {
                Debug.Log($"更新{slotIndex}，新的item:{item}");
            }

            ISlotProvider slotProvider = null;

            if (item != null)
            {
                slotProvider = item as ISlotProvider;

                if (slotProvider == null)
                {
                    Debug.LogWarning($"物品{item}不是{nameof(ISlotProvider)}");
                    return;
                }
            }

            if (allSlotVisualElementsBySlotIndex.TryGetValue(slotIndex, out var slotVisualElements))
            {
                foreach (var slotVisualElement in slotVisualElements)
                {
                    slotVisualElement.SetSlotProvider(slotProvider);
                }

                if (config.isDebugging)
                {
                    Debug.Log($"改变了{slotVisualElements.Count}个{nameof(SlotVisualElement)}");
                }
            }
            else
            {
                if (config.isDebugging)
                {
                    Debug.LogWarning($"没找到{slotIndex}对应的{nameof(slotVisualElements)}");
                }
            }
        }

        private void OnContainerSizeChanged(IContainer container, int currentSize)
        {
            if (config.isDebugging)
            {
                Debug.Log($"容器大小改变:{bindContainer.size}");
            }

            if (bindContainer != null && isOpen)
            {
                BuildSlotVisualElement();
            }
        }

        #endregion

        #region Bind Container

        [ShowInInspector]
        public IContainer bindContainer { get; private set; }

        public event Action<IContainer> OnUnbindContainerEvent;

        public event Action<IContainer> OnBindContainerEvent;

        public void SetBindContainer(IContainer newBindContainer)
        {
            if (isOpen == false && newBindContainer != null)
            {
                throw new InvalidOperationException("只能在打开UI后，再设置UI绑定的容器");
            }

            if (bindContainer != null)
            {
                bindContainer.OnAfterItemChangedEvent -= OnItemChanged;
                bindContainer.OnItemCountChangedEvent -= OnItemCountChanged;
                bindContainer.OnSizeChangedEvent -= OnContainerSizeChanged;

                bindContainer.Close();

                OnUnbindContainerEvent?.Invoke(bindContainer);
            }

            bindContainer = newBindContainer;

            if (bindContainer != null)
            {
                bindContainer.OnAfterItemChangedEvent += OnItemChanged;
                bindContainer.OnItemCountChangedEvent += OnItemCountChanged;
                bindContainer.OnSizeChangedEvent += OnContainerSizeChanged;

                AutoFillSlots();
                OnBindContainerEvent?.Invoke(bindContainer);

                bindContainer.Open();

                foreach (var (slotIndex, item) in bindContainer.EnumerateAllItems())
                {
                    OnItemChanged(bindContainer, slotIndex, item);
                }
            }
        }

        #endregion

        #region Utility

        [Button(nameof(FreshAll))]
        public void FreshAll()
        {
            if (bindContainer != null)
            {
                foreach (var slotIndex in allSlotVisualElementsBySlotIndex.Keys)
                {
                    OnItemChanged(bindContainer, slotIndex, bindContainer.GetItem(slotIndex));
                }
            }
        }

        #endregion
    }
}