using System;
using System.Collections.Generic;

namespace VMFramework.Core.Pool
{
    public abstract partial class LimitedReadOnlyCollectionReferencePool<TItem, TCollection>
        : LimitedReferencePool<TItem>
        where TCollection : class, IReadOnlyCollection<TItem>, new()
    {
        protected readonly TCollection pool = new();

        public override int count => pool.Count;

        protected LimitedReadOnlyCollectionReferencePool(int capacity, Func<TItem> creator,
            Action<TItem> onGetCallback, Action<TItem> onReturnCallback,
            Action<TItem> onClearCallback) : base(capacity, creator, onGetCallback, onReturnCallback,
            onClearCallback)
        {
        }

        public override void Clear()
        {
            foreach (var item in pool)
            {
                onClearCallback(item);
            }
        }

        public override void Resize(int newCapacity)
        {
            newCapacity.AssertIsAbove(0, nameof(newCapacity));
            capacity = newCapacity;
        }
    }
}