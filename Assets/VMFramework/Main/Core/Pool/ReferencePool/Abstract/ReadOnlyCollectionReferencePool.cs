using System;
using System.Collections.Generic;

namespace VMFramework.Core.Pool
{
    public abstract partial class ReadOnlyCollectionReferencePool<TItem, TCollection> : ReferencePool<TItem>
        where TCollection : class, IReadOnlyCollection<TItem>, new()
    {
        protected readonly TCollection pool = new();

        public override int count => pool.Count;

        protected ReadOnlyCollectionReferencePool(Func<TItem> creator, Action<TItem> onGetCallback,
            Action<TItem> onReturnCallback, Action<TItem> onClearCallback) : base(creator, onGetCallback,
            onReturnCallback, onClearCallback)
        {

        }

        public override void Clear()
        {
            foreach (var item in pool)
            {
                onClearCallback(item);
            }
        }
    }
}