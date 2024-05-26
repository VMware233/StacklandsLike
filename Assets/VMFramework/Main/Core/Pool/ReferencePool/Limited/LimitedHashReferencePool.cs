using System;
using System.Collections.Generic;

namespace VMFramework.Core.Pool
{
    public class LimitedHashReferencePool<TItem>
        : LimitedCollectionReferencePool<TItem, HashSet<TItem>>
    {
        public LimitedHashReferencePool(int capacity, Func<TItem> creator, Action<TItem> onGetCallback,
            Action<TItem> onReturnCallback, Action<TItem> onClearCallback) : base(capacity, creator,
            onGetCallback, onReturnCallback, onClearCallback)
        {
        }
    }
}