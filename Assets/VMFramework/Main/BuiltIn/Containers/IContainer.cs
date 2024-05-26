using System;
using System.Collections.Generic;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    public partial interface IContainer : IGameItem
    {
        public bool isOpen { get; }

        public int size { get; }

        public int validItemsSize { get; }
        
        public bool isFull { get; }
        
        public IReadOnlyCollection<int> validSlotIndices { get; }
        
        public delegate void ItemChangedHandler(IContainer container, int index, IContainerItem item);

        public delegate void ItemCountChangedHandler(IContainer container, int index, IContainerItem item,
            int oldCount, int newCount);
        
        public delegate void SizeChangedHandler(IContainer container, int currentSize);

        public event ItemChangedHandler OnBeforeItemChangedEvent;
        public event ItemChangedHandler OnAfterItemChangedEvent;
        public event ItemChangedHandler OnItemAddedEvent;
        public event ItemChangedHandler OnItemRemovedEvent;
        public event ItemCountChangedHandler OnItemCountChangedEvent;
        public event SizeChangedHandler OnSizeChangedEvent;
        
        public event Action<IContainer> OnOpenEvent;
        public event Action<IContainer> OnCloseEvent;

        /// <summary>
        /// 尝试获取IContainerItem，如果index无效，返回false，其他情况不管item是否为null，都返回true
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TryGetItem(int index, out IContainerItem item);
        
        public IContainerItem GetItem(int index);
        
        public IContainerItem GetItemWithoutCheck(int index);
        
        public IEnumerable<IContainerItem> GetAllItems();

        public IContainerItem[] GetItemArray();

        /// <summary>
        /// 尝试合并物品，如果能合并，则返回true，否则返回false
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newItem"></param>
        /// <returns></returns>
        public bool TryMergeItem(int index, IContainerItem newItem);
        
        public void SetItem(int index, IContainerItem item);

        /// <summary>
        /// 如果item完全添加到容器中，则返回true，否则返回false
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TryAddItem(IContainerItem item);
        
        /// <summary>
        /// 如果item完全添加到容器中，则返回true，否则返回false
        /// </summary>
        /// <param name="item"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public bool TryAddItem(IContainerItem item, int startIndex, int endIndex);

        public bool TryPopItemByPreferredCount(int preferredCount, out IContainerItem item,
            out int slotIndex);
        
        public void Sort(int startIndex, int endIndex, Comparison<IContainerItem> comparison);

        /// <summary>
        /// 堆叠物品，将相同物品的数量合并到一起
        /// </summary>
        public void StackItems();

        /// <summary>
        /// 压缩容器，去除物品间的Null
        /// </summary>
        public void Compress();
        
        public void LoadFromItemArray<TItem>(TItem[] itemsArray) where TItem : IContainerItem;

        public void Open();

        public void Close();
    }
}