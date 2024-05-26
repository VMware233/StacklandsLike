using System;

namespace VMFramework.Core.Pool
{
    public abstract partial class LimitedReferencePool<TItem> : ReferencePool<TItem>, ILimitedPool<TItem>
    {
        public int capacity { get; protected set; }

        protected LimitedReferencePool(int capacity, Func<TItem> creator, Action<TItem> onGetCallback, Action<TItem> onReturnCallback, Action<TItem> onClearCallback) : base(creator, onGetCallback, onReturnCallback, onClearCallback)
        {
            this.capacity = capacity;
        }
        
        public abstract void Resize(int newCapacity);
    }
}