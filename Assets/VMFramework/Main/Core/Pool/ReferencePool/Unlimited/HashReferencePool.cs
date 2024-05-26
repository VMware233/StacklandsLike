using System;
using System.Collections.Generic;

namespace VMFramework.Core.Pool
{
    public class HashReferencePool<TItem> : CollectionReferencePool<TItem, HashSet<TItem>>
    {
        public HashReferencePool(Func<TItem> creator, Action<TItem> onGetCallback,
            Action<TItem> onReturnCallback, Action<TItem> onClearCallback) : base(creator, onGetCallback,
            onReturnCallback, onClearCallback)
        {
        }
    }
}