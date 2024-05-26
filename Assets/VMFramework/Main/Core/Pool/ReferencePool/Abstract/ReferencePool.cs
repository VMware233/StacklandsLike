using System;

namespace VMFramework.Core.Pool
{
    public abstract class ReferencePool<TItem> : IPool<TItem>
    {
        protected readonly Action<TItem> onGetCallback;
        protected readonly Func<TItem> creator;
        protected readonly Action<TItem> onReturnCallback;
        protected readonly Action<TItem> onClearCallback;
        
        public abstract int count { get; }

        protected ReferencePool(Func<TItem> creator, Action<TItem> onGetCallback,
            Action<TItem> onReturnCallback, Action<TItem> onClearCallback)
        {
            this.onGetCallback = onGetCallback;
            this.creator = creator;
            this.onReturnCallback = onReturnCallback;
            this.onClearCallback = onClearCallback;
        }
        
        public abstract TItem Get(out bool isFreshlyCreated);
        
        public abstract void Return(TItem item);
        
        public abstract void Clear();
    }
}
