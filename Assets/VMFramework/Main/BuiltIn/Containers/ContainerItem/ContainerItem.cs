using System;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;
using VMFramework.Property;

namespace VMFramework.Containers
{
    public abstract class ContainerItem : VisualGameItem, IContainerItem
    {
        #region Fields and Properties

        [LabelText("所在容器")]
        [ShowInInspector]
        public Container sourceContainer { get; private set; }

        [LabelText("数量")]
        [ShowInInspector]
        public BaseIntProperty count;
        
        [LabelText("最大堆叠数量")]
        [ShowInInspector]
        public abstract int maxStackCount { get; }

        #endregion

        #region Interface Implementation

        Container IContainerItem.sourceContainer
        {
            get => sourceContainer;
            set => sourceContainer = value;
        }

        int IContainerItem.count
        {
            get => count.value;
            set => count.value = value;
        }

        event Action<int, int> IContainerItem.OnCountChangedEvent
        {
            add => count.OnValueChanged += value;
            remove => count.OnValueChanged -= value;
        }

        #endregion
    }
}