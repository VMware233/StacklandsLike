using System;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public sealed class LimitedArrayComponentPool<TComponent>
        : LimitedArrayReferencePool<TComponent>, IComponentPool<TComponent>
        where TComponent : Component
    {
        public LimitedArrayComponentPool(int capacity, Func<TComponent> creator,
            Action<TComponent> onGetCallback = null, Action<TComponent> onReturnCallback = null,
            Action<TComponent> onClearCallback = null) : base(capacity, creator, onGetCallback ?? ComponentPoolDefaultAction.OnGet,
            onReturnCallback ?? ComponentPoolDefaultAction.OnReturn, onClearCallback ?? ComponentPoolDefaultAction.OnClear)
        {
            
        }
    }
}