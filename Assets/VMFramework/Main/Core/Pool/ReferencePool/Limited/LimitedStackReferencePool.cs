using System;
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public class LimitedStackReferencePool<TItem>
        : LimitedReadOnlyCollectionReferencePool<TItem, Stack<TItem>>
    {
        public LimitedStackReferencePool(int capacity, Func<TItem> creator, Action<TItem> onGetCallback,
            Action<TItem> onReturnCallback, Action<TItem> onClearCallback) : base(capacity, creator,
            onGetCallback, onReturnCallback, onClearCallback)
        {
        }
        
        public override TItem Get(out bool isFreshlyCreated)
        {
            if (pool.TryPop(out TItem item))
            {
                onGetCallback(item);

                isFreshlyCreated = false;
                return item;
            }

            isFreshlyCreated = true;
            return creator();
        }

        public override void Return(TItem item)
        {
            if (item == null)
            {
                Debug.LogError("Cannot return null item to the pool.");
                return;
            }

            pool.Push(item);
            onReturnCallback(item);
        }
    }
}