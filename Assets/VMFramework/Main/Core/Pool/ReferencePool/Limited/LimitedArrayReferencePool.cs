using System;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public partial class LimitedArrayReferencePool<TItem> : LimitedReferencePool<TItem>
    {
        private TItem[] pool;
        
        private int _count = 0;
        
        public override int count => _count;


        public LimitedArrayReferencePool(int capacity, Func<TItem> creator, Action<TItem> onGetCallback, Action<TItem> onReturnCallback, Action<TItem> onClearCallback) : base(capacity, creator, onGetCallback, onReturnCallback, onClearCallback)
        {
            pool = new TItem[capacity];
        }

        
        public override TItem Get(out bool isFreshlyCreated)
        {
            if (count > 0)
            {
                _count--;
                var item = pool[_count];
                
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
            
            if (_count >= capacity)
            {
                onClearCallback(item);
                return;
            }
            
            pool[_count] = item;
            _count++;
            onReturnCallback(item);
        }

        public override void Clear()
        {
            for (int i = 0; i < _count; i++)
            {
                onClearCallback(pool[i]);
            }

            _count = 0;
        }

        public override void Resize(int newCapacity)
        {
            newCapacity.AssertIsAbove(0, nameof(newCapacity));
            
            
            var newPool = new TItem[newCapacity];
            
            for (int i = 0; i < _count; i++)
            {
                newPool[i] = pool[i];

                if (i >= newCapacity)
                {
                    onClearCallback(pool[i]);
                }
            }
            
            pool = newPool;
            capacity = newCapacity;
        }
    }
}