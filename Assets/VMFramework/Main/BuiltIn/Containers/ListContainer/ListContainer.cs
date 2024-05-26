using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    public class ListContainer : Container
    {
        public override int size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => items?.Count ?? 0;
        }

        public override bool isFull => false;

        public override bool isEmpty => validItemsSize == 0;

        public int totalItemCount
        {
            get
            {
                if (items.Count == 0)
                {
                    return 0;
                }

                return items.Sum(item => item.count);
            }
        }

        [LabelText("物品")]
        [ShowInInspector]
        private List<IContainerItem> items = new();

        #region Get

        public override bool TryGetItem(int index, out IContainerItem item)
        {
            if (index < 0 || index >= items.Count)
            {
                item = null;
                return false;
            }

            item = items[index];

            return true;
        }

        public override IContainerItem GetItem(int index)
        {
            if (index < 0 || index >= items.Count)
            {
                return null;
            }

            return items[index];
        }

        public override IContainerItem GetItemWithoutCheck(int index)
        {
            return items[index];
        }

        #endregion

        #region Set

        public override void SetItem(int index, IContainerItem item)
        {
            if (index < 0 || index >= items.Count)
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

        public override bool TryAddItem(IContainerItem item, int startIndex, int endIndex)
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

            var clampedStartIndex = startIndex.ClampMin(0);
            var clampedEndIndex = endIndex.ClampMax(items.Count - 1);

            for (var i = clampedStartIndex; i <= clampedEndIndex; i++)
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

            for (var i = clampedStartIndex; i <= clampedEndIndex; i++)
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

            var addTimes = 0;
            while (true)
            {
                if (items.Count >= endIndex)
                {
                    return false;
                }

                if (item.count <= item.maxStackCount)
                {
                    items.Add(null);

                    OnSizeChanged();
                    
                    // Debug.LogWarning($"添加物品 {item} 到{this}的{items.Count - 1} 位置");

                    SetItem(items.Count - 1, item);

                    break;
                }

                var cloneItem = item.GetClone();
                cloneItem.count = item.maxStackCount;
                item.count -= item.maxStackCount;

                items.Add(null);

                OnSizeChanged();

                SetItem(items.Count - 1, cloneItem);

                addTimes++;

                if (addTimes >= 50)
                {
                    break;
                }
            }

            return true;
        }

        #endregion

        #region Pop

        public override bool TryPopItemByPreferredCount(int preferredCount, out IContainerItem item,
            out int slotIndex)
        {
            for (var i = items.Count - 1; i >= 0; i--)
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

        #region Sort

        public override void Sort(int startIndex, int endIndex, Comparison<IContainerItem> comparison)
        {
            startIndex = startIndex.ClampMin(0);
            endIndex = endIndex.ClampMax(size - 1);
            
            this.StackItems(startIndex, endIndex);

            var itemList = this.GetRangeItems(startIndex, endIndex).ToList();

            itemList.RemoveAllNull();

            itemList.Sort(comparison);

            for (var i = 0; i < itemList.Count; i++)
            {
                SetItem(startIndex + i, itemList[i]);
            }

            for (var i = endIndex; i >= startIndex + itemList.Count; i--)
            {
                items.RemoveAt(i);
            }

            OnSizeChanged();
        }

        #endregion

        #region Compress

        public override void Compress(int startIndex, int endIndex)
        {
            startIndex = startIndex.ClampMin(0);
            endIndex = endIndex.ClampMax(size - 1);
            
            var itemList = this.GetRangeItems(startIndex, endIndex).ToList();

            itemList.RemoveAllNull();

            for (var i = 0; i < itemList.Count; i++)
            {
                SetItem(startIndex + i, itemList[i]);
            }

            for (var i = endIndex; i >= startIndex + itemList.Count; i--)
            {
                items.RemoveAt(i);
            }

            OnSizeChanged();
        }

        #endregion

        #region Get All

        public override IEnumerable<IContainerItem> GetAllItems()
        {
            return items;
        }

        public override IContainerItem[] GetItemArray()
        {
            return items.ToArray();
        }

        #endregion

        #region Expand

        protected void ExpandTo(int newSize)
        {
            if (newSize <= items.Count)
            {
                return;
            }

            for (var i = items.Count; i < newSize; i++)
            {
                items.Add(null);
            }

            OnSizeChanged();
        }

        #endregion

        public override void LoadFromItemArray<TItem>(TItem[] itemsArray)
        {
            for (var i = 0; i < items.Count; i++)
            {
                SetItem(i, null);
            }

            items.Clear();
            OnSizeChanged();

            for (var i = 0; i < itemsArray.Length; i++)
            {
                items.Add(null);
            }

            OnSizeChanged();

            for (var i = 0; i < itemsArray.Length; i++)
            {
                SetItem(i, itemsArray[i]);
            }
        }
    }
}