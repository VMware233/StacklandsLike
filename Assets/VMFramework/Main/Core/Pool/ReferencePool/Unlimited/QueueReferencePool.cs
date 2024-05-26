using System;
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public class QueueReferencePool<TItem> : ReadOnlyCollectionReferencePool<TItem, Queue<TItem>>
    {
        public QueueReferencePool(Func<TItem> creator, Action<TItem> onGetCallback,
            Action<TItem> onReturnCallback, Action<TItem> onClearCallback) : base(creator, onGetCallback,
            onReturnCallback, onClearCallback)
        {

        }

        public override TItem Get(out bool isFreshlyCreated)
        {
            if (pool.TryDequeue(out var item))
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

            pool.Enqueue(item);
            onReturnCallback(item);
        }
    }
}