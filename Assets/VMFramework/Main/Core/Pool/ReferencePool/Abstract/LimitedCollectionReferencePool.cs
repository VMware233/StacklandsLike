using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public abstract class LimitedCollectionReferencePool<TItem, TCollection>
        : LimitedReadOnlyCollectionReferencePool<TItem, TCollection>, ICheckablePool<TItem>
        where TCollection : class, ICollection<TItem>, IReadOnlyCollection<TItem>, new()
    {
        protected LimitedCollectionReferencePool(int capacity, Func<TItem> creator,
            Action<TItem> onGetCallback, Action<TItem> onReturnCallback,
            Action<TItem> onClearCallback) : base(capacity, creator, onGetCallback, onReturnCallback,
            onClearCallback)
        {

        }

        public override TItem Get(out bool isFreshlyCreated)
        {
            if (count != 0)
            {
                var newOne = pool.First();
                pool.Remove(newOne);

                onGetCallback(newOne);

                isFreshlyCreated = false;
                return newOne;
            }

            isFreshlyCreated = true;
            return creator();
        }

        public override void Return(TItem item)
        {
            if (item == null)
            {
                Debug.LogError("Cannot return null item to the pool!");
                return;
            }
            
            if (count == capacity)
            {
                onClearCallback(item);
                return;
            }
            
            onReturnCallback(item);
            pool.Add(item);
        }

        public bool Contains(TItem item)
        {
            return pool.Contains(item);
        }
    }
}