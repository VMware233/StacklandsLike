using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public abstract class CollectionReferencePool<TItem, TCollection>
        : ReadOnlyCollectionReferencePool<TItem, TCollection>, ICheckablePool<TItem>
        where TCollection : class, ICollection<TItem>, IReadOnlyCollection<TItem>, new()
    {
        protected CollectionReferencePool(Func<TItem> creator, Action<TItem> onGetCallback,
            Action<TItem> onReturnCallback, Action<TItem> onClearCallback) : base(creator, onGetCallback,
            onReturnCallback, onClearCallback)
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
            
            pool.Add(item);
            onReturnCallback(item);
        }
        
        public bool Contains(TItem item)
        {
            return pool.Contains(item);
        }
    }
}