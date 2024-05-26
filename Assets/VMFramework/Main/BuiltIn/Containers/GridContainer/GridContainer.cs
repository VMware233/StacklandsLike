using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Linq;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    public class GridContainer : Container
    {
        protected GridContainerPreset gridContainerPreset => (GridContainerPreset)gamePrefab;

        [ShowInInspector]
        private IContainerItem[] items;

        public override int size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => items?.Length ?? 0;
        }

        public override bool isFull
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => validItemsSize == size;
        }

        public override bool isEmpty => validItemsSize == 0;

        protected override void OnCreate()
        {
            base.OnCreate();

            gridContainerPreset.size.AssertIsAbove(0, $"{nameof(GridContainerPreset)}'s size");

            items = new IContainerItem[gridContainerPreset.size];
        }

        #region Basic Operation

        #region Get

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetItem(int index, out IContainerItem item)
        {
            if (index < 0 || index >= items.Length)
            {
                item = null;
                return false;
            }

            item = items[index];

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IContainerItem GetItem(int index)
        {
            if (index < 0 || index >= items.Length)
            {
                return null;
            }

            return items[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IContainerItem GetItemWithoutCheck(int index)
        {
            return items[index];
        }

        #endregion

        #region Set

        public override void SetItem(int index, IContainerItem item)
        {
            if (index < 0 || index >= items.Length)
            {
                Debug.LogWarning($"设置物品 {item} 时index: {index} 越界");
                return;
            }

            var targetItem = item;

            if (targetItem is { count: <= 0 })
            {
                targetItem = null;
            }

            var oldItem = items[index];

            if (oldItem != null)
            {
                OnItemRemoved(index, oldItem);
            }

            OnBeforeItemChanged(index, oldItem);

            items[index] = targetItem;

            if (targetItem != null)
            {
                OnItemAdded(index, targetItem);
            }

            OnAfterItemChanged(index, targetItem);

            if (isDebugging)
            {
                if (targetItem == null)
                {
                    Debug.LogWarning($"{this}的{index}被设置成了Null");
                }
                else
                {
                    Debug.LogWarning($"{this}的{index}被设置成了{targetItem}");
                }
            }
        }

        #endregion

        #region Add

        protected virtual bool PreTryAddItem(IContainerItem item,
            out IEnumerable<int> slotIndices, out bool shouldEndAddingItem)
        {
            slotIndices = Enumerable.Empty<int>();
            shouldEndAddingItem = false;
            return false;
        }

        public override bool TryAddItem(IContainerItem item, int startIndex,
            int endIndex)
        {
            if (isDebugging)
            {
                Debug.LogWarning($"尝试添加物品 {item}");
            }

            if (item == null)
            {
                return true;
            }

            if (item.count <= 0)
            {
                return true;
            }

            if (startIndex > endIndex)
            {
                return false;
            }

            if (PreTryAddItem(item, out var preSlotIndices,
                    out var shouldEndAddingItem))
            {
                return true;
            }

            if (shouldEndAddingItem)
            {
                return false;
            }

            startIndex = startIndex.ClampMin(0);
            endIndex = endIndex.ClampMax(items.Length - 1);

            for (var i = startIndex; i <= endIndex; i++)
            {
                var itemInContainer = items[i];
                if (itemInContainer == null)
                {
                    continue;
                }

                if (TryMergeItem(i, item))
                {
                    if (item.count <= 0)
                    {
                        return true;
                    }
                }
            }

            for (var i = startIndex; i <= endIndex; i++)
            {
                var itemInContainer = items[i];
                if (itemInContainer != null)
                {
                    continue;
                }

                bool shouldReturn;

                if (item.count <= item.maxStackCount)
                {
                    SetItem(i, item);

                    shouldReturn = true;
                }
                else
                {
                    var cloneItem = item.GetClone();
                    cloneItem.count = item.maxStackCount;
                    item.count -= item.maxStackCount;

                    SetItem(i, cloneItem);

                    shouldReturn = false;
                }

                if (shouldReturn)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Pop

        public override bool TryPopItemByPreferredCount(int preferredCount, out IContainerItem item,
            out int slotIndex)
        {
            for (int i = items.Length - 1; i >= 0; i--)
            {
                if (this.TryPopItemByPreferredCount(i, preferredCount, out item))
                {
                    slotIndex = i;
                    return true;
                }
            }

            item = null;
            slotIndex = default;
            return false;
        }

        #endregion

        #endregion

        #region Sort

        public override void Sort(int startIndex, int endIndex,
            Comparison<IContainerItem> comparison)
        {
            startIndex = startIndex.ClampMin(0);
            endIndex = endIndex.ClampMax(size - 1);
            
            this.StackItems(startIndex, endIndex);

            var itemList = this.GetRangeItems(startIndex, endIndex).ToList();

            itemList.RemoveAllNull();

            if (itemList.Count != 0)
            {
                return;
            }

            itemList.Sort(comparison);

            for (int i = 0; i < itemList.Count; i++)
            {
                SetItem(startIndex + i, itemList[i]);
            }

            for (int i = startIndex + itemList.Count; i <= endIndex; i++)
            {
                SetItem(i, null);
            }
        }

        #endregion

        #region Compress Items

        public override void Compress(int startIndex, int endIndex)
        {
            startIndex = startIndex.ClampMin(0);
            endIndex = endIndex.ClampMax(size - 1);

            var itemList = this.GetRangeItems(startIndex, endIndex).ToList();

            itemList.RemoveAllNull();

            if (itemList.Count == 0)
            {
                return;
            }

            for (int i = 0; i < itemList.Count; i++)
            {
                SetItem(startIndex + i, itemList[i]);
            }

            for (int i = startIndex + itemList.Count; i <= endIndex; i++)
            {
                SetItem(i, null);
            }
        }

        #endregion

        #region Get Items

        public override IEnumerable<IContainerItem> GetAllItems()
        {
            return items;
        }

        public override IContainerItem[] GetItemArray()
        {
            return items;
        }

        public Dictionary<int, IContainerItem> GetSomeItemsDictionary(
            IEnumerable<int> slotIndices)
        {
            var items = new Dictionary<int, IContainerItem>();

            foreach (var slotIndex in slotIndices)
            {
                var item = GetItem(slotIndex);

                if (item != null)
                {
                    items[slotIndex] = item;
                }
            }

            return items;
        }

        #endregion

        #region Load

        public override void LoadFromItemArray<TItem>(TItem[] itemsArray)
        {
            for (int i = 0; i < itemsArray.Length; i++)
            {
                SetItem(i, itemsArray[i]);
            }

            for (int i = itemsArray.Length; i < items.Length; i++)
            {
                SetItem(i, null);
            }
        }

        #endregion

        #region Resize

        public void Resize(int newSize)
        {
            if (newSize == items.Length)
            {
                return;
            }

            var newItems = new IContainerItem[newSize];

            for (int i = 0; i < newSize && i < items.Length; i++)
            {
                newItems[i] = items[i];
            }

            items = newItems;
        }

        #endregion

        #region Debug

        #region Local

        [Button(nameof(Shuffle))]
        private void Shuffle()
        {
            var itemList = items.ToList();

            itemList.Shuffle();

            foreach (var (slotIndex, item) in itemList.Enumerate())
            {
                SetItem(slotIndex, item);
            }
        }

        #endregion

        #endregion
    }
}
