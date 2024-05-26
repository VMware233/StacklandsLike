using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    public abstract partial class Container : GameItem, IContainer
    {
        [ShowInInspector]
        public IContainerOwner owner { get; private set; }

        public bool isOpen { get; private set; } = false;

        public abstract int size { get; }

        public int validItemsSize
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => validSlotIndices.Count;
        }

        [ShowInInspector]
        public abstract bool isFull { get; }

        [ShowInInspector]
        public abstract bool isEmpty { get; }

        protected readonly SortedSet<int> validSlotIndices = new();

        public event IContainer.ItemChangedHandler OnBeforeItemChangedEvent;
        public event IContainer.ItemChangedHandler OnAfterItemChangedEvent;
        public event IContainer.ItemChangedHandler OnItemAddedEvent;
        public event IContainer.ItemChangedHandler OnItemRemovedEvent;
        
        public event Action<IContainer> OnOpenEvent;
        public event Action<IContainer> OnCloseEvent;

        private readonly Dictionary<int, Action<int, int>> itemCountChangedActions = new();
        
        public event IContainer.ItemCountChangedHandler OnItemCountChangedEvent;

        public event IContainer.SizeChangedHandler OnSizeChangedEvent;

        #region Interface Implementation

        IReadOnlyCollection<int> IContainer.validSlotIndices => validSlotIndices;

        #endregion

        #region Init

        protected override void OnCreate()
        {
            base.OnCreate();

            using var containerCreateEvent = ContainerCreateEvent.Get();
            containerCreateEvent.SetContainer(this);
            containerCreateEvent.Propagate();
        }

        #endregion

        #region Owner

        public void SetOwner(IContainerOwner newOwner)
        {
            if (owner != null)
            {
                Debug.LogWarning("试图修改已经生成的容器所有者");
                return;
            }

            owner = newOwner;
        }

        #endregion

        #region Destroy

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            if (isDebugging)
            {
                Debug.LogWarning($"{this} is Destroyed");
            }
            
            using var containerDestroyEvent = ContainerDestroyEvent.Get();
            containerDestroyEvent.SetContainer(this);
            containerDestroyEvent.Propagate();
        }

        #endregion

        #region Open & Close

        public void Open()
        {
            if (isDebugging)
            {
                Debug.LogWarning($"打开容器：{this}");
            }

            isOpen = true;
            
            OnOpenEvent?.Invoke(this);
        }

        public void Close()
        {
            if (isDebugging)
            {
                Debug.LogWarning($"关闭容器：{this}");
            }

            isOpen = false;
            
            OnCloseEvent?.Invoke(this);
        }

        #endregion

        #region IContainerItem Event

        protected void OnItemAdded(int slotIndex, IContainerItem item)
        {
            item.sourceContainer = this;

            OnItemAddedEvent?.Invoke(this, slotIndex, item);

            itemCountChangedActions[slotIndex] = ItemChangedAction;

            item.OnCountChangedEvent += ItemChangedAction;

            validSlotIndices.Add(slotIndex);

            item.OnAddToContainer(this);

            return;

            void ItemChangedAction(int previousCount, int currentCount)
            {
                OnItemCountChangedEvent?.Invoke(this, slotIndex, item, previousCount,
                    currentCount);

                if (currentCount <= 0)
                {
                    SetItem(slotIndex, null);
                }
            }
        }

        protected void OnItemRemoved(int slotIndex, IContainerItem item)
        {
            OnItemRemovedEvent?.Invoke(this, slotIndex, item);

            item.OnCountChangedEvent -= itemCountChangedActions[slotIndex];

            validSlotIndices.Remove(slotIndex);

            if (item.sourceContainer == this)
            {
                item.sourceContainer = null;
            }

            item.OnRemoveFromContainer(this);
        }

        protected void OnBeforeItemChanged(int slotIndex, IContainerItem item)
        {
            OnBeforeItemChangedEvent?.Invoke(this, slotIndex, item);
        }

        protected void OnAfterItemChanged(int slotIndex, IContainerItem item)
        {
            OnAfterItemChangedEvent?.Invoke(this, slotIndex, item);
        }

        #endregion

        #region Size Event

        protected void OnSizeChanged()
        {
            OnSizeChangedEvent?.Invoke(this, size);

            if (isDebugging)
            {
                Debug.LogWarning($"{this}容器Size改变为:{size}");
            }
        }

        #endregion

        #region Get Item
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract bool TryGetItem(int index, out IContainerItem item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract IContainerItem GetItem(int index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract IContainerItem GetItemWithoutCheck(int index);

        #endregion

        #region Get All Items

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract IEnumerable<IContainerItem> GetAllItems();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract IContainerItem[] GetItemArray();

        #endregion

        #region Try Merge
        
        public bool TryMergeItem(int index, IContainerItem newItem)
        {
            var itemInContainer = GetItem(index);

            itemInContainer.AssertIsNotNull(nameof(itemInContainer));

            if (itemInContainer.IsMergeableWith(newItem) == false)
            {
                return false;
            }

            OnBeforeItemChanged(index, itemInContainer);

            itemInContainer.MergeWith(newItem);

            OnAfterItemChanged(index, itemInContainer);

            return true;
        }

        #endregion

        #region Set Item

        public abstract void SetItem(int index, IContainerItem item);

        #endregion

        #region Add Item
        
        public virtual bool TryAddItem(IContainerItem item)
        {
            return TryAddItem(item, int.MinValue, int.MaxValue);
        }
        
        public abstract bool TryAddItem(IContainerItem item, int startIndex, int endIndex);

        #endregion

        #region Pop Item
        
        public abstract bool TryPopItemByPreferredCount(int preferredCount, out IContainerItem item, 
            out int slotIndex);

        #endregion

        #region Stack Items

        [Button("堆叠物品")]
        public virtual void StackItems()
        {
            this.StackItems(int.MinValue, int.MaxValue);
        }

        #endregion

        #region Compress Items

        [Button("压缩物品")]
        public void Compress()
        {
            Compress(int.MinValue, int.MaxValue);
        }

        /// <summary>
        /// 压缩容器，去除物品间的Null
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        public abstract void Compress(int startIndex, int endIndex);

        #endregion

        #region Sort

        public virtual void Sort(Comparison<IContainerItem> comparison)
        {
            Sort(int.MinValue, int.MaxValue, comparison);
        }

        public abstract void Sort(int startIndex, int endIndex, Comparison<IContainerItem> comparison);

        #endregion

        #region String

        protected override IEnumerable<(string propertyID, string propertyContent)>
            OnGetStringProperties()
        {
            // yield return (nameof(uuid), uuid);
            yield return (nameof(validItemsSize), validItemsSize.ToString());
        }

        #endregion

        public abstract void LoadFromItemArray<TItem>(TItem[] itemsArray)
            where TItem : IContainerItem;
    }
}
